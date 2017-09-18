using System.Collections.Generic;
using MailClient.HelperClass;
using MailClient.Interface;
using System.Windows.Input;
using System.Windows;

namespace MailClient.ViewModel
{
    class ApplicationViewModel : BindableClass
    {
        #region fields

        private ICommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        private IList<IPageViewModel> _pageViewModels;
        private ICommand _logInCommand;

        // TODO Create singleton with controlling the application, transition of pages and stuff
        #endregion

        #region constructors

        public ApplicationViewModel()
        {
            // Add available pages in order such as in Enum.PageName

            _pageViewModels = new List<IPageViewModel> { new LoggingViewModel(), new ReceivedViewModel() };
            // information of viewmodels are saved in other instances of logging and received viewmodels

            //TODO add new viewmodels

            // Set starting page
            CurrentPageViewModel = _pageViewModels[(int)Enum.PageName.Logging];
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
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;

            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    RaisePropertyChanged(nameof(CurrentPageViewModel));
                }
            }
        }

        public ICommand LogInCommand
        {
            get
            {
                if (_logInCommand == null)
                {
                    _logInCommand = new RelayCommand(
                        p => LogIn(),
                        p => LogInValidation());
                }
                return _logInCommand;
            }
        }

        #endregion

        #region methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (CurrentPageViewModel != viewModel)
                CurrentPageViewModel = viewModel;
        }

        private bool ValidateChangeViewModel(IPageViewModel viewModel)
        {
            return true;//!(CurrentPageViewModel is LoggingViewModel && !(page is LoggingViewModel));
        }

        private void LogIn()
        {
            //TODO LogIn to the email, or should be in model?
            // Check if login and password for chosen email are good
            // Go to Received Page and receive email
            MessageBox.Show(((LoggingViewModel)PageViewModels[(int)Enum.PageName.Logging]).Login);
            MessageBox.Show(((int)((LoggingViewModel)PageViewModels[(int)Enum.PageName.Logging]).EmailMode).ToString());
            // _applicationViewModel.ChangeViewModel(_applicationViewModel.PageViewModels[(int)Enum.PageName.Received]);
        }

        private bool LogInValidation()
        {
            // TODO Validation of logging
            // Check if all fields are completed
            return true;
        }

        #endregion
    }
}
