using System.Security;

namespace MailClient.Extension
{
    public static class ExtensionString
    {
        public static SecureString ToSecureString(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;
            else
            {
                var result = new SecureString();
                foreach (char c in source.ToCharArray())
                    result.AppendChar(c);
                return result;
            }
        }
    }
}
