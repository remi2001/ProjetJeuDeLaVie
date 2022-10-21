using System;

namespace ProjetJeuDeLaVie
{
    class Program
    {
        #region Attribut
        private static List<Menu> MenuPouvantEtreENTRER;
        private static Menu FEN_MenuPrincipale;
        private static Menu FEN_MenuOption;
        private static bool SiLancer;
        private static bool termine;
        private static Terrain? TerrainDuJeu;

        //Valeur paramètre par défaut
        private static int Pourcentage;
        private static int NombreLigne;
        private static int NombreColonne;
        private static int NbGeneration;
        #endregion

        static void Main(String[] args)
        {
            //Par défaut
            Pourcentage = 60;
            NombreLigne = 100;
            NombreColonne = 100;
            NbGeneration = 30;
            termine = false;

            Console.CursorVisible = false;
            Console.Title = "Jeu de la vie";

            CreationMenu();

            do
            {
                Console.Clear();
                SiLancer = false;

                //Tant que l'utilisateur n'a pas lancer le jeu
                do
                {
                    SiLancer = MenuPrincipal();
                }while (!SiLancer);

                Console.Clear();
                Console.WriteLine("LANCEMENT");
                Thread.Sleep(1000);
                Console.Clear();

                //On crée entièrement le terrain
                TerrainDuJeu = CreationTerrainJeu();

                //Crée le jeu
                DeroulementJeu JeuDeLaVie = new DeroulementJeu(Pourcentage, NbGeneration, TerrainDuJeu);

                //L'utilisateur sélectionne le jeu
                SelectionJeu(JeuDeLaVie);
            } while (!termine);

           
        }

        #region Fonction

        /// <summary>
        /// 
        /// </summary>
        public static void CreationMenu()
        {
            FEN_MenuPrincipale = new Menu(CreationSousMenuPrincipal(), 1,
                "Voici le menu. Pour naviguer, appuyez sur les flèches haut et bas. Pour valider votre sélection appuyez sur ENTER");

            MenuPouvantEtreENTRER = new(1)
            {
                FEN_MenuPrincipale
            };

            FEN_MenuOption = new Menu(CreationSousMenuOption(), 2, "Voici les options Modifiables");
            MenuPouvantEtreENTRER.Add(FEN_MenuOption);

        }
        
        /// <summary>
        /// 
        /// </summary>
        public static List<SousMenu> CreationSousMenuPrincipal()
        {
            SousMenu FEN_Lancer = new SousMenu(0, "Lancer");
            SousMenu FEN_Option = new SousMenu(1, "Option");
            SousMenu FEN_Quitter = new SousMenu(2, "Quitter");

            List<SousMenu> ListeSousMenuPrincipale = new List<SousMenu>(3)
            {
                FEN_Lancer,
                FEN_Option,
                FEN_Quitter,
            };

            return ListeSousMenuPrincipale;
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<SousMenu> CreationSousMenuOption()
        {
            SousMenu FEN_OptionPourcentage = new SousMenu(0, "Le pourcentage de cellule vivante (actuel : " + Pourcentage + " )");
            SousMenu FEN_OptionVitesseJeu = new SousMenu(1, "La vitesse du jeu (pas encore implémenter)");
            SousMenu FEN_OptionGenFinal = new SousMenu(2, "Le choix de la génération final (actuel : " + NbGeneration + " )");
            SousMenu FEN_OptionNombreLigne = new SousMenu(3, "Le nombre de ligne du terrain (actuel : " + NombreLigne + ")");
            SousMenu FEN_OptionNombreColonne = new SousMenu(4, "Le nombre de colonne du terrain (actuel : " + NombreColonne + ")");

            List<SousMenu> ListeSousMenuOption = new List<SousMenu>(5)
            {
                FEN_OptionPourcentage,
                FEN_OptionVitesseJeu,
                FEN_OptionGenFinal,
                FEN_OptionNombreLigne,
                FEN_OptionNombreColonne
            };

            return ListeSousMenuOption;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool MenuPrincipal()
        {
            byte PositionCurseur;

            //On signale au programme que l'on ne veut pas lancer le jeu
            SiLancer = false;

            FEN_MenuPrincipale.ToString();

            //On permet la naviguation du menu principale à l'utilisateur
            PositionCurseur = FEN_MenuPrincipale.Naviguer();

            //On sélectionne le sous menu choisi par l'utilisateur dans le menu principal
            FEN_MenuPrincipale.SelectionSousMenu(PositionCurseur);

            return SiLancer;
        }

        /// <summary>
        /// Initialise le menu des options et Affiche les options du jeu
        /// </summary
        public static void OptionJeu()
        {
            byte PositionCurseur;

            Console.Clear();

            FEN_MenuOption.ToString();

            //On donne la possiblité de naviguer dans le menu des options
            PositionCurseur = FEN_MenuOption.Naviguer();

            //Si le curseur n'est pas égal au code du retour en arrière
            if (PositionCurseur != 255)
            {
                //On sélectionne le sous menu choisi par l'utilisateur dans le menu option
                FEN_MenuOption.SelectionSousMenu(PositionCurseur);
            }
            else
            {
                //On remet le curseur à la position des options, c'est à dire, 1
                FEN_MenuPrincipale.SetCurseur = 1;
            }
            Console.Clear();
        }

        /// <summary>
        /// Quitte le jeu, c'est à dire, le programme
        /// </summary>
        public static void QuitterJeu()
        {
            Environment.Exit(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Terrain CreationTerrainJeu()
        {
            //Création du terrain
            Terrain Terrain_du_Jeu = new Terrain(Pourcentage, NombreColonne, NombreLigne);
            Terrain_du_Jeu.InitialisationTerrain();
            Terrain_du_Jeu.GestionApparitionCellule();
            Terrain_du_Jeu.ModifTerrainParUtilisateur();
            //----------------------------------------------------------------------------------------//

            return Terrain_du_Jeu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="JeuDeLaVie"></param>
        public static void SelectionJeu(DeroulementJeu JeuDeLaVie)
        {
            byte ModeDeJeu = 1;

            if(ModeDeJeu == 1)
            {
                JeuDeLaVie.DeroulementJeuNormal();
            }

            if (ModeDeJeu == 2)
            {
                JeuDeLaVie.DeroulementJeuDayAndNight();
            }

            termine = true;
        }
        #endregion

        #region Accesseur 
        public static int GetPourcentage
        {
            get { return Pourcentage; }
        }
        public static int SetPourcentage
        {
            set => Pourcentage = value;
        }
        public static int GetNombreColonne
        {
            get { return NombreColonne; }
        }
        public static int SetNombreColonne
        {
            set => NombreColonne = value;
        }
        public static int GetNombreLigne
        {
            get { return NombreLigne; }
        }
        public static int SetNombreLigne
        {
            set => NombreLigne = value;
        }
        public static int GetNbGeneration
        {
            get { return NbGeneration; }
        }
        public static int SetNbGeneration
        {
            set => NbGeneration = value;
        }

        public static bool SetSiLancer
        {
            set => SiLancer = value;
        }
        #endregion
    }
}