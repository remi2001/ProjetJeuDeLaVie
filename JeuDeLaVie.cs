using System;

namespace ProjetJeuDeLaVie
{
    class Program
    {
        #region Attribut
        /// <summary>
        /// Liste comportant tous les menus portant une naviguation
        /// </summary>
        private static List<Menu> MenuPouvantEtreENTRER;
        /// <summary>
        /// Menu principal
        /// </summary>
        private static Menu FEN_MenuPrincipale;
        /// <summary>
        /// Menu des options
        /// </summary>
        private static Menu FEN_MenuOption;
        /// <summary>
        /// Menu du lancement du jeu
        /// </summary>
        private static Menu FEN_MenuLancer;
        /// <summary>
        /// Valeur enregistrant si le jeu peut se lancer
        /// </summary>
        private static bool SiLancer;
        /// <summary>
        /// Valeur enregistrant si le jeu est terminé
        /// </summary>
        private static bool termine;
        /// <summary>
        /// Valeur enregistrant le mode de choisit
        /// </summary>
        private static byte ModeJeu;
        /// <summary>
        /// Le terrain du jeu
        /// </summary>
        private static Terrain? TerrainDuJeu;

        //Pramètre du jeu
        /// <summary>
        /// Pourcentage d'appartition des cellules dans le terrain
        /// </summary>
        private static int Pourcentage;
        /// <summary>
        /// Le nombre de ligne du terrain
        /// </summary>
        private static int NombreLigne;
        /// <summary>
        /// Le nombre de colonne du terrain
        /// </summary>
        private static int NombreColonne;
        /// <summary>
        /// Le nombre de génération du jeu
        /// </summary>
        private static int NbGeneration;
        /// <summary>
        /// Vitesse du jeu
        /// </summary>
        private static float VitesseJeu;
        #endregion

        static void Main(String[] args)
        {
            //Initialisation des paramètre par défaut
            Pourcentage = 60;
            NombreLigne = 100;
            NombreColonne = 100;
            NbGeneration = 30;
            termine = false;
            VitesseJeu = 1;
            
            //On cache le curseur de la console
            Console.CursorVisible = false;
            //Change le nom de la fenêtre de la console
            Console.Title = "Jeu de la vie";

            CreationMenu();

            do
            {
                Console.Clear();
                SiLancer = false;

                //Tant que l'utilisateur n'a pas lancer le jeu
                do
                {
                    MenuPrincipal();
                }while (!SiLancer);

                Console.Clear();
                Console.WriteLine("LANCEMENT");

                if(ModeJeu == 0)
                    Console.WriteLine("MODE NORMAL");
                if(ModeJeu == 1)
                    Console.WriteLine("DAY AND NIGHT");

                Thread.Sleep(1000);
                
                //On crée entièrement le terrain
                TerrainDuJeu = CreationTerrainJeu();

                //Création du jeu
                DeroulementJeu JeuDeLaVie = new DeroulementJeu(Pourcentage, NbGeneration, TerrainDuJeu, VitesseJeu);

                //L'utilisateur sélectionne le jeu
                SelectionJeu(JeuDeLaVie);
            } while (!termine);
        }

        #region Fonction

        /// <summary>
        /// Appelle tous les constructeur et les fonctions permettant la création de tous les menus
        /// </summary>
        public static void CreationMenu()
        {
            FEN_MenuPrincipale = new Menu(CreationSousMenuPrincipal(), 1,
                "Voici le menu. Pour naviguer, appuyez sur les flèches haut et bas. Pour valider votre sélection appuyez sur ENTER");

            MenuPouvantEtreENTRER = new(1)
            {
                FEN_MenuPrincipale
            };

            FEN_MenuOption = new Menu(CreationSousMenuOption(), 2, "Voici les options modifiables");
            MenuPouvantEtreENTRER.Add(FEN_MenuOption);

            FEN_MenuLancer = new Menu(CreationSousMenuLancer(), 3, "Sélectionnez le mode de jeu");
            MenuPouvantEtreENTRER.Add(FEN_MenuLancer);

        }

        /// <summary>
        /// Appelle plusieurs constructeurs de sous-menu pour créé le menu principal
        /// </summary>
        /// <returns>Retourne un tableau comportant tous les sous-menus crée</returns>
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
        /// Appelle plusieurs constructeurs de sous-menu pour créé le menu des options
        /// </summary>
        /// <returns>Retourne un tableau comportant tous les sous-menus crée</returns>
        public static List<SousMenu> CreationSousMenuOption()
        {
            SousMenu FEN_OptionPourcentage = new SousMenu(0, "Le pourcentage de cellule vivante (actuel : " + Pourcentage + " )");
            SousMenu FEN_OptionVitesseJeu = new SousMenu(1, "La vitesse du jeu (actuel : " + VitesseJeu + " )");
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
        /// Appelle plusieurs constructeurs de sous-menu pour créé le menu du lancement du jeu
        /// </summary>
        /// <returns>Retourne un tableau comportant tous les sous-menus crée</returns>
        public static List<SousMenu> CreationSousMenuLancer()
        {
            SousMenu FEN_ModeNormal = new SousMenu(0, "Mode normal");
            SousMenu FEN_ModeDayAndNight = new SousMenu(1, "Mode day and night");

            List<SousMenu> ListeSousMenuLancer = new List<SousMenu>(2)
            {
                FEN_ModeNormal,
                FEN_ModeDayAndNight
            };

            return ListeSousMenuLancer;
        }

        /// <summary>
        /// Affiche, rend possible la naviguation et sélectionne le sous-menu correspondant au menu principal
        /// </summary>
        public static void MenuPrincipal()
        {
            byte PositionCurseur;

            //On signale au programme que l'on ne veut pas lancer le jeu
            SiLancer = false;

            FEN_MenuPrincipale.ToString();

            //On permet la naviguation du menu principale à l'utilisateur
            PositionCurseur = FEN_MenuPrincipale.Naviguer();

            //On sélectionne le sous menu choisi par l'utilisateur dans le menu principal
            FEN_MenuPrincipale.SelectionSousMenu(PositionCurseur);

        }

        /// <summary>
        /// Affiche, rend possible la naviguation et sélectionne le sous-menu correspondant au menu du lancement du jeu
        /// </summary>
        public static void MenuLancer()
        {
            Console.Clear();

            FEN_MenuLancer.ToString();

            //On donne la possiblité de naviguer dans le menu des options
            ModeJeu = FEN_MenuLancer.Naviguer();

            //Signale au programme que le jeu peut se lancer
            SiLancer = true;

            Console.Clear();
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
        /// Permet de créer le terrain du jeu
        /// </summary>
        /// <returns>Retourne le terrain du jeu</returns>
        public static Terrain CreationTerrainJeu()
        {
            //Création du terrain
            Terrain Terrain_du_Jeu = new Terrain(Pourcentage, NombreLigne, NombreColonne);
            //Initalise le terrain
            Terrain_du_Jeu.InitialisationTerrain();
            //Fait apparaître les cellules selon le pourcentage d'apparition
            Terrain_du_Jeu.GestionApparitionCellule();
            //Donne la possibiliter de modifier le terrain à l'utilisateur
            Terrain_du_Jeu.ModifTerrainParUtilisateur();
            //----------------------------------------------------------------------------------------//

            return Terrain_du_Jeu;
        }

        /// <summary>
        /// Sélectionne le mode de jeu
        /// </summary>
        /// <param name="JeuDeLaVie"></param>
        public static void SelectionJeu(DeroulementJeu JeuDeLaVie)
        {
            //Mode normal
            if (ModeJeu == 0)
            {
                JeuDeLaVie.DeroulementJeuNormal();
            }

            //Mode day and night
            if (ModeJeu == 1)
            {
                JeuDeLaVie.DeroulementJeuDayAndNight();
            }

            //Signale au programme que le jeu est terminé
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
        public static float SetVitesseJeu
        {
            set => VitesseJeu = value;
        }
        public static float GetVitesseJeu
        {
            get { return VitesseJeu; }
        }
        #endregion
    }
}