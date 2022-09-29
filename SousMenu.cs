using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetJeuDeLaVie
{
    internal class SousMenu
    {
        private byte IdMenu;
        private string Phrase;
        private char SiSelectionner = ' ';

        public SousMenu(byte iD, string phrase)
        {
            //Faire en sorte que si un Id est pris, la fen ne peut pas se créé
            this.IdMenu = iD;
            this.Phrase = phrase;
        }

        public void Lancer()
        {
            Console.Clear();
            Console.WriteLine("LANCEMENT");
            //JeuDeLaVie.Deroulement();
        }

        public void Option()
        {
            Console.Clear();
            List<SousMenu> ListeSousMenuOption = new List<SousMenu>();

            SousMenu? FEN_Option1 = new SousMenu(1, "Option 1");
            SousMenu? FEN_Option2 = new SousMenu(2, "Option 2");
            SousMenu? FEN_Option3 = new SousMenu(3, "Option 3");

            ListeSousMenuOption.Add(FEN_Option1);
            ListeSousMenuOption.Add(FEN_Option2);
            ListeSousMenuOption.Add(FEN_Option3);

            Menu MenuOption = new Menu(ListeSousMenuOption, 2, "Voici les options Modifiables");

            MenuOption.ToString();
            MenuOption.Naviguer(MenuOption);

            FEN_Option1 = null;
            FEN_Option2 = null;
            FEN_Option3 = null;
            ListeSousMenuOption.Clear();
        }

        public void Quitter()
        {
            Environment.Exit(1);
        }

        public override string? ToString()
        {
            return SiSelectionner + this.IdMenu + "-" + this.Phrase;
        }

        public char SetSiSelectionner
        {
            set { SiSelectionner = value; }
        }

    }
}
