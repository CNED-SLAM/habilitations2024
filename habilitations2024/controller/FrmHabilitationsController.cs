using habilitations2024.dal;
using habilitations2024.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace habilitations2024.controller
{
    /// <summary>
    /// Contrôleur de FrmHabilitations
    /// </summary>
    public class FrmHabilitationsController
    {
        /// <summary>
        /// objet d'accès aux opérations possibles sur Developpeur
        /// </summary>
        private readonly DeveloppeurAccess developpeurAccess;
        /// <summary>
        /// objet d'accès aux opérations possible sur Profil
        /// </summary>
        private readonly ProfilAccess profilAccess;

        /// <summary>
        /// Récupère les acces aux données
        /// </summary>
        public FrmHabilitationsController()
        {
            developpeurAccess = new DeveloppeurAccess();
            profilAccess = new ProfilAccess();
        }

        /// <summary>
        /// Récupère et retourne les infos des développeurs
        /// </summary>
        /// <returns>liste des développeurs</returns>
        public List<Developpeur> GetLesDeveloppeurs()
        {
            return developpeurAccess.GetLesDeveloppeurs();
        }

        /// <summary>
        /// Récupère et retourne les infos des profils
        /// </summary>
        /// <returns>liste des profils</returns>
        public List<Profil> GetLesProfils()
        {
            return profilAccess.GetLesProfils();
        }

        /// <summary>
        /// Demande de suppression d'un développeur
        /// </summary>
        /// <param name="developpeur">objet developpeur à supprimer</param>
        public void DelDeveloppeur(Developpeur developpeur)
        {
            developpeurAccess.DelDepveloppeur(developpeur);
        }

        /// <summary>
        /// Demande d'ajout d'un développeur
        /// </summary>
        /// <param name="developpeur">objet developpeur à ajouter</param>
        public void AddDeveloppeur(Developpeur developpeur)
        {
            developpeurAccess.AddDeveloppeur(developpeur);
        }

        /// <summary>
        /// Demande de modification d'un développeur
        /// </summary>
        /// <param name="developpeur">objet developpeur à modifier</param>
        public void UpdateDeveloppeur(Developpeur developpeur)
        {
            developpeurAccess.UpdateDeveloppeur(developpeur);
        }

        /// <summary>
        /// Demande de changement de pwd
        /// </summary>
        /// <param name="developpeur">objet developpeur avec nouveau pwd</param>
        public void UpdatePwd(Developpeur developpeur)
        {
            developpeurAccess.UpdatePwd(developpeur);
        }

        /// <summary>
        /// Demande de suppression d'un profil
        /// </summary>
        /// <param name="profil">objet profil à supprimer</param>
        public void DelProfil(Profil profil)
        {
            profilAccess.DelProfil(profil);
        }
        /// <summary>
        /// Demande d'ajout d'un profil 
        /// </summary>
        /// <param name="profil"></param>
        public void AddProfil(Profil profil)
        {
            profilAccess.AddProfil(profil);
        }

        /// <summary>
        /// Conrtôle si le pwd respecte les règles :
        /// au moins une minuscule, une majuscule, un chiffre, un caractère spécial, pas d'espace
        /// et longueur entre 8 et 30 caractères
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool PwdFort(string pwd)
        {
            if (pwd.Length < 8 || pwd.Length > 30)
                return false;
            // Définir un timeout raisonnable
            TimeSpan timeout = TimeSpan.FromMilliseconds(100);
            // description en une fois des règles à respecter :
            // \p{Ll} → Correspond aux lettres minuscules Unicode, y compris les caractères accentués (é, ô, à...).
            // \p{Lu} → Correspond aux lettres majuscules Unicode, y compris les caractères accentués (É, Ô, À...).
            // \d → Correspond à un chiffre Unicode.
            // \W → Correspond à un caractère non alphanumérique Unicode.
            // _ → Correspond au caractère souligné (_).
            // (?!.*\s) → Ne doit pas contenir d'espace.
            // {8,30} → Longueur entre 8 et 30 caractères.
            string pattern = @"^(?=.*[\p{Ll}])(?=.*[\p{Lu}])(?=.*\d)(?=.*[\W_])(?!.*\s).{8,30}$";
            try
            {
                return Regex.IsMatch(pwd, pattern, RegexOptions.None, timeout);
            }
            catch (RegexMatchTimeoutException)
            {
                // Échec si l'évaluation prend trop de temps
                return false;
            }
        }

    }
}
