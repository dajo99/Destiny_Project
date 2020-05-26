using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Destiny_Models;
using Destiny_DAL;
using System.Linq;
using System.Collections.Generic;

namespace Destiny_UnitTests
{
    [TestClass]
    public class DestinyTests
    {
        //Author = Gilles
        [TestMethod]
        public void ValideerAccountGegevens_WhenCalled_ReturnsEmptyString()
        {
            ///arrange
            string foutmelding = "";

            ///act

            foutmelding = ValidateAccount.ValideerAccountGegevens("blabla", "wawawa", "gillesekegui@hotmail.com", "blabla");

            ///assert
            Assert.AreEqual("", foutmelding);
        }

        //Author = Gilles
        [TestMethod]
        public void ValideerAccountGegevens_WhenCalled_ReturnsNotEmptyStringOnGebruikersNaam()
        {
            ///arrange
            string foutmelding = "";

            ///act

            foutmelding = ValidateAccount.ValideerAccountGegevens("blabla", "", "gillesekegui@hotmail.com", "blabla");

            ///assert
            Assert.IsTrue(foutmelding != "");
        }


        //Author = Gilles
        [TestMethod]
        public void CharacterToevoegen_WhenCalled_Returns0WhenPropertyHasNoValue()
        {
            ///arrange
            Character c = new Character();
            c.AccountId = 2;

            // c.CharacterKlasseId LEGE PROPERTY
            c.CharacterSubklasseId = 2;
            //c.Face LEGE PROPERTY
            c.Gender = "Man";
            c.HeadOption = "kaal";
            c.Marking = "Geen marking";
            c.Level = 5;
            c.RasId = 1;
            c.id = 5;
            ///act

            int check = DatabaseOperations.CharacterToevoegen(c);

            ///assert
            Assert.AreEqual(0, check);
        }
        //Author = Dajo
        [TestMethod]
        public void ConversieToInt_WhenCalled_ReturnsNumber()
        {
            ///arrange
            string antwoord = "5";

            ///act

            int getal = GeneralItems.ConversieToInt(antwoord);

            ///assert
            Assert.AreEqual(5,getal);
        }

        //Author = Dajo
        [TestMethod]
        public void ToevoegenLocatie_WhenCalled_ReturnZeroWhenMapIsEmpty()
        {
            ///arrange
            Locatie locatie = new Locatie();
            locatie.Naam = "Antwerpen";
            locatie.RestrictedArea = true;
            ///act
            int antwoord = DatabaseOperations.ToevoegenLocatie(locatie);

            ///assert
            Assert.IsTrue(antwoord == 0);
        }

        //Author = Dajo
        [TestMethod]
        public void ToevoegenWeapon_WhenCalled_ReturnGreaterThanZero()
        {
            ///arrange
            Item i = new Item();
            i.Naam = "AK-47";
            i.Zeldzaamheid = "Legendary";
            
            Wapen wapen = new Wapen();
            wapen.id = i.id;
            wapen.WapenklasseId = 2;
            wapen.DamagetypeId = 1;

            ///act
            int antwoord = DatabaseOperations.ToevoegenWapen(i, wapen);

            ///assert
            Assert.IsTrue(antwoord > 0);
        }


        //Author = Kevin
        [TestMethod]
        public void ToevoegenSpecialItem_WhenCalled_NamesAreEqualAndRarityAreBothExotic_ReturnZero()
        {
            ///arrange
            Item i1 = new Item();
            i1.Naam = "browsky";
            i1.Zeldzaamheid = "Exotic";

            SpecialItem si1= new SpecialItem();
            si1.id = i1.id;
            si1.CategorieId = 1;

            SpecialItem si2 = new SpecialItem();
            si2.id = i1.id;
            si2.CategorieId = 2;

            DatabaseOperations.ToevoegenSpecialItem(i1, si1);

            List<Item> items = new List<Item>() { i1 };

            ///act
            int answer = 0;
            if (!items.Contains(i1))
            {
                answer= DatabaseOperations.ToevoegenSpecialItem(i1, si2);
            }

            ///assert
            Assert.IsTrue(answer == 0);
        }

        //Author = Kevin
        [TestMethod]
        public void ToevoegenSpecialItem_WhenCalled_NamesAreNotEqualAndRarityIsNotExotic_ReturnGreaterThanZero()
        {
            ///arrange
            Item i1 = new Item();
            i1.Naam = "browski2";
            i1.Zeldzaamheid = "Legendary";

            SpecialItem si1 = new SpecialItem();
            si1.id = i1.id;
            si1.CategorieId = 1;

            Item i2 = new Item();
            i2.Naam = "browski3";
            i2.Zeldzaamheid = "Common";
            SpecialItem si2 = new SpecialItem();
            si2.id = i1.id;
            si2.CategorieId = 2;

            DatabaseOperations.ToevoegenSpecialItem(i1, si1);

            List<Item> items = new List<Item>() { i1 };
            ///act

            int answer = 0;

            if (!items.Contains(i1))
            {
                answer = DatabaseOperations.ToevoegenSpecialItem(i2, si2);
            }

            ///assert
            Assert.IsTrue(answer > 0);
        }

        //Author = Kevin
        [TestMethod]
        public void ToevoegenArmor_WhenCalled_InputOfIntellectIsEmptyString_ReturnAnwserGreaterThanZeroAndIntellectIsZero()
        {
            ///arrange
            Item i = new Item();
            i.Naam = "browski2";
            i.Zeldzaamheid = "Legendary";

            Armor a = new Armor();
            a.id = i.id;
            a.ArmorSlot = "Helmet";
            string intellect = "";
            a.Intellect = GeneralItems.ConversieToInt(intellect);

            //act
            int answer = DatabaseOperations.ToevoegenArmor(i, a);

            ///assert
            Assert.IsTrue(answer > 0 && a.Intellect == 0);
        }

        //Author = Kevin
        [TestMethod]
        public void ToevoegenArmor_WhenCalled_InputOfIntellectIsString5_ReturnAnwserGreaterThanZeroAndIntellectIs5()
        {
            ///arrange
            Item i = new Item();
            i.Naam = "browski2";
            i.Zeldzaamheid = "Legendary";

            Armor a = new Armor();
            a.id = i.id;
            a.ArmorSlot = "Helmet";
            string intellect = "5";
            a.Intellect = GeneralItems.ConversieToInt(intellect);

            //act
            int answer = DatabaseOperations.ToevoegenArmor(i, a);

            ///assert
            Assert.IsTrue(answer > 0 && a.Intellect == 5);
        }
    }
}
