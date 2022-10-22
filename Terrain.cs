using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetJeuDeLaVie
{
    internal class Terrain
    {
        #region Attributs
        private bool[,] terrain;
        private Random aleatoire = new Random();
        private int pourcentage;

        private int NombreLigne;
        private int NombreColonne;
        private int nb_cellule;
        #endregion

        #region Constructeur

        /// <summary>
        /// Construit un Terrain pour le jeu lorsque cela est nécessaire
        /// </summary>
        /// <param name="pourcentage_saisie"></param>
        /// <param name="nombreLigne"></param>
        /// <param name="nombreColonne"></param>
        public Terrain(int pourcentage_saisie, int nombreLigne, int nombreColonne)
        {
            this.pourcentage = pourcentage_saisie;
            this.NombreLigne = nombreLigne;
            this.NombreColonne = nombreColonne;
            this.nb_cellule = 0;

            terrain = new bool[NombreLigne, NombreColonne];
        }

        #endregion

        #region Fonction

        /// <summary>
        /// Initialisation de toutes les valeurs du terrain a faux (correspondant au cellules mortes) lors de l'appel de cette fonction
        /// </summary>
        public void InitialisationTerrain()
        {
            for (int LigneCellule = 0; LigneCellule < terrain.GetLength(0); LigneCellule++)
            {
                for (int ColonneCellule = 0; ColonneCellule < terrain.GetLength(1); ColonneCellule++)
                {
                    terrain[LigneCellule, ColonneCellule] = false;
                }
            }
        }

        /// <summary>
        /// Création des cellules vivantes de facon aléatoires et selon le pourcentage choisi dans le terrain qui appelle cette fonction
        /// </summary>
        public void GestionApparitionCellule()
        {
            //On réexecute cela tant que le pourcentage de cellules vivantes par rapport a la taille du terrain est inférieur
            while (nb_cellule < (pourcentage * ((terrain.GetLength(0)*terrain.GetLength(1))/100)))
            {
                //Choix d'une ligne et d'une colonne aléatoire entre les bornes du terrain
                NombreLigne = aleatoire.Next(terrain.GetLength(0));
                NombreColonne = aleatoire.Next(terrain.GetLength(1));

                //La cellule choisi aléatoirement devient vivante uniquement si elle n'etait pas déja devenu vivante
                if (terrain[NombreLigne, NombreColonne] == false)
                {
                    terrain[NombreLigne, NombreColonne] = true;
                    nb_cellule++;
                }
            }
        }

        /// <summary>
        /// Affichage du terrain apppelant cette fonction
        /// </summary>
        /// <param name="VitesseJeu"></param>
        public void AffichageTerrain(float VitesseJeu)
        {
            //Temps d'attente entre 2 affichages de terrain
            int Delai = Convert.ToInt32(1000 * VitesseJeu);
            Thread.Sleep(Delai);

            //Affichage du terrain
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

        /// <summary>
        /// Modification, si l'utilisateur le desir, de l'etat d'une ou plusieurs cellules du terrain choisi par l'utilisateur avant le lancement du jeu
        /// </summary>
        public void ModifTerrainParUtilisateur()
        {
            //Ligne et Colonne qui representront les coordonées de la cellule choisi par l'utilisateur
            int LigneCelluleChoisi, ColonneCelluleChoisi;

            //Si cette variable passe a faux l'utilisateur ne veut pas ou plus modifier alors l'on quitte cette fonction
            bool ModifUtilisateur = true;

            //Permet de savoir si c'est la premier modification ou pas de l'utilisateur
            bool PlusieursModif=false;

            do
            {
                //Represente les lignes du tableau
                int Ligne = 1;

                //Represente les réponse qui seront entrés par l'utilisateur
                Char Reponse=' ', ReponseFinale=' ';

                //On suppose que l'utilisateur fais une mauvaise saisie si c'est le cas un message affichage sinon la variables correpondates passe a faux
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
                if (PlusieursModif)
                    Console.WriteLine("Voulez-vous encore modifier le tableau ci-dessus avant le début du jeu ? (O/N)");
                else
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

                //L'utilisateur veut Modifier une cellule ici
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

                    //Saisie de la colonne de la cellule a modif
                    Console.WriteLine("Veuillez saisir la colonne de la cellule que vous souhaité modifier entre 1 et " + (terrain.GetLength(1)));
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

                    //Recapitulatif de la sélection et informations que la potentiel modification concerne la cellule avec ?
                    Console.Clear();
                    Console.WriteLine("Vous avez séléctionné la ligne {0} et la colonne {1} corespondante a la cellule avec ? ci-dessous", LigneCelluleChoisi, ColonneCelluleChoisi);

                    //Ceci permet de remettre les coordonées corespondantes au tableau entre 0 et n et non entre 1 et n+1
                    ColonneCelluleChoisi--;
                    LigneCelluleChoisi--;

                    //Affichage de la ligne séléctionné avec ? pour symboliser la potentiel modificatioon
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

                    //Indication de l'etat de la cellule séléctionné avant la modification et demande a l'utilisateur si il confirme cette modification
                    if (terrain[LigneCelluleChoisi, ColonneCelluleChoisi] == true)
                        Console.WriteLine("Cette cellule est actuellement vivante souhaité-vous donc la rendre morte ?(O/N)");
                    else
                        Console.WriteLine("Cette cellule est actuellement morte souhaité-vous donc la rendre vivante ?(O/N)");

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

                    //Si l'utilisateur confirme la cellule a séléctionné alors on change l'état de la cellule
                    if (ReponseFinale == 'O')
                    {
                        if (terrain[LigneCelluleChoisi, ColonneCelluleChoisi] == true)
                            terrain[LigneCelluleChoisi, ColonneCelluleChoisi] = false;
                        else
                            terrain[LigneCelluleChoisi, ColonneCelluleChoisi] = true;
                        PlusieursModif = true;
                    }
                }
                //L'utilisateur ne veut pas modifier
                else
                    ModifUtilisateur = false;
            } while (ModifUtilisateur == true);
        }

        #endregion


        #region Accesseur
        public bool[,] UtilisationTerrain
        {
            get { return terrain; }
            set { terrain = value; }
        }

        #endregion
    }
}
