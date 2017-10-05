using System.Text.RegularExpressions;

namespace MailClient.Security
{
    internal static class EmailValidator
    {
        private static string _emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";     
        public static bool Validate(string email)
        {
            return Regex.IsMatch (email, _emailPattern);
        }
    }
}
