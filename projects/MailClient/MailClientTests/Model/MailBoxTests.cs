using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailClient.Model;
using System.Security;
using System.Net;
using System;

namespace MailClient.Tests
{
    [TestClass()]
    public class MailBoxTests
    {
        SecureString passwordBad = new NetworkCredential("", "badPassword").SecurePassword;
        SecureString passwordGood = new NetworkCredential("", "testing1").SecurePassword;

        string mailBad = "testingemail10002";
        string mailGood = "testingemail10002@gmail.com";

        string titleGood = "test1";
        string titleBad = "testingBad";
        string messageGood = "testing message";

        [TestMethod()]
        public void MailBoxTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SendTest()
        {            
            MailBox mailBox = new MailBox(new User(mailGood, passwordGood, Enum.EmailMode.Gmail));
            Mail mail = new Mail(mailGood, mailGood, titleGood, messageGood);
            mailBox.Send(mail);
            Assert.AreEqual(1,1);


            
        }

        [TestMethod()]
        public void ReceiveTest()
        {
            Assert.Fail();
        }
    }
}