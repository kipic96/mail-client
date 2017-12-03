using MailClient.Model.Entity;
using MailClient.Model.UserManager;
using MailClient.ViewModel.Base;
using MailClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MailClient.ViewModel.Log;

namespace MailClient
{
    class PageManager : BaseViewModel
    {
        #region fields

        private ICommand _changePageCommand;
        private BaseViewModel _currentPageViewModel;
        private IList<BaseViewModel> _pages;
        private MailBox _mailBox;

        #endregion

        public IList<BaseViewModel> Pages
        {
            get
            {
                if (_pages == null)
                    return new ObservableCollection<BaseViewModel>();
                return _pages;
            }
            set
            {
                _pages = value;
                RaisePropertyChanged(nameof(Pages));
            }
        } 

        public BaseViewModel FindPage(Enum.PageType pageNumber)
        {
            foreach (var page in Pages)
            {
                if (page.PageType == pageNumber)
                    return page;
            }
            return null;
        }

        #region constructors

        public PageManager()
        {
            Pages = new ObservableCollection<BaseViewModel>();
            Pages.Add(new LoggingViewModel());
            Pages.Add(new ReceivedViewModel());
            Pages.Add(new SendViewModel());
            // TODO here add new viewModels

            (FindPage(Enum.PageType.Logging) as LoggingViewModel)
                .LogInAction += LogInAction;
            (FindPage(Enum.PageType.Received) as ReceivedViewModel)
                .ReceiveMails += ReceiveMailsAction;
            (FindPage(Enum.PageType.Received) as ReceivedViewModel)
                .MailChoosen += MailChoosenAction;
            (FindPage(Enum.PageType.Send) as SendViewModel)
                .SendMail += SendMailAction;

            var user = UserManager.LoadRemeberedUser();
            
            if (user != null)
            {
                _mailBox = new MailBox(user);
                _currentPageViewModel = FindPage(Enum.PageType.Received);
            }
            else
            {
                _mailBox = new MailBox();
                _currentPageViewModel = FindPage(Enum.PageType.Logging);
            }            
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
                        p => ChangeViewModel((BaseViewModel)p),
                        p => ValidateChangeViewModel((BaseViewModel)p));
                }
                return _changePageCommand;
            }
        }

        #endregion

        #region properties

        public Action LogoutAction { get; set; }

        public BaseViewModel CurrentPageViewModel
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

        private void ChangeViewModel(BaseViewModel viewModel)
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
            UserManager.ForgetUser();
            LogoutAction();
        }

        private bool ValidateChangeViewModel(BaseViewModel viewModel)
        {
            return !(_currentPageViewModel is LoggingViewModel);
        }

        private void ChangePageAction(Enum.PageType pageNumber)
        {
            ChangeViewModel(FindPage(pageNumber));
        }

        private IEnumerable<Mail> ReceiveMailsAction()
        {            
            return _mailBox.Receive();           
        }

        private void LogInAction(User user)
        {
            _mailBox = new MailBox(user);            
            if (Model.Security.AuthenticationValidator.Authenticate(_mailBox))
            {
                UserManager.RememberUser(user);
                ChangeViewModel(FindPage(Enum.PageType.Received));                
            }
            else
            {
                _mailBox = new MailBox();
                LogMessage.Show(ViewModel.Dictionary.LogMessage.WrongLoginOrPassword);
            }            
        }

        private void SendMailAction(Mail mail)
        {
            _mailBox.Send(mail);
            ChangeViewModel(FindPage(Enum.PageType.Received));
            LogMessage.Show(ViewModel.Dictionary.LogMessage.MailSent);
        }

        private void MailChoosenAction(int mailId)
        {
            Mail mail = (FindPage(Enum.PageType.Received) 
                as ReceivedViewModel).ReceivedMails.ElementAt(mailId);
            ChangeViewModel(new MailViewModel(mail));
        }

        #endregion
    }
}
