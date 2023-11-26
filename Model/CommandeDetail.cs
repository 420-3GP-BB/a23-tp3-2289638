using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CommandeDetail
    {
        public string Statut {  get; set; }
        public string ISBN {  get; set; }
        public string Auteur {  get; set; }
        public string Titre {  get; set; }
        public int Annee {  get; set; }
        public string NomMembre {  get; set; }

        public CommandeDetail(string statut, string isbn, string auteur, string titre, int annee, string nomMembre)
        {
            Statut = statut;
            ISBN = isbn;
            Auteur = auteur;
            Titre = titre;
            Annee = annee;
            NomMembre = nomMembre;
        }
        public override string ToString()
        {
            return $"{Titre}, par {Auteur} ({Annee})";
        }
    }
}
