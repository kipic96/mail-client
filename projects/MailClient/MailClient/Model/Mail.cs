using System.Collections;

namespace MailClient.Model
{
    public class Mail
    {
        #region properties

        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        #endregion

        #region constructors

        public Mail() { }

        public Mail(string from, string to, string subject, string message)
        {
            From = from;
            To = to;
            Subject = subject;
            Message = message;
        }

        #endregion

        #region methods

        public static Mail Parse(AE.Net.Mail.MailMessage mailMessage)
        {
            var mail = new Mail();
            mail.From = mailMessage.From.ToString();
            IList mailAdressesTo = mailMessage.To as IList;
            mail.To = mailAdressesTo[0].ToString();
            mail.Subject = mailMessage.Subject;
            mail.Message = mailMessage.Body;
            return mail;
        }

        #endregion
    }
}
