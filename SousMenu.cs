using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetJeuDeLaVie
{
    internal class SousMenu
    {
        #region Attribut
        /// <summary>
        /// L'identifiant du sous-menu
        /// </summary>
        private byte IdMenu;
        /// <summary>
        /// La phrase qu'il faut afficher à l'utilisateur dans le menu
        /// </summary>
        private string Phrase;
        /// <summary>
        /// Enregistre le caratère simulant la présence ou non du curseur
        /// </summary>
        private string SiSelectionner;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de sous-menu
        /// </summary>
        /// <param name="iD">L'identifiant du sous-menu</param>
        /// <param name="phrase">La phrase qu'il faut afficher à l'utilisateur dans le menu</param>
        public SousMenu(byte iD, string phrase)
        {
            //Faire en sorte que si un Id est pris, la fen ne peut pas se créé
            this.IdMenu = iD;
            this.Phrase = phrase;
            //Si c'est le premier sous-menu, le curseur est présent par défaut
            if(iD == 0)
            {
                SiSelectionner = ">>";
            }
            else
            {
                SiSelectionner = " ";
            }
        }
        #endregion

        #region Méthode
        public override string? ToString()
        {
            //Si le curseur est présent
            if(SiSelectionner == ">>")
            {
                //Modifie la couleur de l'arrière plan et de police de la console
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                //Modifie la couleur de l'arrière plan et de police de la console
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            return SiSelectionner.ToString() + " " + this.IdMenu + "-" + this.Phrase;
        }
        #endregion

        #region Accesseur
        public string SetSiSelectionner
        {
            set => SiSelectionner = value;
        }

        public string SetPhrase
        {
            set => this.Phrase = value;
        }
        #endregion
    }
}
