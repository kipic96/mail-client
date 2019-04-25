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
using System.Threading.Tasks;
using System.IO;

namespace MailClient
{
    class PageManager : BaseViewModel
    {
        public PageManager()
        {
            Pages = new ObservableCollection<BaseViewModel>();
            Pages.Add(new LoggingViewModel());
            Pages.Add(new ReceivedViewModel());
            Pages.Add(new SendViewModel());
            // Add new ViewModels here

            var user = UserManager.LoadRemeberedUser();
            if (user != null)
            {
                _mailBox = new MailBox(user);
                _currentPageViewModel = FindPage(Enum.PageType.Received);
            }
            else
            {
                _mailBox = MailBox.EmptyMailBox;
                _currentPageViewModel = FindPage(Enum.PageType.Logging);
            }

            var loggingVM = FindPage(Enum.PageType.Logging) as LoggingViewModel;
            var receivedVM = FindPage(Enum.PageType.Received) as ReceivedViewModel;
            var sendVM = FindPage(Enum.PageType.Send) as SendViewModel;
            loggingVM.LogInUser += OnLogIn;
            loggingVM.ValidateEmail += OnValidateEmail;
            receivedVM.ReceiveMails += OnReceiveMails;
            receivedVM.MailChoosen += OnMailChoosen;
            sendVM.SendMail += OnMailSend;
            sendVM.ValidateEmail += OnValidateEmail;
        }

        #region fields    

        private MailBox _mailBox;
        private IList<BaseViewModel> _pages;
        private BaseViewModel _currentPageViewModel;
        private ICommand _changePageCommand;

        #endregion

        #region properties

        public Action OnLogOut { get; set; }

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

        #region commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((BaseViewModel)p),
                        p => 
                        {
                            return !(_currentPageViewModel.PageType
                                        == Enum.PageType.Logging);
                        });
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
                if (viewModel.PageType == Enum.PageType.Logging)
                {
                    LogOut();
                }
                CurrentPageViewModel = viewModel;
            }                
        }

        private void LogOut()
        {
            UserManager.ForgetUser();
            OnLogOut();
        }

        private void OnLogIn(User user)
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

        private bool OnValidateEmail(string email)
        {
            return _mailBox.ValidateEmail(email);
        }

        private IEnumerable<Mail> OnReceiveMails()
        {            
            return _mailBox.Receive();           
        }

        private void OnMailSend(Mail mail)
        {
            try
            {
                string sentEmailsCommunicate = Properties.Resources.MailSentTo + ":" + Environment.NewLine;
                string logName = "sentEmailsOn" + DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".txt";
                File.WriteAllText(logName, sentEmailsCommunicate);

                var sentEmails = _mailBox.Send(mail, logName);

                ChangeViewModel(FindPage(Enum.PageType.Received));                
                foreach (var sentEmail in sentEmails)
                {
                    sentEmailsCommunicate += sentEmail + Environment.NewLine;
                }
                LogMessage.Show(sentEmailsCommunicate);
            }
            catch (Exception ex)
            {
                LogMessage.Show(ex.Message);
            }            
        }

        private void OnMailChoosen(int mailId)
        {
            Mail mail = (FindPage(Enum.PageType.Received) 
                as ReceivedViewModel).ReceivedMails.ElementAt(mailId);
            ChangeViewModel(new MailViewModel(mail));
        }

        #endregion
    }
}
