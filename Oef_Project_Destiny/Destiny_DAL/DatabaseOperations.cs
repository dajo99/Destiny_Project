using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Security.Cryptography;

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
                fileOperations.Foutloggen(ex);
                return 0;
            }
        }

        public static List<Account> CheckLogin(Account a)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Accounts
                    .Where(x => x.Accountnaam != a.Accountnaam)
                    .ToList();
            }

        }

        public static Account OphalenAccount(string accountnaam)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    return destinyEntities.Accounts
                        .Where(x => x.Accountnaam.Contains(accountnaam)).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                fileOperations.Foutloggen(ex);
                return null;
            }

        }

        public static int WijzigenAccount(Account a)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(a).State = EntityState.Modified;
                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                fileOperations.Foutloggen(ex);
                return 0;
            }
        }

        public static int VerwijderenAccount(Account a)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {

                    destinyEntities.Entry(a).State = EntityState.Deleted;
                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                fileOperations.Foutloggen(ex);
                return 0;
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


        public static List<Map> OphalenWerelden()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Maps
                    .OrderBy(x => x.Wereld)
                    .ToList();
            }
        }


        public static List<Locatie> OphalenLocaties(int wereld)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Locaties
                    .Where(x => x.MapId == wereld)
                    .OrderBy(x => x.id)
                    .ToList();
            }
        }


        public static int AanpassenLocatie(Locatie locatie)
        {
            try
            {
                using(DestinyEntities destinyEntities = new DestinyEntities())
                {

                    destinyEntities.Entry(locatie).State = EntityState.Modified;
                    return destinyEntities.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                fileOperations.Foutloggen(ex);
                return 0;
            }
        }


        public static int ToevoegenLocatie(Locatie locatie)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {

                    destinyEntities.Locaties.Add(locatie);
                    return destinyEntities.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                fileOperations.Foutloggen(ex);
                return 0;
            }
        }

        public static int VerwijderenLocatie(Locatie locatie)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {

                    destinyEntities.Entry(locatie).State = EntityState.Deleted;
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
