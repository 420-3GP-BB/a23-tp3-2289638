using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    internal class Commande
    {
        string Statut;
        string ISBN;

        public Commande(XmlNode commandeNode)
        {
            XmlElement? commandeElement = commandeNode as XmlElement;
            Statut = commandeElement.GetAttribute("statut");
            ISBN = commandeElement.GetAttribute("ISBN-13");
        }
    }
}
