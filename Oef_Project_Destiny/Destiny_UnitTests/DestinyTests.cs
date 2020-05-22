using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Destiny_Models;
using Destiny_DAL;

namespace Destiny_UnitTests
{
    [TestClass]
    public class DestinyTests
    {
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

        [TestMethod]
        public void ToevoegenLocatie_WhenCalled_ReturnNUllWhenMapIsEmpty()
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


    }
}
