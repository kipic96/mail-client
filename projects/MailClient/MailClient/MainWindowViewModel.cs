using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using MailClient.Logging;
using System.Windows.Input;
using MailClient.HelperClass;

namespace MailClient
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            // Add available pages
            IPageViewModel homePage = new LoggingViewModel();
            PageViewModels = new List<IPageViewModel>();
            PageViewModels.Add(homePage);

            // TODO: Add next Pages here

            // Set starting page
            CurrentPageViewModel = homePage;
        }


        #region Properties

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return CurrentPageViewModel;
            } 
            set
            {
                if (CurrentPageViewModel != value)
                {
                    CurrentPageViewModel = value;
                    RaisePropertyChanged(nameof(CurrentPageViewModel));
                }
            }
        }

        public IList<IPageViewModel> PageViewModels { get; }


        #endregion

        #region Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (ChangePageCommand == null)
                {
                    ChangePageCommand = new RelayCommand(
                        page => ChangeViewModel((IPageViewModel)page),
                        page => CanChangeViewModel());
                }

                return ChangePageCommand;
            }
            private set { ChangePageCommand = value; }
        }

        #endregion

        #region Methods
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        private bool CanChangeViewModel()
        {
            // TODO Change the magic string
            return (CurrentPageViewModel.PageName != "Logging");
        }
        #endregion
    }
}
