using System;

namespace MailClient.HelperClass
{
    class PageNumberEventArg : EventArgs
    {
        public Enum.PageNumber PageNumber { get; set; }

        public PageNumberEventArg(Enum.PageNumber pageNumber)
        {
            PageNumber = pageNumber;
        }
    }
}
