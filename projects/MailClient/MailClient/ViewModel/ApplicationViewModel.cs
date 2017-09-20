using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Interface;
using System;
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
        
        #endregion

        #region constructors

        public ApplicationViewModel()
        {
            IPageViewModel logging = new LoggingViewModel();
            // subsribing to event of changing the page 
            LoggingViewModel.ChangePage += ChangeViewModelEventHandler;
            _pageViewModels.Add(logging);
            _pageViewModels.Add(new ReceivedViewModel());
            // error is here, two loggingViewModels are created

            //TODO add new viewmodels

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
            if (CurrentPageViewModel != viewModel)
                CurrentPageViewModel = viewModel;
        }

        private bool ValidateChangeViewModel(IPageViewModel viewModel)
        {
            return true;//!(CurrentPageViewModel is LoggingViewModel && !(page is LoggingViewModel));
        }       

        private void ChangeViewModelEventHandler(object sender, EventArgs pageNumber)
        {
            ChangeViewModel(FindPageWithPageNumber(((PageNumberEventArg)pageNumber).PageNumber));
        }

        #endregion
    }
}
