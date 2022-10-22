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
            while (nb_cellule < (pourcentage * ((terrain.GetLength(0)*terrain.GetLength(1))/100)))
            {
                NombreLigne = aleatoire.Next(terrain.GetLength(0));
                NombreColonne = aleatoire.Next(terrain.GetLength(1));

                if (terrain[NombreLigne, NombreColonne] == false)
                {
                    terrain[NombreLigne, NombreColonne] = true;
                    nb_cellule++;
                }
            }
        }

        public void AffichageTerrain(float VitesseJeu)
        {
            int Delai = Convert.ToInt32(1000 * VitesseJeu);
            //Temps d'attente entre 2 affichage donc a la vitesse du jeu
            Thread.Sleep(Delai);

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
            int LigneCelluleChoisi, ColonneCelluleChoisi;
            bool ModifUtilisateur = true;

            do
            {
                int Ligne = 1;
                Char Reponse=' ', ReponseFinale=' ';
                bool MauvaiseSaisie = true, MauvaiseSaisieLigne = true, MauvaiseSaisieColonne = true, MauvaiseSaisieFinale = true;

                //Nettoyage de la console
                Console.Clear();

                //Affichage du terrain actuel
                for (int LigneCellule = 0; LigneCellule < terrain.GetLength(0); LigneCellule++)
                {
                    Console.Write(Ligne + "\t|");
                    Ligne++;
                    for (int ColonneCellule = 0; ColonneCellule < terrain.GetLength(1); ColonneCellule++)
                    {
                        if (terrain[LigneCellule, ColonneCellule] == true) Console.Write("X|");
                        else Console.Write(" |");
                    }
                    Console.WriteLine();
                }

                //Saisie au choix de la modification ou non du terrain
                Console.WriteLine("Voulez-vous modifier le tableau ci-dessus avant le début du jeu ? (O/N)");
                do
                {
                    if (!Char.TryParse(Console.ReadLine(), out Reponse))
                        Console.WriteLine("Veuillez entrer O si vous souhaité modifier une cellule sinon entrer N");
                    else
                    {
                        if (Reponse != 'O' && Reponse != 'N')
                            Console.WriteLine("Veullez entrer O si vous souhaité modifier une cellule sinon entrer N");
                        else
                            MauvaiseSaisie = false;

                    }
                }
                while (MauvaiseSaisie == true);

                if (Reponse == 'O')
                {
                    //Saisie de la ligne de la cellule a modif
                    Console.WriteLine("Veuillez saisir la ligne de la cellule que vous souhaité modifier entre 1 et " + (terrain.GetLength(0)));
                    do
                    {
                        if (!Int32.TryParse(Console.ReadLine(), out LigneCelluleChoisi))
                            Console.WriteLine("Veuillez saisir la ligne de la cellule que vous souhaité modifier entre 1 et " + (terrain.GetLength(0)));
                        else
                        {
                            if (LigneCelluleChoisi <= terrain.GetLength(0) && LigneCelluleChoisi > 0)
                                MauvaiseSaisieLigne = false;
                            else
                                Console.WriteLine("Veuillez saisir la ligne de la cellule que vous souhaité modifier entre 1 et " + (terrain.GetLength(0)));
                        }
                    } while (MauvaiseSaisieLigne == true);

                    Console.WriteLine("Veuillez saisir la colonne de la cellule que vous souhaité modifier entre 1 et " + (terrain.GetLength(1)));
                    //Saisie de la colonne de la cellule a modif
                    do
                    {
                        if (!Int32.TryParse(Console.ReadLine(), out ColonneCelluleChoisi))
                            Console.WriteLine("Veuillez saisir la colonne de la cellule que vous souhaité modifier entre 1 et " + (terrain.GetLength(1)));
                        else
                        {
                            if (ColonneCelluleChoisi <= terrain.GetLength(1) && ColonneCelluleChoisi > 0)
                                MauvaiseSaisieColonne = false;
                            else
                                Console.WriteLine("Veuillez saisir la colonne de la cellule que vous souhaité modifier entre 1 et " + (terrain.GetLength(1)));
                        }
                    } while (MauvaiseSaisieColonne == true);

                    ColonneCelluleChoisi--;
                    LigneCelluleChoisi--;


                    //Affichage de la ligne avec la potentiale modif symboliser par ? + indication de son etat
                    Console.Clear();
                    Console.WriteLine("Vous avez séléctionné la ligne {0} et la colonne {1} corespondante a la cellule avec ? ci-dessous", LigneCelluleChoisi, ColonneCelluleChoisi);
                    Console.Write("|");
                    for (int ColonneCellule = 0; ColonneCellule < terrain.GetLength(1); ColonneCellule++)
                    {
                        if (ColonneCellule == ColonneCelluleChoisi) Console.Write("?|");
                        else
                        {
                            if (terrain[LigneCelluleChoisi, ColonneCellule] == true) Console.Write("X|");
                            else Console.Write(" |");
                        }
                    }
                    Console.WriteLine("");
                    if (terrain[LigneCelluleChoisi, ColonneCelluleChoisi] == true)
                        Console.WriteLine("Cette cellule est actuellement vivante souhaité-vous donc la modifié ?(O/N)");
                    else
                        Console.WriteLine("Cette cellule est actuellement morte souhaité-vous donc la modifié ?(O/N)");

                    //Demande si l'utilisateur veut faire la modif
                    do
                    {
                        if (!Char.TryParse(Console.ReadLine(), out ReponseFinale))
                            Console.WriteLine("Veuillez entrer O si vous souhaité modifier cette cellule sinon entrer N");
                        else
                        {
                            if (ReponseFinale != 'O' && ReponseFinale != 'N')
                                Console.WriteLine("Veullez entrer O si vous souhaité modifier cette cellule sinon entrer N");
                            else
                                MauvaiseSaisieFinale = false;

                        }
                    }
                    while (MauvaiseSaisieFinale == true);

                    if (ReponseFinale == 'O')
                    {
                        if (terrain[LigneCelluleChoisi, ColonneCelluleChoisi] == true)
                            terrain[LigneCelluleChoisi, ColonneCelluleChoisi] = false;
                        else
                            terrain[LigneCelluleChoisi, ColonneCelluleChoisi] = true;
                    }
                }
                else
                    ModifUtilisateur = false;
            } while (ModifUtilisateur == true);
        }

        public bool[,] UtilisationTerrain
        {
            get { return terrain; }
            set { terrain = value; }
        }
    }
}
