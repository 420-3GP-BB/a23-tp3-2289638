using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    internal class Livre
    {
        string Titre;
        string Auteur;
        string Editeur;
        int Annee;
        string ISBN;

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
    }
}
