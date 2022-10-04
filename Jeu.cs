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
            SousMenu FEN_Lancer = new SousMenu(0, "Lancer");
            SousMenu FEN_Option = new SousMenu(1, "Option");
            SousMenu FEN_Quitter = new SousMenu(2, "Quitter");

            List<SousMenu> ListeSousMenuPrincipale = new List<SousMenu>(3)
            {
                FEN_Lancer,
                FEN_Option,
                FEN_Quitter,
            };

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

            SousMenu? FEN_Option1 = new SousMenu(0, "Option 1");
            SousMenu? FEN_Option2 = new SousMenu(1, "Option 2");
            SousMenu? FEN_Option3 = new SousMenu(2, "Option 3");

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

        /// <summary>
        /// Compte le nombre de cellule autour de celle rentré en paramètre
        /// </summary>
        private int ComptCellAutour(Terrain terrain, int i ,int j)
        {
            int nbCellule = 0;
            if (terrain.UtilisationTerrain[i - 1,j - 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i - 1, j + 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i    , j + 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i + 1, j + 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i + 1, j - 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i    , j - 1] == true) nbCellule++;
            return nbCellule;
        }

        /// <summary>
        /// Déroulement des règles du jeu standard
        /// </summary>
        public void DeroulementNormal(Terrain terrain)
        {
            int nbcellule;
            Terrain ProchaineGeneration = new Terrain(0);
            for (int i = 0; i <= 100; i++)
            {
                for (int j = 0; j <= 100; j++)
                {
                    nbcellule = ComptCellAutour(terrain, i, j);
                    //Condition pour la naissance d'une cellule
                    if (nbcellule == 3 && terrain.UtilisationTerrain[i, j] == false)
                    {
                        //La valeur deviens true dans le tableau de la prochaine génération
                        ProchaineGeneration.UtilisationTerrain[i, j] = true;
                    }
                    //Condition pour la mort d'une cellule
                    if ((nbcellule == 0 || nbcellule == 1 || nbcellule == 4 || nbcellule == 5 || nbcellule == 6 || nbcellule == 7 || nbcellule == 8) && (terrain.UtilisationTerrain[i, j] == true))
                    {
                        //La valeur deviens false dans le tableau de la prochaine génération
                        ProchaineGeneration.UtilisationTerrain[i, j] = false;
                    }
                }
            }
            ProchaineGeneration.Affichage_du_terrain();
        }

        /// <summary>
        /// Déroulement des règles du jeu en mode Day and Night
        /// </summary>
        public void DeroulementDayAndNight(Terrain terrain)
        {
            int nbcellule;
            Terrain ProchaineGeneration = new Terrain(0);
            for (int i = 0; i <= 100; i++)
            {
                for (int j = 0; j <= 100; j++)
                {
                    nbcellule = ComptCellAutour(terrain, i, j);
                    //Condition pour la naissance d'une cellule
                    if ((nbcellule == 3 || nbcellule == 4 || nbcellule == 6 || nbcellule == 7 || nbcellule == 8) && (terrain.UtilisationTerrain[i, j] == false))
                    {
                        //La valeur deviens true dans le tableau de la prochaine génération
                        ProchaineGeneration.UtilisationTerrain[i, j] = true;
                    }
                    //Condition pour la mort d'une cellule
                    if ((nbcellule == 0 || nbcellule == 1 || nbcellule == 2 || nbcellule == 4 || nbcellule == 5) && (terrain.UtilisationTerrain[i, j] == true))
                    {
                        //La valeur deviens false dans le tableau de la prochaine génération
                        ProchaineGeneration.UtilisationTerrain[i, j] = true;
                    }
                }
            }
            ProchaineGeneration.Affichage_du_terrain();
        }
    }
}
