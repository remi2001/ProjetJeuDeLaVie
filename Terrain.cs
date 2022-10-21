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

        public Terrain(int pourcentage_saisie, int nombreLigne, int nombreColonne)
        {
            this.pourcentage = pourcentage_saisie;
            this.NombreLigne = nombreLigne;
            this.NombreColonne = nombreColonne;
            this.nb_cellule = 0;

            terrain = new bool[NombreLigne, NombreColonne];
        }

        public void InitialisationTerrain()
        {
            //initialisation du tableau
            for (int LigneCellule = 0; LigneCellule < terrain.GetLength(0); LigneCellule++)
            {
                for (int ColonneCellule = 0; ColonneCellule < terrain.GetLength(1); ColonneCellule++)
                {
                    terrain[LigneCellule, ColonneCellule] = false;
                }
            }
        }

        public void GestionApparitionCellule()
        {
            //Gestion d'apparition des cellules selon le pourcentage et de facon aléatoire
            while (nb_cellule < (pourcentage * 100))
            {
                NombreLigne = aleatoire.Next(100);
                NombreColonne = aleatoire.Next(100);

                if (terrain[NombreLigne, NombreColonne] == false)
                {
                    terrain[NombreLigne, NombreColonne] = true;
                    nb_cellule++;
                }
            }
        }

        public void AffichageTerrain()
        {
            //Temps d'attente entre 2 affichage de 1 seconde qui equivaut donc a la vitesse du jeu
            Thread.Sleep(1000);

            Console.Clear();
            for (int LigneCellule = 0; LigneCellule < terrain.GetLength(0); LigneCellule++)
            {
                for (int ColonneCellule = 0; ColonneCellule < terrain.GetLength(1); ColonneCellule++)
                {
                    if (terrain[LigneCellule, ColonneCellule] == true) Console.Write("X");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public void ModifTerrainParUtilisateur()
        {
            int Ligne = 0;
            Char Reponse;
            bool MauvaiseSaisie=true;

            //Nettoyage de la console
            Console.Clear();

            //Affichage du terrain actuel
            for (int LigneCellule = 0; LigneCellule < terrain.GetLength(0); LigneCellule++)
            {
                Console.Write(Ligne+"\t|");
                Ligne++;
                for (int ColonneCellule = 0; ColonneCellule < terrain.GetLength(1); ColonneCellule++)
                {                    
                    if (terrain[LigneCellule, ColonneCellule] == true) Console.Write("X|");
                    else Console.Write(" |");
                }
                Console.WriteLine();
            }

            //Saisie su choix de la modification ou non du terrain
            Console.WriteLine("Voulez-vous modifier le tableau ci-dessus avant le début du jeu ? (O/N)");
            do
            {
                if (!Char.TryParse(Console.ReadLine(), out Reponse))
                {
                    Console.WriteLine("Veuillez saisir uniquement O si vous souhaité réalisé une modification sinon uniquement N");
                }
                else
                {
                    MauvaiseSaisie=false;
                }
            } while (MauvaiseSaisie==true);
        }

        public bool[,] UtilisationTerrain
        {
            get { return terrain; }
            set { terrain = value; }
        }
    }
}
