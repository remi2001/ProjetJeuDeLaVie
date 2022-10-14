using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetJeuDeLaVie
{
    internal class Terrain
    {

        private bool[,] terrain;
        private Random aleatoire = new Random();

        //Variable du pourcentage d'apparitions des cellules pouvant etre changer
        private int pourcentage;

        private int NombreLigne;
        private int NombreColonne;
        private int nb_cellule;

        public Terrain(int pourcentage_saisie)
        {
            this.pourcentage = pourcentage_saisie;
            this.NombreLigne = 100;
            this.NombreColonne = 100;
            this.nb_cellule = 0;

            terrain = new bool[NombreLigne, NombreColonne];
        }

        public void InitialisationTerrain()
        {
            //initialisation du tableau
            for (int i = 0; i < terrain.GetLength(0); i++)
            {
                for (int j = 0; j < terrain.GetLength(1); j++)
                {
                    terrain[i, j] = false;
                }
            }
        }

        public void GestionApparitionCellule()
        {
            //Gestion d'apparition des cellules selon le pourcentage et de facon aléatoire
            while (nb_cellule < (pourcentage * 100))
            {
                NombreLigne = aleatoire.Next(99);
                NombreColonne = aleatoire.Next(99);

                if (terrain[NombreLigne, NombreColonne] == false)
                {
                    terrain[NombreLigne, NombreColonne] = true;
                    nb_cellule++;
                }
                else
                {
                    while (terrain[NombreLigne, NombreColonne] == true)
                    {
                        NombreLigne++;
                        if (NombreLigne == terrain.GetLength(0))
                        {
                            NombreLigne = 0;
                            NombreColonne++;
                            if (NombreColonne == terrain.GetLength(1))
                            {
                                NombreColonne = 0;
                                NombreLigne = 0;
                            }
                        }
                    }
                    terrain[NombreLigne, NombreColonne] = true;
                    nb_cellule++;
                }
            }
        }

        public void Affichage_du_terrain()
        {
            //Temps d'attente entre 2 affichage de 10 seconde
            //Thread.Sleep(10000);
            //Ou taper sur entrer pour passer a la génération suivante
            Console.ReadLine();

            Console.Clear();
            for (int i = 0; i < terrain.GetLength(0); i++)
            {
                for (int j = 0; j < terrain.GetLength(1); j++)
                {
                    if (terrain[i, j] == true) Console.Write("X");
                    else Console.Write(" ");
                }
                Console.WriteLine("");
            }
        }

        public bool[,] UtilisationTerrain
        {
            get { return terrain; }
            set { terrain = value; }
        }
    }
}
