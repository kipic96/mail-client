using System;
using System.IO;
using MailClient.Extension;


namespace MailClient.Model.UserManager
{
    public static class UserManager
    {
        private readonly static string _userManagerFile = "userManager.txt";

        public static void RememberUser(User user)
        {
            // TODO Check if it works
            ForgetUser();
            using (var streamWriter = new StreamWriter(_userManagerFile))
            {
                string userPassword = user.Password.MakeItString();

                streamWriter.WriteLine(user.Login);
                streamWriter.WriteLine(userPassword);
                streamWriter.WriteLine((int)user.EmailMode);
            }
        }

        public static User LoadRemeberedUser()
        {
            // TODO check if it works
            try
            {
                using (var streamReader = new StreamReader(_userManagerFile))
                {
                    var user = new User();
                    string userLogin = streamReader.ReadLine();
                    if (userLogin != null)
                    {
                        string userPassword = streamReader.ReadLine();
                        if (userPassword != null)
                        {
                            int userMailType;
                            if (int.TryParse(streamReader.ReadLine(), out userMailType))
                            {
                                user.Login = userLogin;
                                user.Password = userPassword.ToSecureString();
                                user.EmailMode = (Enum.EmailMode)userMailType;
                                return user;
                            }
                        }
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void ForgetUser()
        {
            File.WriteAllText(_userManagerFile, string.Empty);
        }        
    }
}
