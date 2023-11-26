using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Membre
    {
        public string Nom;
        public bool Admin;
        public List<string> LivresISBN;
        public ObservableCollection<Commande> Commandes;
        public Membre(XmlNode node)
        {
            LivresISBN = new List<string>();
            Commandes = new ObservableCollection<Commande>();
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
        public Membre()
        {
            Nom = string.Empty;
            Admin = false;
        }
        public override string ToString()
        {
            return Nom;
        }
    }
}
