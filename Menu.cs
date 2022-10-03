using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProjetJeuDeLaVie
{
    internal class Menu
    {
        private List<SousMenu> ListeSousMenu;
        private byte GroupeMenu;
        private string PhraseExplicatifMenu;
        private byte Curseur;

        public Menu(List<SousMenu> listeSousMenu, byte groupeMenu, string phraseExplicatifMenu)
        {
            ListeSousMenu = listeSousMenu;
            GroupeMenu = groupeMenu;
            PhraseExplicatifMenu = phraseExplicatifMenu;
            this.Curseur = 0;
        }

        public void Naviguer(Jeu JeuDeLaVie)
        {
            bool Valide = false;
            do
            {
                ConsoleKeyInfo ToucheAppuye = Console.ReadKey();

                if(ToucheAppuye.Key != ConsoleKey.Enter)
                {
                    switch (ToucheAppuye.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if(this.Curseur != this.ListeSousMenu.Count - 1)
                            {
                                this.Curseur++;
                                this.ListeSousMenu[Curseur - 1].SetSiSelectionner = " ";
                                this.ListeSousMenu[Curseur].SetSiSelectionner = ">>";
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (this.Curseur != 0)
                            {
                                this.Curseur--;
                                this.ListeSousMenu[Curseur].SetSiSelectionner = ">>";
                                this.ListeSousMenu[Curseur + 1].SetSiSelectionner = " ";
                            }
                            break;
                    }

                    Console.Clear();
                    this.ToString();
                }
                else
                {
                    Valide = true;
                }
                
            } while (Valide == false);


            this.SelectionSousMenu(this.Curseur, this.GroupeMenu,JeuDeLaVie);
        }

        private void SelectionSousMenu(byte IdSousMenu, byte GroupeMenu, Jeu JeuDeLaVie)
        {
            string test = GroupeMenu + "." + IdSousMenu;

            switch (test)
            {
                case "1.0":
                    JeuDeLaVie.LancerJeu();
                    break;
                case "1.1":
                    JeuDeLaVie.OptionJeu();
                    break;
                case "1.2":
                    JeuDeLaVie.QuitterJeu();
                    break;
                case "2.0":
                    break;
                case "2.1":
                    break;
                case "2.2":
                    break;
            }
        }

        public override string? ToString()
        {

            Console.WriteLine(this.PhraseExplicatifMenu);
            foreach(SousMenu sousmenu in ListeSousMenu)
            {
                Console.WriteLine(sousmenu.ToString());
            }

            //Problème affichage couleur console blanche COM A REVOIRE
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            return null;
        }
    }
}
