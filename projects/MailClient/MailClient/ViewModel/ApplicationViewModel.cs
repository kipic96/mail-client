using MailClient.Model;
using MailClient.ViewModel.Helper;
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
        private MailBox _mailBox;

        #endregion

        #region constructors

        public ApplicationViewModel()
        {
            (_pageViewModels.FindPage(Enum.PageNumber.Logging) as LoggingViewModel)
                .LogInAction += LogInAction;
            (_pageViewModels.FindPage(Enum.PageNumber.Received) as ReceivedViewModel)
                .ReceiveMails += ReceiveMailsAction;
            (_pageViewModels.FindPage(Enum.PageNumber.Received) as ReceivedViewModel)
                .MailChoosen += MailChoosenAction;
            (_pageViewModels.FindPage(Enum.PageNumber.Send) as SendViewModel)
                .SendMail += SendMailAction;


            _currentPageViewModel = _pageViewModels.FindPage(Enum.PageNumber.Logging);
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
        
        public string UserLogin
        {
            // TODO make it work
            get
            {
                if (_mailBox == null)
                    return string.Empty;
                return _mailBox.UserLogin;
            }
            private set
            {
                UserLogin = _mailBox.UserLogin;
                RaisePropertyChanged(nameof(UserLogin));
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
        }

        private bool ValidateChangeViewModel(IPageViewModel viewModel)
        {
            return !(_currentPageViewModel is LoggingViewModel);
        }

        private void ChangePageAction(Enum.PageNumber pageNumber)
        {
            ChangeViewModel(_pageViewModels.FindPage(pageNumber));
        }

        private IEnumerable<Mail> ReceiveMailsAction()
        {            
            return _mailBox.Receive();           
        }

        private void LogInAction(User user)
        {
            _mailBox = new MailBox(user as User);
            if (Model.Security.AuthenticationValidator.Authenticate(_mailBox))
            {
                ChangeViewModel(_pageViewModels.FindPage(Enum.PageNumber.Received));
            }
            else
            {
                _mailBox = new MailBox();
                Log.LogMessage.Show(Dictionary.LogMessage.WrongLoginOrPassword);
            }            
        }

        private void SendMailAction(Mail mail)
        {
            _mailBox.Send(mail);
            ChangeViewModel(_pageViewModels.FindPage(Enum.PageNumber.Received));
            Log.LogMessage.Show(Dictionary.LogMessage.MailSent);
        }

        private void MailChoosenAction(int mailId)
        {
            Mail mail = (_pageViewModels.FindPage(Enum.PageNumber.Received) as ReceivedViewModel).ReceivedMails.ElementAt(mailId);
            ChangeViewModel(new MailViewModel(mail));
        }

        #endregion
    }
}
