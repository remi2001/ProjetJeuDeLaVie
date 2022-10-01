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
