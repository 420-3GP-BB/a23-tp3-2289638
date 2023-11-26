using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Commande
    {
        public string Statut;
        public string ISBN;

        public Commande(XmlNode commandeNode)
        {
            XmlElement? commandeElement = commandeNode as XmlElement;
            Statut = commandeElement.GetAttribute("statut");
            ISBN = commandeElement.GetAttribute("ISBN-13");
        }
        public Commande()
        {
            Statut = string.Empty;
            ISBN = string.Empty;
        }
        public override string ToString()
        {
            return $"";
        }
    }
}
