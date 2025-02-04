using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using habilitations2024.model;

namespace habilitations2024.dal.Tests
{
    [TestClass()]
    public class ProfilAccessTests
    {
        private static readonly Access access = Access.GetInstance();
        private static readonly ProfilAccess profilAccess = new ProfilAccess();

        private static void BeginTransaction()
        {
            access.Manager.ReqControle("SET AUTOCOMMIT=0");
            access.Manager.ReqControle("START TRANSACTION");
            access.Manager.ReqControle("SET FOREIGN_KEY_CHECKS=0");
        }
        private static void EndTransaction()
        {
            access.Manager.ReqControle("ROLLBACK");
            access.Manager.ReqControle("SET FOREIGN_KEY_CHECKS=1");
        }

        [TestMethod()]
        public void ProfilAccessTest()
        {
            Assert.IsNotNull(access, "devrait réussir si connexion à la BDD correcte");
        }

        [TestMethod()]
        public void GetLesProfilsTest()
        {
            List<Profil> lesProfils = profilAccess.GetLesProfils();
            Assert.AreNotEqual(0, lesProfils.Count, "devrait réussir : au moins 1 profil dans la BDD");
        }

        [TestMethod()]
        public void AddProfilTest()
        {
            BeginTransaction();
            List<Profil> lesProfils = profilAccess.GetLesProfils();
            int nbBeforeInsert = lesProfils.Count;
            string nom = "nvnom";
            Profil profil = new Profil(0, nom);
            profilAccess.AddProfil(profil);
            lesProfils = profilAccess.GetLesProfils();
            int nbAfterInsert = lesProfils.Count;
            Profil profilAdd = lesProfils.Find(obj => obj.Nom.Equals(nom));
            Assert.IsNotNull(profilAdd, "devrait réussir : un profil ajouté");
            Assert.AreEqual(nbBeforeInsert + 1, nbAfterInsert, "devrait réussir : un profil en plus");
            EndTransaction();
        }

        [TestMethod()]
        public void DelProfilTest()
        {
            BeginTransaction();
            List<Profil> lesProfils = profilAccess.GetLesProfils();
            int nbBeforeDelete = lesProfils.Count;
            if (nbBeforeDelete > 0)
            {
                Profil profil = lesProfils[0];
                profilAccess.DelProfil(profil);
                lesProfils = profilAccess.GetLesProfils();
                Profil profilDel = lesProfils.Find(obj => obj.Idprofil.Equals(profil.Idprofil));
                Assert.IsNull(profilDel, "devrait réussir : un profil supprimé");
                int nbAfterDelete = lesProfils.Count;
                Assert.AreEqual(nbBeforeDelete - 1, nbAfterDelete, "devrait réussir : un profil en moins");
            }
            EndTransaction();
        }
    }
}