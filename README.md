# Application Habilitations
Application C# écrite sous Visual Studio 2022 Entreprise et exploitant une BDD MySQL.<br><br>

## Présentation de l'application
### Présentation du contexte
L'entreprise cliente est spécialisée dans développement de sites internet personnalisées. Elle a développé une application interne pour générer plus facilement des sites pour ses clients. Son utilisation consiste à gérer des paramétrages pour obtenir un site opérationnel (couleur de la charte, liste des pages souhaitées, fonctionnalités désirées…). Cette application est régulièrement enrichie par l'ajout de nouveaux modules et nécessite aussi la mise à jour des modules existants. Les modules sont hiérarchisés suivant le type de fichiers à générer : CSS, JavaScript, HTML, PHP. L'accès à la partie back-end de cette solution applicative doit être sécurisé. Suivant le profil des intervenants (stagiaire, designer, dev-front, dev-back), le niveau d'habilitation est différent et ne permet pas de faire les mêmes manipulations.<br>
### But de l'application
Le responsable souhaite avoir <strong>un petit utilitaire pour gérer les profils, et donc les niveaux d'habilitations, des différents développeurs</strong>.<br>
L'application Habilitations représente cet utilitaire.<br>
L'application doit permettre de :
<ul>
<li>présenter la liste des développeurs (nom, prénom, tel, mail, profil) sachant qu'il existe une liste déterminée de profils (admin, stagiaire, designer, dev-front, dev-back) ;</li>
<li>permettre d'ajouter un développeur (le pwd est alors par défaut initialisé avec le nom du développeur) ;</li>
<li>permettre de modifier ou supprimer un développeur ;</li>
<li>permettre de modifier le pwd d'un développeur.</li>
</ul>
La liste des profils est fixe et non modifiable via cette application.<br>

### Structure de la BDD
Voici le schéma conceptuel de données présentant la structure de la BDD qui est au format MySQL :<br>
![0-MCD_habilitations](https://github.com/CNED-SLAM/Habilitations/assets/100127886/0715f730-003e-422b-92d9-a04f6071802a)

### Interface
Voici à quoi doit ressembler la fenêtre de l'application :<br>
![1-Ecran](https://github.com/CNED-SLAM/Habilitations/assets/100127886/4aa72f04-1b49-40b2-a0c4-502cdb0f4995)

### Diagramme de paquetage
L'application est structurée dans le respect du pattern MVC.<br>
![2-diagramme_de_paquetages_S2](https://github.com/CNED-SLAM/Habilitations/assets/100127886/c1b7ebf5-9b0e-4f60-8e12-494695d028f6)

#### Explications sur les couches supplémentaires
L'application contient 2 paquetages supplémentaires par rapport au MVC classique :<br>
. 'bddmanager' : contient la classe qui permet d'accéder à la base de données MySQL et d'exécuter les requêtes (classe indépendante et réutilisable).<br>
. 'dal' (Data Access Layer) : répond aux demandes du paquetage 'controller' et exploite 'bddmanager' en lui demandant d'exécuter des requêtes.<br>
L'avantage de cette architecture est l'isolement de la connexion (bddmanager) par rapport au reste de l'application. Le controleur ne sait pas d'où viennent les données (cela pourrait être un autre SGBDR, voire un autre type de fichier, comme XML). Le paquetage 'dal' fait l'intermédiaire en préparant des requêtes SQL. Donc on sait dans les classes de ce paquetage, qu'il est question d'une base de données relationnelle, mais ne sait pas non plus quel est le SGBDR utilisé.<br>
Changer de SGBDR reviendrait à juste changer la classe BddManager (son contenu), donc ne travailler que sur le paquetage 'bddmanager'.<br>
Changer de type de fichier reviendrait à changer aussi les classes du paquetage 'dal', sans toucher au reste de l'application.
#### Présentation du cheminement
L'application démarre sur une vue : c'est la structure classique des applications C# de bureau, mais il serait aussi possible de démarrer sur un contrôleur principal.<br>
La vue crée une instance du contrôleur qui lui est dédié (chaque vue a son propre contrôleur). Quand elle a besoin d'accéder aux données (affichage ou demande de modifications), elle fait appel à son contrôleur.<br>
Le contrôleur fait appel aux classes de la couche 'dal' pour exécuter les demandes de la vue.<br>
Les classes de la couche 'dal' contiennent les requêtes qui doivent être exécutées et sollicitent la couche 'bddmanager' pour exécuter les requêtes.<br>
Chaque classe de la couche 'dal' est liée à une classe métier contenu dans 'model'. Ces classes correspondent aux tables de la base de données (avec une approche objet, donc pas de clés étrangères mais des références d'objets) et ne contiennent que la structure des données (propriétés, getters, setters).
Excepté 'bddmanager' qui est indépendant de l'application (réutilisable dans n'importe quelle application), toutes les couches exploitent le 'model' (pour le formatage des données).<br>
## Etapes de construction
Les différents commits montrent la création de l'application étape par étape.
### Commit "Phase 1 : Création des paquetages et classes, début de code de bddmanager"
La structure de l'application est créée (les paquetages et classes), dans le respect du diagramme de paquetage.<br>
La classe BddManager (dans la paquetage bddmanager) commence à être construite. C'est un singleton qui permet de se connecter à une BDD et d'exécuter des requêtes SQL.<br>
L'application n'est pas encore opérationnelle.
### Commit "Phase 2 : application opérationnelle (sans authentification)"
Toutes les fonctionnalités sont codées.
### Commit "Phase 3 : Authentification"
Ajout d'une fenêtre d'authentification pour limiter l'accès aux développeurs ayant le profil 'admin'.<br>
Voici le nouveau diagramme de paquetage, avec en plus, la classe Admin dans model et la classe FrmAuthentification dans view :<br>
![3-diagramme_de_paquetages_S4](https://github.com/CNED-SLAM/Habilitations/assets/100127886/9b086d26-df1a-488c-9c78-d7caa0464ca5)

### Commit "Phase 4 : Installeur"
Création d'un installeur pour l'application.
### Commit "Phase 5 : Contrôle du pwd"
Vérification du format du pwd (entre 8 et 30 caractères, contenant au moins une minuscule, une majuscule, un chiffre, un caractère spécial et pas d'espace.
La méthode PwdFort contient des erreurs volontaires car il est demandé aux étudiants de consulte l'issue mentionnant les problèmes rencontrés et de faire des propositions de pull request.
### Commit "Phase 6 : Gestion des logs"
Journalisation avec Serilog dans les classes du paquetage dal (accès aux données).
Attention, les erreurs dans la méthode PwdFort ajoutées dans le commit précédent, ne sont pas encore corrigées. Elles le seront dans le commit suivant.
### Commit "Phase 7 : qualité de code (SonarQube), transfert chaine de connexion dans App.config."
Code corrigé et optimisé avec les alertes de Sonarlint en local et les signalements sur le serveur SonarQube (en particulier au niveau sécurité, d'où le transfert de la chaîne de connexion dans le fichier App.config).
### Commit "Phase 8 : tests sur classes des packages model et dal (avec accès à la BDD)"
Tests unitaires sur les classes du package 'model'. Tests d'intégration sur les classes du package 'dal' qui accèdent à la BDD.
### Commit "Phase 9 : ajout de fonctionnalités avec 'tests firsts'"
Ecriture des tests unitaires avant l'écriture des méthodes correspondantes à tester.

## Installation
Il est possible de tester l'application étape par étape (commit par commit) ou de tester directement la version finale.<br>
Pour tester une version dans un environnement de développement, il faut d'abord installer les outils suivants :<br>
. SGBDR MySQL (par exemple en installant WAMP ou un logiciel similaire)<br>
. De préférence un IDE pour manipuler le code (cette application a été réalisée sous Visual Studio 2022)<br> 
Il faut ensuite :<br>
. Dans MySQL, exécuter le script contenu dans habilitations.sql (présent en racine du dépôt) pour créer et remplir la BDD.<br>
. Récupérer le code du commit voulu, l'ouvrir dans l'IDE et l'exécuter.
