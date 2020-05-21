using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_Models
{
    public static class ValidateAccount
    {
        public static string ValideerAccountGegevens(string wachtwoord, string gebruikersnaam, string mail, string herhaalWachtwoord)
        {
            string foutmeldingen = "";
            //Lengte van ingegeven gebruikersnaam checken
            if (gebruikersnaam.Length < 3)
            {
                foutmeldingen += "Gebruikersnaam moet langer zijn dan 3 characters" + Environment.NewLine;
            }
            //checken als email-adres valide
            if (!new EmailAddressAttribute().IsValid(mail))
            {
                foutmeldingen += "Het opgegeven mailadres bestaat niet!" + Environment.NewLine;
            }
            //Wachtwoordvalidatie
            if (wachtwoord.Length < 6)
            {
                foutmeldingen += "Lengte van wachtwoord moet langer zijn dan 6 characters!" + Environment.NewLine;
            }
            if (wachtwoord != herhaalWachtwoord)
            {
                foutmeldingen += "Wachtwoorden komen niet overeen!" + Environment.NewLine;
            }
            return foutmeldingen;
        }
    }
}
