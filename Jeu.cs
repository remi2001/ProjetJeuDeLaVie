using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetJeuDeLaVie
{
    internal class Jeu
    {
        public void LancementDuJeu()
        {
            List<SousMenu> ListeSousMenuPrincipale = new List<SousMenu>();

            SousMenu FEN_Lancer = new SousMenu(1, "Lancer");
            SousMenu FEN_Option = new SousMenu(2, "Option");
            SousMenu FEN_Quitter = new SousMenu(3, "Quitter");

            ListeSousMenuPrincipale.Add(FEN_Lancer);
            ListeSousMenuPrincipale.Add(FEN_Option);
            ListeSousMenuPrincipale.Add(FEN_Quitter);

            Menu MenuPrincipale = new Menu(ListeSousMenuPrincipale, 1, "Voici le menu. Pour naviguer, appuyez sur les flèches haut et bas. Pour ____ appuyez sur ENTER");

            MenuPrincipale.ToString();
            MenuPrincipale.Naviguer(this);
            
        }

        /// <summary>
        /// Lance le jeu
        /// </summary>
        public void LancerJeu()
        {
            //Enlève tout affichage de la console
            Console.Clear();
            Console.WriteLine("LANCEMENT");
        }

        /// <summary>
        /// Initialise le menu des options et Affiche les options du jeu
        /// </summary>
        public void OptionJeu()
        {
            //On enlève l'affichage présent
            Console.Clear();

            List<SousMenu>? ListeSousMenuOption = new List<SousMenu>();

            SousMenu? FEN_Option1 = new SousMenu(1, "Option 1");
            SousMenu? FEN_Option2 = new SousMenu(2, "Option 2");
            SousMenu? FEN_Option3 = new SousMenu(3, "Option 3");

            ListeSousMenuOption.Add(FEN_Option1);
            ListeSousMenuOption.Add(FEN_Option2);
            ListeSousMenuOption.Add(FEN_Option3);

            Menu MenuOption = new Menu(ListeSousMenuOption, 2, "Voici les options Modifiables");

            MenuOption.ToString();
            MenuOption.Naviguer(this);

            //Suppression des sous menu afin d'éviter une duplication. Le garbage collector va automatique les supprimers et libérer de
            //La mémoire. Il détecte les objets à supprimer en les mettant à nul et de notifier avec une "?" lors de leurs constructions 
            //Comme plus haut
            FEN_Option1 = null;
            FEN_Option2 = null;
            FEN_Option3 = null;
            ListeSousMenuOption = null;
        }

        /// <summary>
        /// Quitte le jeu, c'est à dire, le programme
        /// </summary>
        public void QuitterJeu()
        {
            Environment.Exit(1);
        }

        public void Deroulement()
        {
            bool[,] terrain = new bool[100, 100];
            Random aleatoire = new Random();

            //Variable du pourcentage d'apparitions des cellules pouvant etre changer
            int pourcentage = 14;

            int longueur = 0;
            int hauteur = 0;
            int nb_cellule = 0;

            //initialisation du tableau
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    terrain[i, j] = false;
                }
            }

            //Gestion d'apparition des cellules selon le pourcentage et de facon aléatoire
            while (nb_cellule < (pourcentage * 100))
            {
                longueur = aleatoire.Next(99);
                hauteur = aleatoire.Next(99);

                if (terrain[longueur, hauteur] == false)
                {
                    terrain[longueur, hauteur] = true;
                    nb_cellule++;
                }
                else
                {
                    while (terrain[longueur, hauteur] == true)
                    {
                        longueur++;
                        if (longueur == 100)
                        {
                            longueur = 0;
                            hauteur++;
                            if (hauteur == 100)
                            {
                                hauteur = 0;
                                longueur = 0;
                            }
                        }
                    }
                    terrain[longueur, hauteur] = true;
                    nb_cellule++;
                }
            }

            //Affichage
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (terrain[i, j] == true) Console.Write("X");
                    else Console.Write(" ");
                }
                Console.WriteLine("");
            }
        }
    }
}
