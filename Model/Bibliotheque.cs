using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Bibliotheque
    {
        public string DernierUtilisateur;
        public List<Livre> LivreList;
        public List<Membre> MembreList;

        public Bibliotheque(string pathFichier)
        {
            LivreList = new List<Livre>();
            MembreList = new List<Membre>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(pathFichier);
            XmlElement? root = xmlDocument.DocumentElement;
            DernierUtilisateur = root.GetAttribute("dernierUtilisateur");
            XmlNode membresRoot = root.SelectSingleNode("membres");
            XmlNode livresRoot = root.SelectSingleNode("livres");
            foreach(XmlNode membre in membresRoot.ChildNodes)
            {
                MembreList.Add(new Membre(membre));
            }
            foreach(XmlNode livre in livresRoot.ChildNodes)
            {
                LivreList.Add(new Livre(livre));
            }
        }
    }
}
