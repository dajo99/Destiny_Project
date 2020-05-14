using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
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
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    var query = destinyEntities.Accounts;


                    return query.ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                fileOperations.Foutloggen(ex);
                return null;
            }            

		}
       
        public static List<Character> OphalenCharacterOptiesVoorAanmaken()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                var query = destinyEntities.Characters;
                return query.ToList();
            }
        }
        public static List<CharacterKlasse> OphalenCharacterKlasseVoorAanmaken()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                var query = destinyEntities.CharacterKlasses.Include(x => x.CharacterSubklasses);
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
        public static int IdOphalenAccount(string name)
        {
            using (DestinyEntities entity = new DestinyEntities())
            {
                var query = entity.Accounts.Where(x => x.Accountnaam == name).Select(x => x.id);
                return query.FirstOrDefault();
            }
        }
        public static int CharacterToevoegen(Character aanmaking)
        {

            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Characters.Add(aanmaking);
                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                fileOperations.Foutloggen(ex);
                return 0;
            }
                
            
           
              
        }
        

    }
}
