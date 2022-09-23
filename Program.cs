using System;

namespace ProjetJeuDeLaVie
{
    class Program
    {
        static void Main(String[] args)
        {
            bool[,] terrain = new bool[100, 100];
            Random aleatoire = new Random();

            //Variable du pourcentage d'apparitions des cellules pouvant etre changer
            int pourcentage = 14;

            int longueur = 0;
            int hauteur = 0;
            int nb_cellule= 0;

            //initialisation du tableau
            for(int i=0; i < 100; i++)
            {
                for(int j=0; j < 100; j++)
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