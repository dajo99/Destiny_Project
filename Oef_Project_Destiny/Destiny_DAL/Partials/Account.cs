using Destiny_Models;
using System;
using System.Collections.Generic;
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
                if (columnName == "Accountnaam" && string.IsNullOrWhiteSpace(Accountnaam))
                {
                    return "Accountnaam is een verplicht veld!";
                }
                if (columnName == "Mail" && string.IsNullOrWhiteSpace(Mail))
                {
                    return "Mail is een verplicht veld!";
                }
                if (columnName == "Wachtwoord" && string.IsNullOrWhiteSpace(Wachtwoord))
                {
                    return "Wachtwoord is een verplicht veld!";
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
