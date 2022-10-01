using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            this.Curseur = 1;
        }

        public void Naviguer(Jeu JeuDeLaVie)
        {
            

            ConsoleKeyInfo ToucheAppuye = Console.ReadKey();

            if (ToucheAppuye.Key != ConsoleKey.Enter)
            {
                do
                {
                    if (this.Curseur >= 1 && this.Curseur <= 3)
                    {
                        Console.Clear();
                        switch (ToucheAppuye.Key)
                        {
                            case ConsoleKey.DownArrow:
                                
                                this.ListeSousMenu[this.Curseur - 1].SetSiSelectionner = '>';
                                this.Curseur++;
                                break;
                            case ConsoleKey.UpArrow:
                                this.ListeSousMenu[this.Curseur - 1].SetSiSelectionner = ' ';
                                this.Curseur--;
                                break;
                        }
                    }

                    this.ToString();
                    ToucheAppuye = Console.ReadKey();
                } while (ToucheAppuye.Key != ConsoleKey.Enter);
            }

            this.SelectionSousMenu(this.Curseur, this.GroupeMenu,JeuDeLaVie);
        }

        private void SelectionSousMenu(byte IdSousMenu, byte GroupeMenu, Jeu JeuDeLaVie)
        {
            string test = GroupeMenu + "." + IdSousMenu;

            switch (test)
            {
                case "1.1":
                    JeuDeLaVie.LancerJeu();
                    break;
                case "1.2":
                    JeuDeLaVie.OptionJeu();
                    break;
                case "1.3":
                    JeuDeLaVie.QuitterJeu();
                    break;
                case "2.1":
                    break;
                case "2.2":
                    break;
                case "2.3":
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

            return "";
        }
    }
}
