using MailClient.Model.Entity;
using MailClient.ViewModel.Base;
using MailClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MailClient.ViewModel.Log;
using MailClient.Model;

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

        #endregion 

        #region constructors

        public PageManager()
        {
            Pages = new ObservableCollection<BaseViewModel>();
            Pages.Add(new LoggingViewModel());
            Pages.Add(new ReceivedViewModel());
            Pages.Add(new SendViewModel());
            // TODO here add new viewModels if nesseary

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
                        p => ValidateChangeViewModel());
                }
                return _changePageCommand;
            }
        }

        #endregion

        #region methods     

        public BaseViewModel FindPage(Enum.PageType pageNumber)
        {
            foreach (var page in Pages)
            {
                if (page.PageType == pageNumber)
                    return page;
            }
            return null;
        }

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
            UserManager.ForgetUser();
            LogoutAction();
        }

        private bool ValidateChangeViewModel()
        {
            return !(_currentPageViewModel.PageType == Enum.PageType.Logging);
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
            if (_mailBox.Authenticate())
            {
                UserManager.RememberUser(user);
                ChangeViewModel(FindPage(Enum.PageType.Received));                
            }
            else
            {
                LogMessage.Show(Properties.Resources.WrongLoginOrPassword);
            }            
        }

        private void SendMailAction(Mail mail)
        {
            _mailBox.Send(mail);
            ChangeViewModel(FindPage(Enum.PageType.Received));
            LogMessage.Show(Properties.Resources.MailSent);
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
