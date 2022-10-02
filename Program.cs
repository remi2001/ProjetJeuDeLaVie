using System;

namespace ProjetJeuDeLaVie
{
    class Program
    {
        static void Main(String[] args)
        {
            Jeu JeuDeLaVie = new Jeu();
            JeuDeLaVie.LancementDuJeu();

            //Test de la classe Terrain
            //Le pourcentage saisie doit etre entre 0 et 100
            /*
            Terrain Terrain_du_Jeu = new Terrain(0);
            Terrain_du_Jeu.Affichage_du_terrain();
            */
        }
    }
}