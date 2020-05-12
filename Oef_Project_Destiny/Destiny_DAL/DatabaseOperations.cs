using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Destiny_DAL
{
    public static class DatabaseOperations
    {
        public static int ToevoegenAccount(Account a)
        {
			try
			{
				using (DestinyEntities destinyEntities = new DestinyEntities())
				{
					destinyEntities.Accounts.Add(a);
					return destinyEntities.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				/*FileOperations.Foutloggen(ex);*/
				return 0;
			}
        }

		public static List<Account> CheckLogin()
		{
			using (DestinyEntities destinyEntities = new DestinyEntities())
			{
				var query = destinyEntities.Accounts;
					

				return query.ToList();

			}
		}

		

	}
}
