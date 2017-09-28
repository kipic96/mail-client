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
            LoggingViewModel.LogInUserAction += ReceiveEmailAction;            

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
            if (_currentPageViewModel != viewModel)
                _currentPageViewModel = viewModel;
        }

        private bool ValidateChangeViewModel(IPageViewModel viewModel)
        {
            return !(_currentPageViewModel is LoggingViewModel && !(viewModel is LoggingViewModel));
        }

        private void ChangePageAction(PageNumber pageNumber)
        {
            ChangeViewModel(_pageViewModels.FindPage(pageNumber));
        }

        private void ReceiveEmailAction(User user)
        {
            _mailBox = new MailBox(user); // do i need to construct it here or maybe i can do it earlier
            // TO DO do something with received emails, maybe with events, they need to go to ReceivedViewModel
            
            var receivedEmails = _mailBox.Receive();
            if (receivedEmails != null)
            {
                /*((ReceivedViewModel)_pageViewModels.FindPage(PageNumber.Received)).ReceivedMails 
                    = (new ObservableCollection<Mail>(receivedEmails)).Cast<ReceivedMails>();*/
               (ReceivedViewModel) _pageViewModels.FindPage(PageNumber.Received) = new ReceivedViewModel(receivedEmails);

                ChangeViewModel(_pageViewModels.FindPage(PageNumber.Received));
            }            
        }

        private void SendEmailAction(User user, Mail mail)
        {
            /*// testing mail
            mail.Message = "no siema 2 :D";
            mail.Subject = "elo";
            mail.To = "kipic96@gmail.com";
            // testing mail

            _mailBox = new MailBox(user);
            _mailBox.Send(mail);*/
        }

        #endregion
    }
}
