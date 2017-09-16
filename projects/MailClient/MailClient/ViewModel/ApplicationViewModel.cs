using System.Collections.Generic;
using System.Linq;
using MailClient.HelperClass;
using MailClient.Interface;
using System.Windows.Input;

namespace MailClient.ViewModel
{
    class ApplicationViewModel : BindableClass
    {
        #region Fields

        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private IList<IPageViewModel> _pageViewModels;

        #endregion

        public ApplicationViewModel()
        {
            // Add available pages
            PageViewModels.Add(new LoggingViewModel());
            PageViewModels.Add(new ReceivedViewModel());

            //TODO add new viewmodels

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        page => ChangeViewModel((IPageViewModel)page),
                        page => ValidateChangeViewModel((IPageViewModel)page));
                }

                return _changePageCommand;
            }
        }

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
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private bool ValidateChangeViewModel(IPageViewModel page)
        {
            return !(CurrentPageViewModel is LoggingViewModel && !(page is LoggingViewModel));
        }

        #endregion
    }
}
