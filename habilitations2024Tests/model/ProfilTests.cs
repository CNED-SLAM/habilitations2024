using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace habilitations2024.model.Tests
{
    [TestClass()]
    public class ProfilTests
    {
        private const int id = 6;
        private const string nom = "responsable";
        private readonly Profil profil = new Profil(id, nom);

        [TestMethod()]
        public void ProfilTest()
        {
            Assert.AreEqual(id, profil.Idprofil, "devrait réussir : id valorisé");
            Assert.AreEqual(nom, profil.Nom, "devrait réussir : nom valorisé");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(nom, profil.ToString(), "devrait réussir : nom retourné");
        }
    }
}