namespace MailClient.Model
{
    public class Mail
    {
        #region properties

        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        #endregion

        #region constructors

        public Mail() { }

        public Mail(string sender, string receiver, string title, string message)
        {
            Sender = sender;
            Receiver = receiver;
            Title = title;
            Message = message;
        }

        #endregion
    }
}
