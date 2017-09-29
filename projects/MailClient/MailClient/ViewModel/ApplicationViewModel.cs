using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Interface;
using MailClient.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            LoggingViewModel.LogInAction += ChangePageAction;
            LoggingViewModel.LogInUserAction += LogInUserAction;
            ReceivedViewModel.ReceiveMails += ReceiveMailsAction;
            ReceivedViewModel.MailChoosen += MailChoosenAction;  

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
            //TODO something is wrong with changing view models
            if (CurrentPageViewModel != viewModel)
                CurrentPageViewModel = viewModel;
        }

        private bool ValidateChangeViewModel(IPageViewModel viewModel)
        {
            return !(_currentPageViewModel is LoggingViewModel && !(viewModel is LoggingViewModel));
        }

        private void ChangePageAction(PageNumber pageNumber)
        {
            ChangeViewModel(_pageViewModels.FindPage(pageNumber));
        }

        private IEnumerable<Mail> ReceiveMailsAction()
        {            
            return _mailBox.Receive();           
        }

        private void LogInUserAction(IUser user)
        {
            _mailBox = new MailBox(user as User);
        }

        private void SendEmailAction(Mail mail)
        {
            _mailBox.Send(mail);
        }

        private void MailChoosenAction(int mailId)
        {
            // TODO error from null ReceivedEmails because all the ReceivedViewModel page is cleared from any data
            Mail mail = (_pageViewModels.FindPage(PageNumber.Received) as ReceivedViewModel).ReceivedMails.ElementAt(mailId);
            ChangeViewModel(new MailViewModel(mail));
        }

        #endregion
    }
}
