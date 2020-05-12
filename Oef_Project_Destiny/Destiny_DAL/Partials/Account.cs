using Destiny_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL.Partials
{
    public partial class Account : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Accountnaam" && string.IsNullOrWhiteSpace(Accountnaam))
                {

                }
            }
        }
    }
}
