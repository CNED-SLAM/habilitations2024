using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace habilitations2024.model.Tests
{
    [TestClass()]
    public class AdminTests
    {
        private const string nom = "Dupont";
        private const string prenom = "Alain";
        private const string pwd = "pwddupontalain";
        private static readonly Admin admin = new Admin(nom, prenom, pwd);

        [TestMethod()]
        public void AdminTest()
        {
            Assert.AreEqual(nom, admin.Nom, "devrait réussir : nom valorisé");
            Assert.AreEqual(prenom, admin.Prenom, "devrait réussir : prenom valorisé");
            Assert.AreEqual(pwd, admin.Pwd, "devrait réussir : pwd valorisé");
        }
    }
}