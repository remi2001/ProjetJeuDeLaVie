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
        private string SiSelectionner;

        public SousMenu(byte iD, string phrase)
        {
            //Faire en sorte que si un Id est pris, la fen ne peut pas se créé
            this.IdMenu = iD;
            this.Phrase = phrase;
            if(iD == 0)
            {
                SiSelectionner = ">>";
            }
            else
            {
                SiSelectionner = " ";
            }
        }
        
        public override string? ToString()
        {

            if(SiSelectionner == ">>")
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            return SiSelectionner.ToString() + " " + this.IdMenu + "-" + this.Phrase;
        }

        public string SetSiSelectionner
        {
            set => SiSelectionner = value;
        }

        public string SetPhrase
        {
            set => this.Phrase = value;
        }
    }
}
