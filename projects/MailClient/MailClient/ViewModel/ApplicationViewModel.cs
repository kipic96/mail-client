using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Model;
using MailClient.Model.Interface;
using MailClient.ViewModel.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;


namespace MailClient.ViewModel
{
    class ApplicationViewModel : BindableClass
    {
        #region fields

        private ICommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        private PageViewModels _pageViewModels = new PageViewModels();
        private IMailBox _mailBox;

        #endregion

        #region constructors

        public ApplicationViewModel()
        {
            //TODO add subsrictions to events  
            (_pageViewModels.FindPage(PageNumber.Logging) as LoggingViewModel)
                .LogInAction += LogInAction;
            (_pageViewModels.FindPage(PageNumber.Received) as ReceivedViewModel)
                .ReceiveMails += ReceiveMailsAction;
            (_pageViewModels.FindPage(PageNumber.Received) as ReceivedViewModel)
                .MailChoosen += MailChoosenAction;
            (_pageViewModels.FindPage(PageNumber.Send) as SendViewModel)
                .SendMail += SendMailAction;


            _currentPageViewModel = _pageViewModels.FindPage(PageNumber.Logging);
        }

        #endregion

        #region commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => ValidateChangeViewModel((IPageViewModel)p));
                }
                return _changePageCommand;
            }
        }        

        #endregion

        #region properties

        public PageViewModels PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    return new PageViewModels();
                return _pageViewModels;
            }
            private set
            {
                _pageViewModels = value;
                RaisePropertyChanged(nameof(PageViewModels));
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            private set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    RaisePropertyChanged(nameof(CurrentPageViewModel));
                }
            }
        }          

        #endregion

        #region methods        

        private void ChangeViewModel(IPageViewModel viewModel)
        {            
            if (CurrentPageViewModel != viewModel)
            {
                if (viewModel is LoggingViewModel)
                {
                    LogOut();
                }
                CurrentPageViewModel = viewModel;
            }                
        }

        private void LogOut()
        {
            _mailBox = new MailBox();
            _pageViewModels.Clear();
            int t = 2;
        }

        private bool ValidateChangeViewModel(IPageViewModel viewModel)
        {
            return !(_currentPageViewModel is LoggingViewModel && !(viewModel is LoggingViewModel));
        }

        private void ChangePageAction(PageNumber pageNumber)
        {
            ChangeViewModel(_pageViewModels.FindPage(pageNumber));
        }

        private IEnumerable<IMail> ReceiveMailsAction()
        {            
            return _mailBox.Receive();           
        }

        private void LogInAction(IUser user)
        {
            _mailBox = new MailBox(user as User);
            if (Model.Security.AuthenticationValidator.Authenticate(_mailBox))
            {
                ChangeViewModel(_pageViewModels.FindPage(PageNumber.Received));
            }
            else
            {
                _mailBox = new MailBox();
                Log.LogMessage.Show(Dictionary.LogMessage.WrongLoginOrPassword);
            }            
        }

        private void SendMailAction(IMail mail)
        {
            _mailBox.Send(mail);
            ChangeViewModel(_pageViewModels.FindPage(PageNumber.Received));
        }

        private void MailChoosenAction(int mailId)
        {
            // TODO error from null ReceivedEmails because all the ReceivedViewModel page is cleared from any data
            IMail mail = (_pageViewModels.FindPage(PageNumber.Received) as ReceivedViewModel).ReceivedMails.ElementAt(mailId);
            ChangeViewModel(new MailViewModel(mail));
        }

        #endregion
    }
}
