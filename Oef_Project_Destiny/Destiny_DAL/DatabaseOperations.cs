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
                fileOperations.Foutloggen(ex);
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

        public static List<CharacterCustomization> OphalenCharacterOptiesVoorAanmaken()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                var query = destinyEntities.CharacterCustomizations.Include("Ras");
                return query.ToList();
            }
        }
        public static List<CharacterKlasse> OphalenCharacterKlasseVoorAanmaken()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                var query = destinyEntities.CharacterKlasses;
                return query.ToList();
            }
        }

        public static List<CharacterSubklasse> OphalenCharacterSubklasseVoorAanmaken(int id)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                var query = destinyEntities.CharacterSubklasses.Where(x => x.CharacterKlasseId == id);
                return query.ToList();
            }
        }

        public static List<Ras> OphalenRasVoorAanmaken()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                var query = destinyEntities.Ras1;
                return query.ToList();
            }
        }
        public static int InstellingenVanAanmakenOpslaanInDatabase(CharacterCustomization aanmaking)
        {
           
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.CharacterCustomizations.Add(aanmaking);
                    return destinyEntities.SaveChanges();
                }
           
            
        }


    }
}
