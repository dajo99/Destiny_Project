using System.Windows.Media;
using Destiny_DAL;

namespace Destiny_Models
{
    public static class User
    {
        public static Account Acc { get; set; }
        public static Character Character { get; set; }

        public static string DomeinNaarLowerCase(string mail)
        {
            int positie = mail.IndexOf('@');
            return mail.Substring(0, positie + 1) + mail.Substring(positie + 1).ToLower();
        }
    }
    
}
