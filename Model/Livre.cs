using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Livre
    {
        public string Titre;
        public string Auteur;
        public string Editeur;
        public int Annee;
        public string ISBN;

        public Livre(XmlNode livreNode)
        {
            XmlElement livreElement = livreNode as XmlElement;
            ISBN = livreElement.GetAttribute("ISBN-13");
            XmlNode titre = livreElement.SelectSingleNode("titre");
            XmlNode auteur = livreElement.SelectSingleNode("auteur");
            XmlNode editeur = livreElement.SelectSingleNode("editeur");
            XmlNode annee = livreElement.SelectSingleNode("annee");
            Titre = titre.InnerText;
            Auteur = auteur.InnerText;
            Editeur = editeur.InnerText;
            int.TryParse(annee.InnerText, out int parsedAnnee);
            Annee = parsedAnnee;
        }
        public override string ToString()
        {
            return$"{Titre}, par {Auteur} ({Annee}); {Editeur}";
        }
    }
}
