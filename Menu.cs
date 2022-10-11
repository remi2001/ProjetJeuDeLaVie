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

        /// <summary>
        /// Permet de naviguer dans menu
        /// </summary>
        /// <param name="JeuDeLaVie">Le jeu</param>
        public void Naviguer(Jeu JeuDeLaVie)
        {
            bool Valide = false;
            do
            {
                ConsoleKeyInfo ToucheAppuye = Console.ReadKey();

                //Si l'utilisateur n'appuie pas sur ENTRE pour validé sa sélection
                if (ToucheAppuye.Key != ConsoleKey.Enter)
                {
                    //Si l'utilisateur n,appuie pas sur la flèche de gauche pour faire retour
                    if (ToucheAppuye.Key != ConsoleKey.LeftArrow)
                    {
                        //Gère le curseur
                        GestionCurseur(ToucheAppuye);

                        //Néttoie la console pour refaire l'affichage du menu
                        Console.Clear();
                        ToString();
                    }
                    else
                    {
                        RetourAuMenuPrecedent(JeuDeLaVie);
                    }
                }
                else
                {
                    Valide = true;
                }

            } while (Valide == false);

            //Sélectionne l'action liée au sous-menu voulant être parcouru par l'utilisateur
            SelectionSousMenu(this.Curseur, this.GroupeMenu, JeuDeLaVie);
        }

        private void RetourAuMenuPrecedent(Jeu JeuDeLaVie)
        {
            //Si le menu actuel n'est pas le menu principale
            if (GroupeMenu != 1)
            {
                Console.Clear();

                //On affiche et permet de naviguer dans le menu n-1
                JeuDeLaVie.GetListeMenuPouvantEtreENTRER[GroupeMenu - 2].ToString();
                JeuDeLaVie.GetListeMenuPouvantEtreENTRER[GroupeMenu - 2].Naviguer(JeuDeLaVie);
            }
            else
            {
                //Sinon je refait la navigation afin d'éviter tout arrêt prématuré du programme
                Naviguer(JeuDeLaVie);
            }
        }

        /// <summary>
        /// Permet de gérer le curseur du menu, c'est à dire, sa position selon les touches
        /// </summary>
        /// <param name="ToucheAppuye"></param>
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
        /// <param name="GroupeMenu">Le groupe du menu dans le quel on est</param>
        /// <param name="JeuDeLaVie">Le jeu</param>
        private void SelectionSousMenu(byte IdSousMenu, byte GroupeMenu, Jeu JeuDeLaVie)
        {
            //Formatage de l'dentifacateur complet du sous menu pour ressembler à x.x
            string IdentifacateurCompletDuSousMenu = GroupeMenu + "." + IdSousMenu;

            if (GroupeMenu == 2)
            {
                ModificationOption(JeuDeLaVie, IdSousMenu);
            }
            else{
                switch (IdentifacateurCompletDuSousMenu)
                {
                    case "1.0":
                        JeuDeLaVie.LancerJeu();
                        break;
                    case "1.1":
                        JeuDeLaVie.AffichageOptionJeu();
                        break;
                    case "1.2":
                        JeuDeLaVie.QuitterJeu();
                        break;
                }
            }
            
        }

        private void ModificationOption(Jeu JeuDeLaVie, byte IdSousMenu)
        {
            Console.Clear();

            switch (IdSousMenu)
            {
                case 0:
                    
                    Console.WriteLine("Vous modifier le pourcentage d'appartition des cellules");
                    JeuDeLaVie.SetPourcentage = GestionValeurEntreUtilisateur();
                    ListeSousMenu[IdSousMenu].SetPhrase = "Le pourcentage de cellule vivante (actuel : " + JeuDeLaVie.GetPourcentage + " )";
                    
                    break;
                case 1:
                    
                    break;
                case 2:
                    
                    break;
                case 3:
                    Console.WriteLine("Vous modifier le nombre de ligne du terrain");
                    JeuDeLaVie.SetNombreLigne = GestionValeurEntreUtilisateur();
                    ListeSousMenu[IdSousMenu].SetPhrase = "Le nombre de ligne du terrain (actuel : " + JeuDeLaVie.GetNombreLigne + " )";
                    break;
                case 4:
                    Console.WriteLine("Vous modifier le nombre de colonne du terrain");
                    JeuDeLaVie.SetNombreColonne = GestionValeurEntreUtilisateur();
                    ListeSousMenu[IdSousMenu].SetPhrase = "Le nombre de colonne du terrain (actuel : " + JeuDeLaVie.GetNombreColonne + " )";
                    break;
            }

            Console.WriteLine("VALEUR ENREGISTRE");
            JeuDeLaVie.AffichageOptionJeu();
        }

        private int GestionValeurEntreUtilisateur()
        {
            int NombreEntre;

            bool NombreEntreRespecteCondition;

            do
            {
                Console.WriteLine("Entrez la nouvelle valeur : ");
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

        public override string? ToString()
        {
            Console.WriteLine(PhraseExplicatifMenu);

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
    }
}
