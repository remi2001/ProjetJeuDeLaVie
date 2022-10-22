using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProjetJeuDeLaVie
{
    internal class Menu
    {
        #region Attribut
        /// <summary>
        /// Variable qui contient tous les sous-menu attachés à ce menu
        /// </summary>
        private List<SousMenu> ListeSousMenu;

        /// <summary>
        /// Variable qui permet de savoir la hiéarchie des menu ou de savoir dans qu'elle menu on se situe
        /// </summary>
        private byte GroupeMenu;

        /// <summary>
        /// Variable qui stocke la phrase d'introduction du menu pour informé l'utilisateur par exemple
        /// </summary>
        private string PhraseExplicatifMenu;

        /// <summary>
        /// Variable qui enregistre la position, par défaut, du curseur pour un menu
        /// </summary>
        private byte Curseur;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur d'un menu
        /// </summary>
        /// <param name="listeSousMenu">Liste des sous-menu liée au menu</param>
        /// <param name="groupeMenu">Groupe du menu </param>
        /// <param name="phraseExplicatifMenu">Phrase d'information voulant être affichée pour informer l'utilisateur</param>
        public Menu(List<SousMenu> listeSousMenu, byte groupeMenu, string phraseExplicatifMenu)
        {
            if (listeSousMenu != null)
            {
                ListeSousMenu = listeSousMenu;
                GroupeMenu = groupeMenu;
                PhraseExplicatifMenu = phraseExplicatifMenu;
                Curseur = 0;
            }
            else
            {
                Console.WriteLine("Le menu est vide donc n'existe pas. Il ne peut se construire. Le programme va s'arrêter");
                Environment.Exit(-1);
            }
        }
        #endregion

        #region Méthode

        /// <summary>
        /// Permet de naviguer dans menu
        /// </summary>
        /// <returns>La position du curseur</returns>
        public byte Naviguer()
        {
            //Variable enregistrant si l'utilisateur à valider sa sélection
            bool Valide = false;

            do
            {
                //Lecture de la touche
                ConsoleKeyInfo ToucheAppuye = Console.ReadKey();

                //Si l'utilisateur n'appuie pas sur ENTRE pour validé sa sélection
                if (ToucheAppuye.Key != ConsoleKey.Enter)
                {
                    //Si l'utilisateur n'appuie pas sur la flèche de gauche pour faire retour
                    if (ToucheAppuye.Key == ConsoleKey.LeftArrow && GroupeMenu != 1)
                    {
                        //Simule la touche ENTER pour signaler que l'utilisateur à terminer sa sélection
                        Valide = true;
                        //Code de retour
                        Curseur = 255;
                    }
                    //Néttoie la console pour refaire l'affichage du menu
                    Console.Clear();

                    //Gère le curseur
                    GestionCurseur(ToucheAppuye);

                    //Affiche le menu dans lequel l'utilisateur est
                    ToString();

                }
                else
                {
                    Valide = true;
                }

            } while (Valide == false);

            return Curseur;
        }

        /// <summary>
        /// Permet de gérer le curseur du menu, c'est à dire, sa position selon les touches
        /// </summary>
        /// <param name="ToucheAppuye">Le type de touche appuyé</param>
        private void GestionCurseur(ConsoleKeyInfo ToucheAppuye)
        {
            switch (ToucheAppuye.Key)
            {
                //Si la flèche du bas est appuyé
                case ConsoleKey.DownArrow:
                    //Si le curseur n'arrive pas en bas du menu
                    if (Curseur != ListeSousMenu.Count - 1)
                    {
                        Curseur++;

                        //Attribution du curseur selon la navigation
                        ListeSousMenu[Curseur - 1].SetSiSelectionner = " ";
                        ListeSousMenu[Curseur].SetSiSelectionner = ">>";
                    }
                    break;
                //Si la flèche du haut est appuyé
                case ConsoleKey.UpArrow:
                    //Si le curseur n'arrive pas en haut du menu
                    if (Curseur != 0)
                    {
                        Curseur--;

                        //Attribution du curseur selon la navigation
                        ListeSousMenu[Curseur].SetSiSelectionner = ">>";
                        ListeSousMenu[Curseur + 1].SetSiSelectionner = " ";
                    }
                    break;
            }
        }

        /// <summary>
        /// Sélectionne l'action que va réaliser le programme selon le sous-menu sélectionné
        /// </summary>
        /// <param name="IdSousMenu">Identifiant du menu sélectionné</param>
        public void SelectionSousMenu(byte IdSousMenu)
        {
            //Formatage de l'dentifacateur complet du sous menu pour ressembler à x.x
            string IdentifacateurCompletDuSousMenu = GroupeMenu + "." + IdSousMenu;

            //Si l'utilisateur est dans les options
            if (GroupeMenu == 2)
            {
                //On donne l'accès à la modification des options
                ModificationOption(IdSousMenu);

                //Une fois l'option modifier, on ré-affiche le menu des options si l'utilisateur veut en modifier d'autre
                Program.OptionJeu();
            }
            else
            {
                //On éxecute une action selon le menu choisi et le menu dans le quel l'utilisateur est
                switch (IdentifacateurCompletDuSousMenu)
                {
                    case "1.0":
                        Program.MenuLancer();
                        break;
                    case "1.1":
                        Program.OptionJeu();
                        break;
                    case "1.2":
                        Program.QuitterJeu();
                        break;
                }
            }
        }

        /// <summary>
        /// Modifie la valeur de l'option sélectionnée
        /// </summary>
        /// <param name="IdSousMenu">L'identifiant du sous-menu sélectionner</param>
        public void ModificationOption(byte IdSousMenu)
        {
            Console.Clear();

            //On éxecute différente action selon l'identifiant du sous-menu
            switch (IdSousMenu)
            {
                case 0:
                    Console.WriteLine("Vous modifier le pourcentage d'appartition des cellules");
                    //Modifie la valeur de l'option en vérifiant la saisie de l'utilisateur
                    Program.SetPourcentage = GestionValeurEntreUtilisateur();
                    //Modifie la phrase du sous-menu pour actualiser la valeur de l'option affichée
                    ListeSousMenu[IdSousMenu].SetPhrase = "Le pourcentage de cellule vivante (actuel : " + Program.GetPourcentage + " )";
                    break;

                case 1:
                    Console.WriteLine("Vous modifier la vitesse de jeu. Veuilliez entrer un nombre entre 0,1 et 10 ! " +
                        "Il correspond à la vitesse en seconde.");
                    //Modifie la valeur de l'option en vérifiant la saisie de l'utilisateur
                    Program.SetVitesseJeu = GestionValeurEntreUtilisateurVitesse();
                    //Modifie la phrase du sous-menu pour actualiser la valeur de l'option affichée
                    ListeSousMenu[IdSousMenu].SetPhrase = "La vitesse de jeu (actuel : " + Program.GetVitesseJeu + " )";
                    break;

                case 2:
                    Console.WriteLine("Vous modifier le nombre maximum de génération");
                    //Modifie la valeur de l'option en vérifiant la saisie de l'utilisateur
                    Program.SetNbGeneration = GestionValeurEntreUtilisateur();
                    //Modifie la phrase du sous-menu pour actualiser la valeur de l'option affichée
                    ListeSousMenu[IdSousMenu].SetPhrase = "Le nombre de ligne du terrain (actuel : " + Program.GetNbGeneration + " )";
                    break;

                case 3:
                    Console.WriteLine("Vous modifier le nombre de ligne du terrain");
                    //Modifie la valeur de l'option en vérifiant la saisie de l'utilisateur
                    Program.SetNombreLigne = GestionValeurEntreUtilisateur();
                    //Modifie la phrase du sous-menu pour actualiser la valeur de l'option affichée
                    ListeSousMenu[IdSousMenu].SetPhrase = "Le nombre de ligne du terrain (actuel : " + Program.GetNombreLigne + " )";
                    break;

                case 4:
                    Console.WriteLine("Vous modifier le nombre de colonne du terrain");
                    //Modifie la valeur de l'option en vérifiant la saisie de l'utilisateur
                    Program.SetNombreColonne = GestionValeurEntreUtilisateur();
                    //Modifie la phrase du sous-menu pour actualiser la valeur de l'option affichée
                    ListeSousMenu[IdSousMenu].SetPhrase = "Le nombre de colonne du terrain (actuel : " + Program.GetNombreColonne + " )";
                    break;
            }

            Console.WriteLine("VALEUR ENREGISTRE");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Vérifie la saisie de l'utilisateur
        /// </summary>
        /// <returns>Retourne la valeur saisie</returns>
        private int GestionValeurEntreUtilisateur()
        {
            int NombreEntre;
            bool NombreEntreRespecteCondition;

            do
            {
                Console.WriteLine("Entrez la nouvelle valeur : ");
                //Vérifie si la saisie est bonne
                if (!Int32.TryParse(Console.ReadLine(), out NombreEntre))
                {
                    Console.WriteLine("L'entré saisie ne respecte pas les conditions. Veuilliez ré-essayer !");
                    NombreEntreRespecteCondition = false;
                }
                else
                {
                    NombreEntreRespecteCondition = true;
                }

            } while (!NombreEntreRespecteCondition);

            return NombreEntre;
        }

        /// <summary>
        /// Vérifie la saisie de l'utilisateur
        /// </summary>
        /// <returns>Retourne la valeur saisie</returns>
        private float GestionValeurEntreUtilisateurVitesse()
        {
            float NombreEntre;

            bool NombreEntreRespecteCondition;

            do
            {
                Console.WriteLine("Entrez la nouvelle valeur : ");
                //Vérifie si la saisie est bonne
                if (!float.TryParse(Console.ReadLine(), out NombreEntre))
                {
                    Console.WriteLine("L'entré saisie ne respecte pas les conditions. Veuilliez ré-essayer en entrant un nombre entre 0,1 et 10 ! " +
                        "Il correspond à la vitesse en seconde .");
                    NombreEntreRespecteCondition = false;
                }
                else
                {
                    //Vérifie si la saisie est bien entre 0,0 et 10,0
                    if (NombreEntre <= 0.0 || NombreEntre > 10.0)
                    {
                        Console.WriteLine("L'entré saisie ne respecte pas les conditions. Veuilliez ré-essayer en entrant un nombre entre 0,1 et 10 ! " +
                            "Il correspond à la vitesse en seconde.");
                        NombreEntreRespecteCondition = false;
                    }
                    else
                    {
                        NombreEntreRespecteCondition = true;
                    }
                }

            } while (!NombreEntreRespecteCondition);

            return NombreEntre;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            Console.WriteLine(PhraseExplicatifMenu);

            //Affiche toutes les phrases liées aux sous-menus
            foreach(SousMenu sousmenu in ListeSousMenu)
            {
                Console.WriteLine(sousmenu.ToString());
            }

            //Règle le problème d'affichage de la console avec le fond blanc.
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Pour faire un retour en arrière appuyez sur la flèches de gauche");

            return null;
        }
        #endregion

        #region Accesseur
        public byte GetGroupeMenu
        {
            get { return GroupeMenu; }
        }

        public byte SetCurseur
        {
            set { Curseur = value; }
        }
        #endregion
    }
}
