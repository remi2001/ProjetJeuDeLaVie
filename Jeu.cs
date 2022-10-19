using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetJeuDeLaVie
{
    internal class Jeu
    {
        private List<Menu> MenuPouvantEtreENTRER;
        private Menu MenuPrincipale;
        private Menu MenuOption;
        private Terrain Terrain_du_Jeu;

        //Paramètre du jeu
        private int Pourcentage;
        private int NombreLigne;
        private int NombreColonne;
        private int NbGeneration;

        public Jeu()
        {
            //Valeur paramètre par défaut
            Pourcentage = 60;
            NombreLigne = 100;
            NombreColonne = 100;
            NbGeneration = 30;

            //Création du menu PRINCIPAL
            SousMenu FEN_Lancer = new SousMenu(0, "Lancer");
            SousMenu FEN_Option = new SousMenu(1, "Option");
            SousMenu FEN_Quitter = new SousMenu(2, "Quitter");

            List<SousMenu>? ListeSousMenuPrincipale = new List<SousMenu>(3)
            {
                FEN_Lancer,
                FEN_Option,
                FEN_Quitter,
            };

            MenuPrincipale = new Menu(ListeSousMenuPrincipale, 1,
                "Voici le menu. Pour naviguer, appuyez sur les flèches haut et bas. Pour valider votre sélection appuyez sur ENTER");

            ListeSousMenuPrincipale = null;

            MenuPouvantEtreENTRER = new(1)
            {
                MenuPrincipale
            };
            //----------------------------------------------------------------------------------------//

            //Création du menu OPTION
            SousMenu FEN_OptionPourcentage = new SousMenu(0, "Le pourcentage de cellule vivante (actuel : "+ Pourcentage +" )" );
            SousMenu FEN_OptionVitesseJeu = new SousMenu(1, "La vitesse du jeu (pas encore implémenter)");
            SousMenu FEN_OptionGenFinal = new SousMenu(2, "Le choix de la génération final (actuel : " + NbGeneration + " )");
            SousMenu FEN_OptionNombreLigne = new SousMenu(2, "Le nombre de ligne du terrain (actuel : " + NombreLigne + ")") ;
            SousMenu FEN_OptionNombreColonne = new SousMenu(2, "Le nombre de colonne du terrain (actuel : " + NombreColonne + ")");

            List<SousMenu>? ListeSousMenuOption = new List<SousMenu>(5)
            {
                FEN_OptionPourcentage,
                FEN_OptionVitesseJeu,
                FEN_OptionGenFinal,
                FEN_OptionNombreLigne,
                FEN_OptionNombreColonne
            };

            MenuOption = new Menu(ListeSousMenuOption, 2, "Voici les options Modifiables");
            MenuPouvantEtreENTRER.Add(MenuOption);

            ListeSousMenuOption = null;
            //----------------------------------------------------------------------------------------//
        }

        public void LancementDuJeu()
        {
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
            Thread.Sleep(1000);
            Console.Clear();

            //Création du terrain
            Terrain_du_Jeu = new Terrain(Pourcentage);
            Terrain_du_Jeu.InitialisationTerrain();
            Terrain_du_Jeu.GestionApparitionCellule();
            Terrain_du_Jeu.Affichage_du_terrain();
            //----------------------------------------------------------------------------------------//

            DeroulementNormal(Terrain_du_Jeu);
            //DeroulementDayAndNight(Terrain_du_Jeu);
        }

        /// <summary>
        /// Initialise le menu des options et Affiche les options du jeu
        /// </summary>
        public void AffichageOptionJeu()
        {
            //On enlève l'affichage présent
            Console.Clear();

            MenuOption.ToString();
            MenuOption.Naviguer(this);
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

            if (i == 0 || j == 0 || i == 99 || j == 99)
            {
                if((i == 0 && (j != 0 && j != 99))|| (i == 99 && (j != 0 && j != 99)) || (j == 0 && (i != 0 && i != 99)) || (j == 99 && (i != 0 && i != 99)))
                    nbCellule = ComptCellCote(terrain, i, j, nbCellule);
                else
                    nbCellule = ComptCellCoin(terrain, i, j, nbCellule);
            }
            else
            {
                nbCellule = ComptCellSansParticularite(terrain, i, j, nbCellule);
            }            

            return nbCellule;
        }

        private int ComptCellCote(Terrain terrain, int i, int j, int nbCellule)
        {
            if (i == 0 && j != 0 && j != 99)
            {
                if (terrain.UtilisationTerrain[i, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j + 1] == true) nbCellule++;
            }
            if (i == 99 && j != 0 && j != 99)
            {
                if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
            }
            if (j == 0 && i != 0 && i != 99)
            {
                if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j + 1] == true) nbCellule++;
            }
            if (j == 99 && i != 0 && i != 99)
            {
                if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
            }
            return nbCellule;
        }

        private int ComptCellCoin(Terrain terrain, int i, int j, int nbCellule)
        {
            if (i == 0 && j == 0)
            {
                if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j + 1] == true) nbCellule++;
            }
            if (i == 0 && j == 99)
            {
                if (terrain.UtilisationTerrain[i, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
            }
            if (i == 99 && j == 0)
            {
                if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j + 1] == true) nbCellule++;
            }
            if (i == 99 && j == 99)
            {
                if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j - 1] == true) nbCellule++;
            }
            return nbCellule;
        }

        private int ComptCellSansParticularite(Terrain terrain, int i, int j, int nbCellule)
        {
            if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i, j - 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i - 1, j - 1] == true) nbCellule++;

            if (terrain.UtilisationTerrain[i - 1, j + 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i + 1, j - 1] == true) nbCellule++;

            if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[i + 1, j + 1] == true) nbCellule++;
            return nbCellule;
        }

        /// <summary>
        /// Déroulement des règles du jeu standard
        /// </summary>
        public void DeroulementNormal(Terrain terrain)
        {
            Terrain ProchaineGeneration = new Terrain(0);
            for (int k = 0; k < NbGeneration; k++)
            {
                ProchaineGeneration.InitialisationTerrain();
                int nbcellule;
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        nbcellule = ComptCellAutour(terrain, i, j);
                        //Condition pour la naissance d'une cellule
                        /*
                        if (nbcellule == 3 && terrain.UtilisationTerrain[i, j] == false)
                        {
                            //La valeur deviens true dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[i, j] = true;
                        }*/
                        //Obligatoire sinon les cellules repasse a faux car ce n'est pas le meme tableau
                        //Naissance et survie d'un cellule
                        if(nbcellule == 3 || (nbcellule == 2 && terrain.UtilisationTerrain[i, j] == true))
                        {
                            ProchaineGeneration.UtilisationTerrain[i, j] = true;
                        }
                        //Condition pour la mort d'une cellule
                        if ((nbcellule <= 1 || nbcellule >= 4 ) && (terrain.UtilisationTerrain[i, j] == true))
                        {
                            //La valeur deviens false dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[i, j] = false;
                        }
                    }
                }
                if (ProchaineGeneration.UtilisationTerrain != null)
                {
                    terrain.UtilisationTerrain = (bool[,])ProchaineGeneration.UtilisationTerrain.Clone();
                }
                terrain.Affichage_du_terrain();
            }
        }

        /// <summary>
        /// Déroulement des règles du jeu en mode Day and Night
        /// </summary>
        public void DeroulementDayAndNight(Terrain terrain)
        {
            int nbcellule;
            Terrain ProchaineGeneration = new Terrain(0);
            for (int i = 0; i < 100; i++)
            {
                ProchaineGeneration.InitialisationTerrain();
                for (int j = 0; j < 100; j++)
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
                if (ProchaineGeneration.UtilisationTerrain != null)
                {
                    terrain.UtilisationTerrain = (bool[,])ProchaineGeneration.UtilisationTerrain.Clone();
                }
                terrain.Affichage_du_terrain();
            }
        }

        
        public List<Menu> GetListeMenuPouvantEtreENTRER
        {
            get { return MenuPouvantEtreENTRER; }
        }

        public int GetPourcentage
        {
            get { return Pourcentage; }
        }

        public int SetPourcentage
        {
            set => Pourcentage = value;
        }

        public int GetNombreColonne
        {
            get { return NombreColonne; }
        }

        public int SetNombreColonne
        {
            set => NombreColonne = value;
        }

        public int GetNombreLigne
        {
            get { return NombreLigne; }
        }

        public int SetNombreLigne
        {
            set => NombreLigne = value;
        }
        public int GetNombreGeneration
        {
            get { return NbGeneration; }
        }

        public int SetNombreGeneration
        {
            set => NbGeneration = value;
        }
    }
}
