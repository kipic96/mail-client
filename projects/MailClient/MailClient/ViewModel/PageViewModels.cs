using MailClient.ViewModel.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MailClient.ViewModel
{
    public class PageViewModels 
    {
        private IList<IPageViewModel> _pages { get; set; } = new ObservableCollection<IPageViewModel>();

        public PageViewModels()
        {
            _pages.Add(new LoggingViewModel());
            _pages.Add(new ReceivedViewModel());
            _pages.Add(new SendViewModel());
            // TODO here add new viewModels
        }

        public IList<IPageViewModel> Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                _pages = value;                
                 // Its observable collection, but dont know if seen changes in the GUI
            }
        }

        public IPageViewModel FindPage(Enum.PageNumber pageNumber)
        {
            foreach (var page in _pages)
            {
                if (page.PageNumber == pageNumber)
                    return page;
            }
            return null;
        }

        public void Clear()
        {
            foreach (var page in Pages)
            {
                page.Clear();
            }
        }
    }
}
