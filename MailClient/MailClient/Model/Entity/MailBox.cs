using MailClient.Model.Connection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailClient.Model.Entity
{
    public class MailBox 
    {
        private BaseMechanism _mailMechanism;

        public static readonly MailBox EmptyMailBox =
            new MailBoxNull();

        private MailBox() { }

        public MailBox(User user)
        {
            _mailMechanism = new MailMechanism(user, ConnectionFactory.Create(user.EmailMode));
        }        

        public virtual List<string> Send(Mail mail, string logName)
        {
            var sentEmailAdresses = new List<string>();
            if (mail.SendMultipleEmails)
            {
                sentEmailAdresses = SendMultiple(mail, logName).ToList();
            }
            else
            {
                var sentEmailAdress = _mailMechanism.Send(mail, logName);
                sentEmailAdresses.Add(sentEmailAdress);
            }
            return sentEmailAdresses;
        }

        public virtual IEnumerable<Mail> Receive()
        {
            return _mailMechanism.Receive();
        }

        public virtual bool ValidateEmail(string email)
        {
            return Validator.EmailValidator.Validate(email);
        }

        public virtual bool Authenticate()
        {
            return _mailMechanism.Authenticate();
        }

        private List<string> SendMultiple(Mail mail, string logName)
        {
            List<string> toAdresses = GetMultipleEmailAdresses(mail);
            var sentEmailAdresses = new List<string>();
            foreach (var toAdrress in toAdresses)
            {
                var newMail = mail;
                newMail.To = toAdrress;
                //if (ValidateEmail(newMail.To))
                //{
                var sentEmailAdress = _mailMechanism.Send(newMail, logName);
                sentEmailAdresses.Add(sentEmailAdress);
                //}
                //else
                //{
                //    var notSentEmailAdress = newMail.To + " --> NOT SENT, WRONG EMAIL FORMAT";
                //    sentEmailAdresses.Add(notSentEmailAdress);
                //}
            }
            return sentEmailAdresses;
        }

        private List<string> GetMultipleEmailAdresses(Mail mail)
        {
            var toAdresses = mail.To.Split(' ').ToList();
            toAdresses.ForEach(toAdrress =>
            {
                toAdrress.Replace(" ", "");
                toAdrress.Replace(",", "");
                toAdrress.Replace(Environment.NewLine, "");
            });
            return toAdresses;
        }

        private class MailBoxNull : MailBox
        {
            public MailBoxNull() { }

            public MailBoxNull(User user) : base(user) { }

            public override List<string> Send(Mail mail, string logName) { return new List<string>(); }

            public override IEnumerable<Mail> Receive() { return new List<Mail>(); }

            public override bool Authenticate() { return false; }
        }
    }
}
