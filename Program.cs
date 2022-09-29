using System;

namespace ProjetJeuDeLaVie
{
    class Program
    {
        static void Main(String[] args)
        {

            List<SousMenu> ListeSousMenuPrincipale = new List<SousMenu>();

            SousMenu FEN_Lancer = new SousMenu(1, "Lancer");
            SousMenu FEN_Option = new SousMenu(2, "Option");
            SousMenu FEN_Quitter = new SousMenu(3, "Quitter");

            ListeSousMenuPrincipale.Add(FEN_Lancer);
            ListeSousMenuPrincipale.Add(FEN_Option);
            ListeSousMenuPrincipale.Add(FEN_Quitter);

            Menu MenuPrincipale = new Menu(ListeSousMenuPrincipale, 1, "Voici le menu. Pour naviguer, appuyez sur les flèches haut et bas. Pour ____ appuyez sur ENTER");

            Jeu JeuDeLaVie = new Jeu();
            JeuDeLaVie.LancementDuJeu(MenuPrincipale);
            
        }
    }
}