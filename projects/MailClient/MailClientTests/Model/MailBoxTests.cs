using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailClient.Model;
using System.Security;
using System.Net;
using System.Collections.Generic;

namespace MailClient.Tests
{
    [TestClass()]
    public class MailBoxTests
    {
        SecureString passwordBad = new NetworkCredential("", "badPassword").SecurePassword;
        SecureString passwordGood = new NetworkCredential("", "testing1").SecurePassword;

        string mailBad = "testingemail10002";
        string mailGmail = "testingemail10002@gmail.com";
        string mailInteria = "testingemail10002@interia.pl";
        string mailO2 = "testingemail10002@o2.pl";

        string titleGood = "test1";
        string titleBad = "testingBad";
        string messageGood = "testing message";


        [TestMethod()]
        public void ReceiveTestGmail()
        {
            MailBox mailBox = new MailBox(new User(mailGmail, passwordGood, Enum.EmailMode.Gmail));
            Assert.AreNotEqual(0, ((List<Mail>)mailBox.Receive()).Count);
        }

        [TestMethod()]
        public void ReceiveTestO2()
        {
            MailBox mailBox = new MailBox(new User(mailO2, passwordGood, Enum.EmailMode.O2));
            Assert.AreNotEqual(0, ((List<Mail>)mailBox.Receive()).Count);
        }

        [TestMethod()]
        public void ReceiveTestInteria()
        {
            MailBox mailBox = new MailBox(new User(mailInteria, passwordGood, Enum.EmailMode.Interia));
            Assert.AreNotEqual(0, ((List<Mail>)mailBox.Receive()).Count);
        }
    }
}