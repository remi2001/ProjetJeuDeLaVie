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

        public Menu(List<SousMenu> listeSousMenu, byte groupeMenu, string phraseExplicatifMenu)
        {
            ListeSousMenu = listeSousMenu;
            GroupeMenu = groupeMenu;
            PhraseExplicatifMenu = phraseExplicatifMenu;
        }

        public void Naviguer(Menu MenuActuel)
        {
            int Curseur = 1;

            ConsoleKeyInfo ToucheAppuye = Console.ReadKey();

            if(ToucheAppuye.Key != ConsoleKey.Enter)
            {
                do
                {
                    
                    if (Curseur == 0)
                    {
                        Curseur = 1;
                    }
                    else
                    {
                        if (Curseur == 4)
                        {
                            Curseur = 3;
                        }
                    }

                    if (Curseur > 0 && Curseur < 4)
                    {
                        Console.Clear();
                        switch (ToucheAppuye.Key)
                        {
                            case ConsoleKey.DownArrow:
                                ListeSousMenu[Curseur-1].SetSiSelectionner = '>';
                                Curseur++;
                                
                                break;
                            case ConsoleKey.UpArrow:
                                
                                Curseur--;
                                ListeSousMenu[Curseur].SetSiSelectionner = '>';
                                break;
                        }
                    }

                    MenuActuel.ToString();
                    ToucheAppuye = Console.ReadKey();
                } while (ToucheAppuye.Key != ConsoleKey.Enter);
            }
            
            if(this.GroupeMenu == 1)
            {
                switch (Curseur)
                {
                    case 1:
                        ListeSousMenu[0].Lancer();
                        break;
                    case 2:
                        ListeSousMenu[1].Option();
                        break;
                    case 3:
                        ListeSousMenu[2].Quitter();
                        break;
                }
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
