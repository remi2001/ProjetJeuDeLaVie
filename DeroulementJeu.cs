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
        private int ComptCellAutour(Terrain terrain, int i ,int j)
        {
            int nbCellule = 0;

            if (i == 0 || j == 0 || i == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 || j == TerrainDuJeu.UtilisationTerrain.GetLength(1)-1)
            {
                if((i == 0 && (j != 0 && j != TerrainDuJeu.UtilisationTerrain.GetLength(1)-1))|| (i == TerrainDuJeu.UtilisationTerrain.GetLength(0) -1 && (j != 0 && j != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)) || (j == 0 && (i != 0 && i != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)) || (j == TerrainDuJeu.UtilisationTerrain.GetLength(1)-1 && (i != 0 && i != TerrainDuJeu.UtilisationTerrain.GetLength(1)-1)))
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
            if (i == 0 && j != 0 && j != TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[i, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j + 1] == true) nbCellule++;
            }
            if (i == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 && j != 0 && j != TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
            }
            if (j == 0 && i != 0 && i != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)
            {
                if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j + 1] == true) nbCellule++;
            }
            if (j == TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1 && i != 0 && i != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)
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
            if (i == 0 && j == TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[i, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i + 1, j] == true) nbCellule++;
            }
            if (i == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 && j == 0)
            {
                if (terrain.UtilisationTerrain[i - 1, j] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i, j + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[i - 1, j + 1] == true) nbCellule++;
            }
            if (i == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 && j == TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
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
        public void DeroulementNormal()
        {
            Terrain ProchaineGeneration = new Terrain(0, TerrainDuJeu.UtilisationTerrain.GetLength(0), TerrainDuJeu.UtilisationTerrain.GetLength(1));
            for (int k = 0; k < NbGeneration; k++)
            {
                ProchaineGeneration.InitialisationTerrain();
                int nbcellule;
                for (int i = 0; i < TerrainDuJeu.UtilisationTerrain.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1; j++)
                    {
                        nbcellule = ComptCellAutour(TerrainDuJeu, i, j);
                        //Condition pour la naissance d'une cellule
                        /*
                        if (nbcellule == 3 && terrain.UtilisationTerrain[i, j] == false)
                        {
                            //La valeur deviens true dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[i, j] = true;
                        }*/
                        //Obligatoire sinon les cellules repasse a faux car ce n'est pas le meme tableau
                        //Naissance et survie d'un cellule
                        if(nbcellule == 3 || (nbcellule == 2 && TerrainDuJeu.UtilisationTerrain[i, j] == true))
                        {
                            ProchaineGeneration.UtilisationTerrain[i, j] = true;
                        }
                        //Condition pour la mort d'une cellule
                        if ((nbcellule <= 1 || nbcellule >= 4 ) && (TerrainDuJeu.UtilisationTerrain[i, j] == true))
                        {
                            //La valeur deviens false dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[i, j] = false;
                        }
                    }
                }
                if (ProchaineGeneration.UtilisationTerrain != null)
                {
                    TerrainDuJeu.UtilisationTerrain = (bool[,])ProchaineGeneration.UtilisationTerrain.Clone();
                }
                TerrainDuJeu.Affichage_du_terrain();
            }
        }

        /// <summary>
        /// Déroulement des règles du jeu en mode Day and Night
        /// </summary>
        public void DeroulementDayAndNight()
        {
            int nbcellule;
            Terrain ProchaineGeneration = new Terrain(0, TerrainDuJeu.UtilisationTerrain.GetLength(0), TerrainDuJeu.UtilisationTerrain.GetLength(1));
            for (int k = 0; k < NbGeneration; k++)
            {
                for (int i = 0; i < TerrainDuJeu.UtilisationTerrain.GetLength(0) - 1; i++)
                {
                    ProchaineGeneration.InitialisationTerrain();
                    for (int j = 0; j < TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1; j++)
                    {
                        nbcellule = ComptCellAutour(TerrainDuJeu, i, j);
                        //Condition pour la naissance d'une cellule
                        if ((nbcellule == 3 || nbcellule == 4 || nbcellule == 6 || nbcellule == 7 || nbcellule == 8) && (TerrainDuJeu.UtilisationTerrain[i, j] == false))
                        {
                            //La valeur deviens true dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[i, j] = true;
                        }
                        //Condition pour la mort d'une cellule
                        if ((nbcellule == 0 || nbcellule == 1 || nbcellule == 2 || nbcellule == 4 || nbcellule == 5) && (TerrainDuJeu.UtilisationTerrain[i, j] == true))
                        {
                            //La valeur deviens false dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[i, j] = true;
                        }
                    }
                    if (ProchaineGeneration.UtilisationTerrain != null)
                    {
                        TerrainDuJeu.UtilisationTerrain = (bool[,])ProchaineGeneration.UtilisationTerrain.Clone();
                    }
                    TerrainDuJeu.Affichage_du_terrain();
                }
            }
        }

        public Terrain GetTerrainDuJeu
        {
            get { return GetTerrainDuJeu; }
        }
    }
}
