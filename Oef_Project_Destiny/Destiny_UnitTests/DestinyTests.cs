using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Destiny_Models;
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

            foutmelding = Valideer.ValideerAccountGegevens("blabla", "wawawa", "gillesekegui@hotmail.com", "blabla");

            ///assert
            Assert.AreEqual("", foutmelding);
        }


        [TestMethod]
        public void ValideerAccountGegevens_WhenCalled_ReturnsNotEmptyStringOnGebruikersNaam()
        {
            ///arrange
            string foutmelding = "";

            ///act

            foutmelding = Valideer.ValideerAccountGegevens("blabla", "", "gillesekegui@hotmail.com", "blabla");

            ///assert
            Assert.IsTrue(foutmelding != "");
        }
    }
}
