using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.IO;

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

        public static List<SpecialItem> OphalenSpecialItemsViaNaam(string naam)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.SpecialItems
                    .Include(x => x.Item)
                    .Include(x => x.SpecialItemCategorie)
                    .Where(x => x.SpecialItemCategorie.id == x.CategorieId)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam))
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }

        public static List<SpecialItem> OphalenSpecialItemsViaCategorieEnZeldzaamheid(string naam, int categorieId, string zeldzaamheid)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.SpecialItems
                    .Include(x => x.Item)
                    .Include(x => x.SpecialItemCategorie)
                    .Where(x => x.SpecialItemCategorie.id == x.CategorieId && x.SpecialItemCategorie.id == categorieId)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam) && x.Item.Zeldzaamheid == zeldzaamheid)
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }
        public static List<SpecialItem> OphalenSpecialItemsViaCategorie(string naam, int categorieId)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.SpecialItems
                    .Include(x => x.Item)
                    .Include(x => x.SpecialItemCategorie)
                    .Where(x => x.SpecialItemCategorie.id == x.CategorieId && x.SpecialItemCategorie.id == categorieId)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam))
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }
        public static List<SpecialItem> OphalenSpecialItemsViaZeldzaamheid(string naam, string zeldzaamheid)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.SpecialItems
                    .Include(x => x.Item)
                    .Include(x => x.SpecialItemCategorie)
                    .Where(x => x.SpecialItemCategorie.id == x.CategorieId)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam) && x.Item.Zeldzaamheid == zeldzaamheid)
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }

        public static List<SpecialItemCategorie> OphalenSpecialItemCategories()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.SpecialItemCategories
                    .OrderBy(x => x.id)
                    .ToList();
            }
        }

        public static int ToevoegenItem(Item i, SpecialItem si)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(i).State = EntityState.Added;
                    destinyEntities.Entry(si).State = EntityState.Added;

                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex )
            {
                fileOperations.Foutloggen(ex);
                return 0;
            }
            
        }

        public static List<Item> OphalenItems()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Items
                    .Include(x=> x.SpecialItem)
                    .OrderBy(x => x.id)
                    .ToList();
            }
        }

        public static int AanpassenSpecialItems(SpecialItem si)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(si).State = EntityState.Modified;
                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                fileOperations.Foutloggen(ex);
                return 0;
            }

        }

        public static int AanpassenItems(Item i)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(i).State = EntityState.Modified;
                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                fileOperations.Foutloggen(ex);
                return 0;
            }

        }
        public static int VerwijderenSpecialItem(Item i, SpecialItem si)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(si).State = EntityState.Deleted;
                    destinyEntities.Entry(i).State = EntityState.Deleted;
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
