﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Destiny_DAL
{
    public static class DatabaseOperations
    {
        //----------------------
        //Usercontrole Account
        //----------------------
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
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        public static List<Account> OphalenAccountViaAccount(Account a)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Accounts
                    .Where(x => x.Accountnaam != a.Accountnaam)
                    .ToList();
            }

        }

        public static Account OphalenAccountViaAccountnaam(string accountnaam)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    return destinyEntities.Accounts
                        .Where(x => x.Accountnaam == accountnaam)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
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
                FileOperations.Foutloggen(ex);
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
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        //----------------------
        //Usercontrole Character
        //----------------------
        
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

                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        public static List<Character> CharactersOphalenViaAccountId(int id)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                var query = destinyEntities.Characters.Include(x => x.Ras)
                    .Include(x => x.CharacterKlasse)
                    .Include(x => x.CharacterSubklasse)
                    .Where(x => x.AccountId == id)
                    .OrderBy(x => x.Level);

                return query.ToList();

            }
        }

        
        public static int CharacterVerwijderen(Character verwijderen)
        {

            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(verwijderen).State = EntityState.Deleted;
                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        public static int CharacterUpdaten(Character update)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(update).State = EntityState.Modified;
                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        //----------------------
        //Usercontrole Location
        //----------------------
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
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {

                    destinyEntities.Entry(locatie).State = EntityState.Modified;
                    return destinyEntities.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
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
                FileOperations.Foutloggen(ex);
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
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        //---------------------------
        //Usercontrole SpecialItems
        //---------------------------
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

        public static int ToevoegenSpecialItem(Item i, SpecialItem si)
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
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }

        }

        public static List<Item> OphalenItems()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Items
                    .Include(x => x.SpecialItem)
                    .OrderBy(x => x.id)
                    .ToList();
            }
        }

        public static int AanpassenSpecialItems(Item i, SpecialItem si)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    var special = destinyEntities.SpecialItems.Where(x => x.id == si.id).SingleOrDefault();
                    destinyEntities.Entry(special).CurrentValues.SetValues(si);

                    var item = destinyEntities.Items.Where(x => x.id == i.id).SingleOrDefault();
                    destinyEntities.Entry(item).CurrentValues.SetValues(i);

                    //destinyEntities.Entry(i).State = EntityState.Modified;
                    //destinyEntities.Entry(si).State = EntityState.Modified;

                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
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
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        //---------------------------
        //Usercontrole Armor
        //---------------------------
        public static List<Armor> OphalenArmorViaArmorSlotEnZeldzaamheid(string naam, string armorslot, string zeldzaamheid)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Armors
                    .Include(x => x.Item)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam) && x.Item.Zeldzaamheid == zeldzaamheid)
                    .Where(x => x.ArmorSlot == armorslot)
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }

        public static List<Armor> OphalenArmorViaArmorslot(string naam, string armorslot)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Armors
                    .Include(x => x.Item)
                    .Where(x => x.ArmorSlot == armorslot)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam))
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }

        public static List<Armor> OphalenArmorViaZeldzaamheid(string naam, string zeldzaamheid)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Armors
                    .Include(x => x.Item)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam) && x.Item.Zeldzaamheid == zeldzaamheid)
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }

        public static List<Armor> OphalenArmorViaNaam(string naam)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Armors
                    .Include(x => x.Item)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam))
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }



        public static int ToevoegenArmor(Item i, Armor a)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(i).State = EntityState.Added;
                    destinyEntities.Entry(a).State = EntityState.Added;

                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        public static int AanpassenArmor(Armor a, Item i)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    var armor = destinyEntities.Armors.Where(x => x.id == a.id).SingleOrDefault();
                    destinyEntities.Entry(armor).CurrentValues.SetValues(a);

                    var item = destinyEntities.Items.Where(x => x.id == i.id).SingleOrDefault();
                    destinyEntities.Entry(item).CurrentValues.SetValues(i);

                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }

        }

        public static int VerwijderenArmor(Item i, Armor a)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(a).State = EntityState.Deleted;
                    destinyEntities.Entry(i).State = EntityState.Deleted;
                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }

        }

        //---------------------------
        //Usercontrole Weapons
        //---------------------------
        public static List<Wapenklasse> OphalenWapenCategorie()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Wapenklasses
                    .ToList();
            }
        }

        public static List<Damagetype> OphalenWapenDamagetype()
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Damagetypes
                    .ToList();
            }
        }

        public static List<Wapen> OphalenWapensViaNaam(string naam)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Wapens
                    .Include(x => x.Item)
                    .Include(x => x.Wapenklasse)
                    .Include(x => x.Damagetype)
                    .Where(x => x.Wapenklasse.id == x.WapenklasseId)
                    .Where(x => x.DamagetypeId == x.Damagetype.id)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam))
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }

        public static List<Wapen> OphalenWapensViaCategorieEnZeldzaamheid(string naam, int wapenklasseId, string zeldzaamheid)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Wapens
                    .Include(x => x.Item)
                    .Include(x => x.Damagetype)
                    .Include(x => x.Wapenklasse)
                    .Where(x => x.Wapenklasse.id == x.WapenklasseId && x.Wapenklasse.id == wapenklasseId)
                    .Where(x => x.DamagetypeId == x.Damagetype.id)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam) && x.Item.Zeldzaamheid == zeldzaamheid)
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }
        public static List<Wapen> OphalenWapensViaCategorie(string naam, int wapenklasseId)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Wapens
                    .Include(x => x.Item)
                    .Include(x => x.Damagetype)
                    .Include(x => x.Wapenklasse)
                    .Where(x => x.Wapenklasse.id == x.WapenklasseId && x.Wapenklasse.id == wapenklasseId)
                    .Where(x => x.DamagetypeId == x.Damagetype.id)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam))
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }


        public static List<Wapen> OphalenWapensViaZeldzaamheid(string naam, string zeldzaamheid)
        {
            using (DestinyEntities destinyEntities = new DestinyEntities())
            {
                return destinyEntities.Wapens
                    .Include(x => x.Item)
                    .Include(x => x.Damagetype)
                    .Include(x => x.Wapenklasse)
                    .Where(x => x.Wapenklasse.id == x.WapenklasseId)
                    .Where(x => x.DamagetypeId == x.Damagetype.id)
                    .Where(x => x.Item.id == x.id)
                    .Where(x => x.Item.Naam.Contains(naam) && x.Item.Zeldzaamheid == zeldzaamheid)
                    .OrderBy(x => x.Item.Naam)
                    .ToList();
            }
        }

        public static int ToevoegenWapen(Item i, Wapen w)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(i).State = EntityState.Added;
                    destinyEntities.Entry(w).State = EntityState.Added;

                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }

        }

        public static int VerwijderenWapen(Item i, Wapen w)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    destinyEntities.Entry(w).State = EntityState.Deleted;
                    destinyEntities.Entry(i).State = EntityState.Deleted;
                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }

        }

        public static int AanpassenWapens(Item i, Wapen w)
        {
            try
            {
                using (DestinyEntities destinyEntities = new DestinyEntities())
                {
                    var weapon = destinyEntities.Wapens.Where(x => x.id == w.id).SingleOrDefault();
                    destinyEntities.Entry(weapon).CurrentValues.SetValues(w);

                    var item = destinyEntities.Items.Where(x => x.id == w.id).SingleOrDefault();
                    destinyEntities.Entry(item).CurrentValues.SetValues(i);

                    return destinyEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }
    }
}
