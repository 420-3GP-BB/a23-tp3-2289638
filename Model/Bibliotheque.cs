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
        public void saveBibliotheque(string pathFichier)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement root = xmlDocument.CreateElement("bibliotheque");
            root.SetAttribute("dernierUtilisateur",DernierUtilisateur);
            XmlElement membreRoot = xmlDocument.CreateElement("membres");
            XmlElement livreRoot = xmlDocument.CreateElement("livres");
            foreach(Membre membre in MembreList)
            {
                XmlElement membreElement = xmlDocument.CreateElement("membre");
                membreElement.SetAttribute("nom", membre.Nom);
                string admin = membre.Admin.ToString();
                membreElement.SetAttribute("administrateur", admin);
                foreach(string id in membre.LivresISBN)
                {
                    XmlElement livre = xmlDocument.CreateElement("livre");
                    livre.SetAttribute("ISBN-13", id);
                    membreElement.AppendChild(livre);
                }
                foreach(Commande commande in membre.Commandes)
                {
                    XmlElement commandeElement = xmlDocument.CreateElement("commande");
                    commandeElement.SetAttribute("statut", commande.Statut);
                    commandeElement.SetAttribute("ISBN-13", commande.ISBN);
                    membreElement.AppendChild (commandeElement);
                }
                membreRoot.AppendChild(membreElement);
            }
            root.AppendChild(membreRoot);
            foreach(Livre livre in LivreList)
            {
                XmlElement livreElement = xmlDocument.CreateElement("livre");
                XmlElement titre = xmlDocument.CreateElement("titre");
                XmlElement auteur = xmlDocument.CreateElement("auteur");
                XmlElement editeur = xmlDocument.CreateElement("editeur");
                XmlElement annee = xmlDocument.CreateElement("annee");
                livreElement.SetAttribute("ISBN-13", livre.ISBN);
                titre.InnerText = livre.Titre;
                auteur.InnerText = livre.Auteur;
                editeur.InnerText = livre.Editeur;
                annee.InnerText = livre.Annee.ToString();
                livreElement.AppendChild(titre);
                livreElement.AppendChild(auteur);
                livreElement.AppendChild(editeur);
                livreElement.AppendChild(annee);
                livreRoot.AppendChild(livreElement);
            }
            root.AppendChild(livreRoot);
            xmlDocument.AppendChild(root);
            xmlDocument.Save(pathFichier);
        }

    }
}
