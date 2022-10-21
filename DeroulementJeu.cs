using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetJeuDeLaVie
{
    internal class DeroulementJeu
    {
        //Paramètre du jeu
        private int Pourcentage;
        private int NbGeneration;

        private Terrain TerrainDuJeu;

        public DeroulementJeu(int pourcentage, int nbGeneration, Terrain terrainDuJeu)
        {
            Pourcentage = pourcentage;
            NbGeneration = nbGeneration;
            TerrainDuJeu = terrainDuJeu;
        }

        /// <summary>
        /// Compte le nombre de cellule autour de celle rentré en paramètre
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

            return nbCellule;
        }

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
            TerrainDuJeu.AffichageTerrain();

            Terrain ProchaineGeneration = new Terrain(0, TerrainDuJeu.UtilisationTerrain.GetLength(0), TerrainDuJeu.UtilisationTerrain.GetLength(1));
            for (int k = 0; k < NbGeneration; k++)
            {
                ProchaineGeneration.InitialisationTerrain();
                int nbcellule;
                for (int LigneCellule = 0; LigneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(0); LigneCellule++)
                {
                    for (int ColonneCellule = 0; ColonneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(1); ColonneCellule++)
                    {
                        nbcellule = ComptageCelluleAutour(TerrainDuJeu, LigneCellule, ColonneCellule);
                        //Naissance et survie d'un cellule
                        if(nbcellule == 3 || (nbcellule == 2 && TerrainDuJeu.UtilisationTerrain[LigneCellule, ColonneCellule] == true))
                        {
                            ProchaineGeneration.UtilisationTerrain[LigneCellule, ColonneCellule] = true;
                        }
                        //Condition pour la mort d'une cellule
                        if ((nbcellule <= 1 || nbcellule >= 4 ) && (TerrainDuJeu.UtilisationTerrain[LigneCellule, ColonneCellule] == true))
                        {
                            //La valeur deviens false dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[LigneCellule, ColonneCellule] = false;
                        }
                    }
                }
                if (ProchaineGeneration.UtilisationTerrain != null)
                {
                    TerrainDuJeu.UtilisationTerrain = (bool[,])ProchaineGeneration.UtilisationTerrain.Clone();
                }
                TerrainDuJeu.AffichageTerrain();
            }
        }

        /// <summary>
        /// Déroulement des règles du jeu en mode Day and Night
        /// </summary>
        public void DeroulementJeuDayAndNight()
        {
            //Affichage du terrain de base
            TerrainDuJeu.AffichageTerrain();

            int nbcellule;
            Terrain ProchaineGeneration = new Terrain(0, TerrainDuJeu.UtilisationTerrain.GetLength(0), TerrainDuJeu.UtilisationTerrain.GetLength(1));
            for (int k = 0; k < NbGeneration; k++)
            {
                for (int LigneCellule = 0; LigneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(0); LigneCellule++)
                {
                    ProchaineGeneration.InitialisationTerrain();
                    for (int ColonneCellule = 0; ColonneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(1); ColonneCellule++)
                    {
                        nbcellule = ComptageCelluleAutour(TerrainDuJeu, LigneCellule, ColonneCellule);
                        //A regarder pour ajouter survie
                        //Condition pour la naissance d'une cellule
                        if ((nbcellule == 3 || nbcellule == 4 || nbcellule == 6 || nbcellule == 7 || nbcellule == 8) && (TerrainDuJeu.UtilisationTerrain[LigneCellule, ColonneCellule] == false))
                        {
                            //La valeur deviens true dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[LigneCellule, ColonneCellule] = true;
                        }
                        //Condition pour la mort d'une cellule
                        if ((nbcellule == 0 || nbcellule == 1 || nbcellule == 2 || nbcellule == 4 || nbcellule == 5) && (TerrainDuJeu.UtilisationTerrain[LigneCellule, ColonneCellule] == true))
                        {
                            //La valeur deviens false dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[LigneCellule, ColonneCellule] = true;
                        }
                    }
                    if (ProchaineGeneration.UtilisationTerrain != null)
                    {
                        TerrainDuJeu.UtilisationTerrain = (bool[,])ProchaineGeneration.UtilisationTerrain.Clone();
                    }
                    TerrainDuJeu.AffichageTerrain();
                }
            }
        }

        public Terrain GetTerrainDuJeu
        {
            get { return GetTerrainDuJeu; }
        }
    }
}
