using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailClient.HelperClass;
using MailClient.Interface;

namespace MailClient.ViewModel
{
    class ReceivedViewModel : BindableClass, IPageViewModel
    {
        public string PageName { get; } = Dictionary.PageName.Received;
    }
}
