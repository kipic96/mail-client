using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Interface;
using MailClient.Model;
using System.Collections.Generic;
using System.Windows.Input;


namespace MailClient.ViewModel
{
    class ApplicationViewModel : BindableClass
    {
        #region fields

        private ICommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        private IList<IPageViewModel> _pageViewModels = new List<IPageViewModel>();
        private MailBox _mailBox;

        #endregion

        #region constructors

        public ApplicationViewModel()
        {
            //TODO add new viewmodels and add subsrictions to events 
            _pageViewModels.Add(new LoggingViewModel());
            _pageViewModels.Add(new ReceivedViewModel());
            

            LoggingViewModel.LogInAction += ChangePageAction;
            LoggingViewModel.LogInUserAction += ReceiveEmailAction;            

            // Set starting page
            _currentPageViewModel = FindPageWithPageNumber(PageNumber.Logging);
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

        public IList<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    return new List<IPageViewModel>();
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

        private IPageViewModel FindPageWithPageNumber(PageNumber pageNumber)
        {
            foreach (IPageViewModel page in _pageViewModels)
            {
                if (page.PageNumber == pageNumber)
                    return page;
            }
            return null;
        }

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
            ChangeViewModel(FindPageWithPageNumber(pageNumber));
        }

        private void ReceiveEmailAction(User user)
        {
            _mailBox = new MailBox(user); // do i need to construct it here or maybe i can do it earlier
            // TO DO do something with received emails, maybe with events, they need to go to ReceivedViewModel
            
            var receivedEmails = _mailBox.Receive();
            if (receivedEmails != null)
            {
                ((ReceivedViewModel)FindPageWithPageNumber(PageNumber.Received)).ReceivedEmails = receivedEmails;
                ChangeViewModel(FindPageWithPageNumber(PageNumber.Received));
            }            
        }

        private void SendEmailAction(User user, Mail mail)
        {
            // testing mail
            mail.Message = "no siema 2 :D";
            mail.Title = "elo";
            mail.Receiver = "kipic96@gmail.com";
            // testing mail

            _mailBox = new MailBox(user);
            _mailBox.Send(mail);
        }

        #endregion
    }
}
