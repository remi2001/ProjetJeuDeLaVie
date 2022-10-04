using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetJeuDeLaVie
{
    internal class Terrain
    {
        private bool[,] terrain = new bool[100, 100];
        private Random aleatoire = new Random();

        //Variable du pourcentage d'apparitions des cellules pouvant etre changer
        private int pourcentage;

        private int longueur;
        private int hauteur;
        private int nb_cellule;

        public Terrain(int pourcentage_saisie)
        {
            this.pourcentage = pourcentage_saisie;
            this.longueur = 0;
            this.hauteur = 0;
            this.nb_cellule = 0;

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
        }

        public void Affichage_du_terrain()
        {
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

        public bool[,] UtilisationTerrain
        {
            get { return terrain; }
            set { terrain = value; }
        }
    }
}
