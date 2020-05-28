using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL
{
    public partial class Account : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Accountnaam" && Accountnaam.Length < 3)
                {
                    return "Gebruikersnaam moet langer zijn dan 3 characters";
                }
                if (columnName == "Mail" && !new EmailAddressAttribute().IsValid(Mail))
                {
                    return "Het opgegeven mailadres bestaat niet!";
                }
                if (columnName == "Wachtwoord" && Wachtwoord.Length < 6)
                {
                    return "Lengte van wachtwoord moet minstens 6 tekens lang zijn!";
                }
                return "";
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Account account &&
                   Accountnaam == account.Accountnaam;
        }

        public override int GetHashCode()
        {
            return -332573477 + EqualityComparer<string>.Default.GetHashCode(Accountnaam);
        }
    }
}
