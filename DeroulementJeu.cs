using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjetJeuDeLaVie
{
    internal class DeroulementJeu
    {
        //Paramètre du jeu
        private int Pourcentage;
        private int NbGeneration;
        private Terrain TerrainDuJeu;
        private float VitesseJeu;


        public DeroulementJeu(int pourcentage, int nbGeneration, Terrain terrainDuJeu, float vitesseJeu)
        {
            Pourcentage = pourcentage;
            NbGeneration = nbGeneration;
            TerrainDuJeu = terrainDuJeu;
            VitesseJeu = vitesseJeu;
        }

        /// <summary>
        /// Compte le nombre de cellule vivante autour de celle rentrée en paramètre
        /// </summary>
        private int ComptageCelluleAutour(Terrain terrain, int LigneCellule ,int ColonneCellule)
        {
            int nbCellule = 0;

            if (LigneCellule == 0 || ColonneCellule == 0 || LigneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 || ColonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1)-1)
            {
                if((LigneCellule == 0 && (ColonneCellule != 0 && ColonneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(1)-1))|| (LigneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0) -1 && (ColonneCellule != 0 && ColonneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)) || (ColonneCellule == 0 && (LigneCellule != 0 && LigneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)) || (ColonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1)-1 && (LigneCellule != 0 && LigneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(1)-1)))
                    nbCellule = ComptageCelluleCote(terrain, LigneCellule, ColonneCellule, nbCellule);
                else
                    nbCellule = ComptageCelluleCoin(terrain, LigneCellule, ColonneCellule, nbCellule);
            }
            else
            {
                nbCellule = ComptageCelluleSansParticularite(terrain, LigneCellule, ColonneCellule, nbCellule);
            }            
            //retourne le nombre de cellule vivante
            return nbCellule;
        }

        /// <summary>
        /// Compte le nombre de cellule vivante autour d'une cellule dans un coté rentrée en paramètre
        /// </summary>
        private int ComptageCelluleCote(Terrain terrain, int LigneCellule, int ColonneCellule, int nbCellule)
        {
            if (LigneCellule == 0 && ColonneCellule != 0 && ColonneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule + 1] == true) nbCellule++;
            }
            if (LigneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 && ColonneCellule != 0 && ColonneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule + 1] == true) nbCellule++;
            }
            if (ColonneCellule == 0 && LigneCellule != 0 && LigneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)
            {
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule + 1] == true) nbCellule++;
            }
            if (ColonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1 && LigneCellule != 0 && LigneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)
            {
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule] == true) nbCellule++;
            }
            return nbCellule;
        }

        /// <summary>
        /// Compte le nombre de cellule vivante autour d'une cellule dans un coin rentrée en paramètre
        /// </summary>
        private int ComptageCelluleCoin(Terrain terrain, int LigneCellule, int ColonneCellule, int nbCellule)
        {
            if (LigneCellule == 0 && ColonneCellule == 0)
            {
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule + 1] == true) nbCellule++;
            }
            if (LigneCellule == 0 && ColonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule] == true) nbCellule++;
            }
            if (LigneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 && ColonneCellule == 0)
            {
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule + 1] == true) nbCellule++;
            }
            if (LigneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 && ColonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule - 1] == true) nbCellule++;
            }
            return nbCellule;
        }

        /// <summary>
        /// Compte le nombre de cellule vivante autour d'une cellule rentrée en paramètre qui n'est pas sur un coté ni un coin du tableau
        /// </summary>
        private int ComptageCelluleSansParticularite(Terrain terrain, int LigneCellule, int ColonneCellule, int nbCellule)
        {
            if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule] == true) nbCellule++;
            if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule - 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule - 1] == true) nbCellule++;

            if (terrain.UtilisationTerrain[LigneCellule - 1, ColonneCellule + 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule - 1] == true) nbCellule++;

            if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule] == true) nbCellule++;
            if (terrain.UtilisationTerrain[LigneCellule, ColonneCellule + 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[LigneCellule + 1, ColonneCellule + 1] == true) nbCellule++;
            return nbCellule;
        }

        /// <summary>
        /// Déroulement des règles du jeu standard
        /// </summary>
        public void DeroulementJeuNormal()
        {
            //Affichage du terrain de base
            TerrainDuJeu.AffichageTerrain(1);
            Console.WriteLine("Nombre de génération : 0");
            //Création du tableau de la prochaine génération pour chaque génération
            Terrain ProchaineGeneration = new Terrain(0, TerrainDuJeu.UtilisationTerrain.GetLength(0), TerrainDuJeu.UtilisationTerrain.GetLength(1));
            for (int k = 0; k < NbGeneration; k++)
            {
                //Initialisation du tableau à false
                ProchaineGeneration.InitialisationTerrain();
                int nbcellule;
                for (int LigneCellule = 0; LigneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(0); LigneCellule++)
                {
                    for (int ColonneCellule = 0; ColonneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(1); ColonneCellule++)
                    {
                        nbcellule = ComptageCelluleAutour(TerrainDuJeu, LigneCellule, ColonneCellule);
                        //Condition pour récupérer la touche du clavier utilisé
                        if (Console.KeyAvailable == true)
                        {
                            //Modifier la vitesse en fonction de la touche appuyé
                            ModificationVitesse();
                        }
                        //Naissance et survie d'un cellule
                        if (nbcellule == 3 || (nbcellule == 2 && TerrainDuJeu.UtilisationTerrain[LigneCellule, ColonneCellule] == true))
                        {
                            ProchaineGeneration.UtilisationTerrain[LigneCellule, ColonneCellule] = true;
                        }
                        else//Condition pour la mort d'une cellule
                        {
                            //La valeur deviens false dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[LigneCellule, ColonneCellule] = false;
                        }
                    }
                }
                //Condition permettent de cloner le tableau prochaine génération dans le tableau à afficher
                if (ProchaineGeneration.UtilisationTerrain != null)
                {
                    TerrainDuJeu.UtilisationTerrain = (bool[,])ProchaineGeneration.UtilisationTerrain.Clone();
                }
                TerrainDuJeu.AffichageTerrain(VitesseJeu);
                //Affiche le nombre de génération ainsi que la vitesse du jeu en dessous de chaque génération
                Console.WriteLine("Nombre de génération : " + (k+1));
                Console.WriteLine("Pour augmenter la vitesse de jeu, appuyez sur la flèche du bas. Pour la diminuer, appuyez sur celle du haut !");
                Console.WriteLine("Vitesse du jeu : " + VitesseJeu);
            }
        }

        /// <summary>
        /// Déroulement des règles du jeu en mode Day and Night
        /// </summary>
        public void DeroulementJeuDayAndNight()
        {
            //Affichage du terrain de base
            TerrainDuJeu.AffichageTerrain(1);
            Console.WriteLine("Nombre de génération : 0");
            //Création du tableau de la prochaine génération pour chaque génération
            Terrain ProchaineGeneration = new Terrain(0, TerrainDuJeu.UtilisationTerrain.GetLength(0), TerrainDuJeu.UtilisationTerrain.GetLength(1));
            for (int k = 0; k < NbGeneration; k++)
            {
                //Initialisation du tableau à false
                ProchaineGeneration.InitialisationTerrain();
                int nbcellule;
                for (int LigneCellule = 0; LigneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(0); LigneCellule++)
                {
                    for (int ColonneCellule = 0; ColonneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(1); ColonneCellule++)
                    {
                        nbcellule = ComptageCelluleAutour(TerrainDuJeu, LigneCellule, ColonneCellule);
                        //Condition pour récupérer la touche du clavier utilisé
                        if (Console.KeyAvailable == true)
                        {
                            //Modifier la vitesse en fonction de la touche appuyé
                            ModificationVitesse();
                        }
                        //Naissance et survie d'un cellule
                        if (nbcellule == 3 || nbcellule == 6 || nbcellule == 7 || nbcellule == 8 || (nbcellule == 4 && TerrainDuJeu.UtilisationTerrain[LigneCellule, ColonneCellule] == true))
                        {
                            ProchaineGeneration.UtilisationTerrain[LigneCellule, ColonneCellule] = true;
                        }
                        else//Condition pour la mort d'une cellule
                        {
                            //La valeur deviens false dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[LigneCellule, ColonneCellule] = false;
                        }
                    }
                }
                //Condition permettent de cloner le tableau prochaine génération dans le tableau à afficher
                if (ProchaineGeneration.UtilisationTerrain != null)
                {
                    TerrainDuJeu.UtilisationTerrain = (bool[,])ProchaineGeneration.UtilisationTerrain.Clone();
                }
                TerrainDuJeu.AffichageTerrain(VitesseJeu);
                //Affiche le nombre de génération ainsi que la vitesse du jeu en dessous de chaque génération
                Console.WriteLine("Nombre de génération : " + (k + 1));
                Console.WriteLine("Pour augmenter la vitesse de jeu, appuyez sur la flèche du bas. Pour la diminuer, appuyez sur celle du haut !");
                Console.WriteLine("Vitesse du jeu : " + VitesseJeu);
            }
        }

        /// <summary>
        /// Méthode permettent la modification de la vitesse de jeu
        /// </summary>
        public void ModificationVitesse()
        {
            int vitesseMax = 10;
            float vitesseMin = 0.1F;

            //Condition si la touche est Flèche du bas 
            if(Console.ReadKey().Key == ConsoleKey.DownArrow)
            {
                //Condition quand la vitesse est entre 10 et 1
                if (VitesseJeu < vitesseMax && VitesseJeu >= 1)
                {
                    VitesseJeu += 1;
                }
                //Condition quand la vitesse est entre 0,10 et 1
                else if(VitesseJeu <= 1)
                {
                    VitesseJeu += vitesseMin;
                }

            }
            //Condition si la touche est Flèche du bas
            else if (Console.ReadKey().Key == ConsoleKey.UpArrow)
            {
                //Condition quand la vitesse est entre 0,10 et 1
                if (VitesseJeu > vitesseMin && VitesseJeu <= 1)
                {
                    VitesseJeu -= vitesseMin;
                }
                //Condition quand la vitesse est entre 1 et 10
                else if(VitesseJeu >1)
                {
                    VitesseJeu -= 1;
                }
            }
        }

        public Terrain GetTerrainDuJeu
        {
            get { return GetTerrainDuJeu; }
        }
    }
}