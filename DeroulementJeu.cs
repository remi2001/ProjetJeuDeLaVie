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
        private int ComptCellAutour(Terrain terrain, int AbscisseCellule ,int OrdonneCellule)
        {
            int nbCellule = 0;

            if (AbscisseCellule == 0 || OrdonneCellule == 0 || AbscisseCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 || OrdonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1)-1)
            {
                if((AbscisseCellule == 0 && (OrdonneCellule != 0 && OrdonneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(1)-1))|| (AbscisseCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0) -1 && (OrdonneCellule != 0 && OrdonneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)) || (OrdonneCellule == 0 && (AbscisseCellule != 0 && AbscisseCellule != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)) || (OrdonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1)-1 && (AbscisseCellule != 0 && AbscisseCellule != TerrainDuJeu.UtilisationTerrain.GetLength(1)-1)))
                    nbCellule = ComptCellCote(terrain, AbscisseCellule, OrdonneCellule, nbCellule);
                else
                    nbCellule = ComptCellCoin(terrain, AbscisseCellule, OrdonneCellule, nbCellule);
            }
            else
            {
                nbCellule = ComptCellSansParticularite(terrain, AbscisseCellule, OrdonneCellule, nbCellule);
            }            

            return nbCellule;
        }

        private int ComptCellCote(Terrain terrain, int AbscisseCellule, int OrdonneCellule, int nbCellule)
        {
            if (AbscisseCellule == 0 && OrdonneCellule != 0 && OrdonneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule + 1] == true) nbCellule++;
            }
            if (AbscisseCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 && OrdonneCellule != 0 && OrdonneCellule != TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule + 1] == true) nbCellule++;
            }
            if (OrdonneCellule == 0 && AbscisseCellule != 0 && AbscisseCellule != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)
            {
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule + 1] == true) nbCellule++;
            }
            if (OrdonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1 && AbscisseCellule != 0 && AbscisseCellule != TerrainDuJeu.UtilisationTerrain.GetLength(0)-1)
            {
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule] == true) nbCellule++;
            }
            return nbCellule;
        }

        private int ComptCellCoin(Terrain terrain, int AbscisseCellule, int OrdonneCellule, int nbCellule)
        {
            if (AbscisseCellule == 0 && OrdonneCellule == 0)
            {
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule + 1] == true) nbCellule++;
            }
            if (AbscisseCellule == 0 && OrdonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule] == true) nbCellule++;
            }
            if (AbscisseCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 && OrdonneCellule == 0)
            {
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule + 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule + 1] == true) nbCellule++;
            }
            if (AbscisseCellule == TerrainDuJeu.UtilisationTerrain.GetLength(0)-1 && OrdonneCellule == TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1)
            {
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule - 1] == true) nbCellule++;
                if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule - 1] == true) nbCellule++;
            }
            return nbCellule;
        }

        private int ComptCellSansParticularite(Terrain terrain, int AbscisseCellule, int OrdonneCellule, int nbCellule)
        {
            if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule] == true) nbCellule++;
            if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule - 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule - 1] == true) nbCellule++;

            if (terrain.UtilisationTerrain[AbscisseCellule - 1, OrdonneCellule + 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule - 1] == true) nbCellule++;

            if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule] == true) nbCellule++;
            if (terrain.UtilisationTerrain[AbscisseCellule, OrdonneCellule + 1] == true) nbCellule++;
            if (terrain.UtilisationTerrain[AbscisseCellule + 1, OrdonneCellule + 1] == true) nbCellule++;
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
                for (int AbscisseCellule = 0; AbscisseCellule < TerrainDuJeu.UtilisationTerrain.GetLength(0) - 1; AbscisseCellule++)
                {
                    for (int OrdonneCellule = 0; OrdonneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1; OrdonneCellule++)
                    {
                        nbcellule = ComptCellAutour(TerrainDuJeu, AbscisseCellule, OrdonneCellule);
                        //Condition pour la naissance d'une cellule
                        /*
                        if (nbcellule == 3 && terrain.UtilisationTerrain[i, j] == false)
                        {
                            //La valeur deviens true dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[i, j] = true;
                        }*/
                        //Obligatoire sinon les cellules repasse a faux car ce n'est pas le meme tableau
                        //Naissance et survie d'un cellule
                        if(nbcellule == 3 || (nbcellule == 2 && TerrainDuJeu.UtilisationTerrain[AbscisseCellule, OrdonneCellule] == true))
                        {
                            ProchaineGeneration.UtilisationTerrain[AbscisseCellule, OrdonneCellule] = true;
                        }
                        //Condition pour la mort d'une cellule
                        if ((nbcellule <= 1 || nbcellule >= 4 ) && (TerrainDuJeu.UtilisationTerrain[AbscisseCellule, OrdonneCellule] == true))
                        {
                            //La valeur deviens false dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[AbscisseCellule, OrdonneCellule] = false;
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
                for (int AbscisseCellule = 0; AbscisseCellule < TerrainDuJeu.UtilisationTerrain.GetLength(0) - 1; AbscisseCellule++)
                {
                    ProchaineGeneration.InitialisationTerrain();
                    for (int OrdonneCellule = 0; OrdonneCellule < TerrainDuJeu.UtilisationTerrain.GetLength(1) - 1; OrdonneCellule++)
                    {
                        nbcellule = ComptCellAutour(TerrainDuJeu, AbscisseCellule, OrdonneCellule);
                        //Condition pour la naissance d'une cellule
                        if ((nbcellule == 3 || nbcellule == 4 || nbcellule == 6 || nbcellule == 7 || nbcellule == 8) && (TerrainDuJeu.UtilisationTerrain[AbscisseCellule, OrdonneCellule] == false))
                        {
                            //La valeur deviens true dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[AbscisseCellule, OrdonneCellule] = true;
                        }
                        //Condition pour la mort d'une cellule
                        if ((nbcellule == 0 || nbcellule == 1 || nbcellule == 2 || nbcellule == 4 || nbcellule == 5) && (TerrainDuJeu.UtilisationTerrain[AbscisseCellule, OrdonneCellule] == true))
                        {
                            //La valeur deviens false dans le tableau de la prochaine génération
                            ProchaineGeneration.UtilisationTerrain[AbscisseCellule, OrdonneCellule] = true;
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
