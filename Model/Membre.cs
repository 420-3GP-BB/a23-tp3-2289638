using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    internal class Membre
    {
        string Nom;
        bool Admin;
        List<string> LivresISBN;
        List<Commande> Commandes;


        public Membre(XmlNode node)
        {
            LivresISBN = new List<string>();
            Commandes = new List<Commande>();
            XmlElement? element = node as XmlElement;
            Nom = element.GetAttribute("nom");
            Admin = Convert.ToBoolean(element.GetAttribute("administrateur"));
            XmlNodeList? livresList = element.GetElementsByTagName("livre");
            XmlNodeList? commandesList = element.GetElementsByTagName("commande");
            if (livresList != null)
            {
                foreach (XmlNode livre in livresList)
                {
                    XmlElement? livreElement = livre as XmlElement;
                    LivresISBN.Add(livreElement.GetAttribute("ISBN-13"));
                }
            }
            if (commandesList != null)
            {
                foreach(XmlNode commande in commandesList)
                {
                    Commandes.Add(new Commande(commande));
                }
            }
        }
    }
}
