using MailClient.Enum;
using MailClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.ViewModel
{
    class PageViewModels 
    {
        private IList<IPageViewModel> _pages { get; set; } = new List<IPageViewModel>();

        public PageViewModels()
        {
            _pages.Add(new LoggingViewModel());
            _pages.Add(new ReceivedViewModel());
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

            // Its not observable collection, not seen changes in the GUI
            }
        }

        public IPageViewModel FindPage(PageNumber pageNumber)
        {
            foreach (var page in _pages)
            {
                if (page.PageNumber == pageNumber)
                    return page;
            }
            return null;
        }
    }
}
