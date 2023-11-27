using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class BibliothequeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private Bibliotheque _bibliotheque;
        public ObservableCollection<Livre> Livres { get; private set; }
        public ObservableCollection<Membre> Membres { get; private set; }
        public ObservableCollection<CommandeDetail> allCommandes { get; private set; }
        public Membre choixUtil { get; set; }
        private Membre _utilisateurActuel;

        public Membre UtilisateurActuel
        {
            set
            {
                    _utilisateurActuel = value;
                    LivresUtil = new ObservableCollection<Livre>();
                    CommandesAttenteUtil = new ObservableCollection<CommandeDetail>();
                    CommandesTraiteesUtil = new ObservableCollection<CommandeDetail>();
                    isAdmin = _utilisateurActuel.Admin;
                    foreach(Livre livre in Livres)
                    {
                        foreach(string livreID in UtilisateurActuel.LivresISBN)
                        {
                            if (livreID.Equals(livre.ISBN)) 
                            {
                                LivresUtil.Add(livre);
                            }
                        }
                    }
                    foreach(Commande commande in UtilisateurActuel.Commandes)
                    {
                        CommandeDetail commandeDetail = ConvertToCommandeDetail(commande, UtilisateurActuel.Nom);
                        if (commandeDetail.Statut.Equals("Attente"))
                        {
                            CommandesAttenteUtil.Add(commandeDetail);
                        } else
                        {
                            CommandesTraiteesUtil.Add(commandeDetail);
                        }
                    }
                    OnPropertyChanged(nameof(UtilisateurActuel));

                
            }
            get { return _utilisateurActuel; }
        }
        public string NomUtil
        {
            get
            {
                return UtilisateurActuel.Nom;
            }
        }
        public ObservableCollection<Livre> _livresUtil;
        public ObservableCollection<Livre> LivresUtil
        {
            get
            {
                return _livresUtil;
            }
            set
            {
                _livresUtil = value;
                OnPropertyChanged(nameof(LivresUtil));
            }
        }
        private ObservableCollection<CommandeDetail> _commandesAttenteUtil;

        public ObservableCollection<CommandeDetail> CommandesAttenteUtil
        {
            get
            {
                return _commandesAttenteUtil;
            }
            set
            {
                _commandesAttenteUtil = value;
                OnPropertyChanged(nameof(CommandesAttenteUtil));
            }
        }
        private ObservableCollection<CommandeDetail> _commandesTraiteesUtil;
        public ObservableCollection<CommandeDetail> CommandesTraiteesUtil
        {
            get
            {
                return _commandesTraiteesUtil;
            }
            set
            {
                _commandesTraiteesUtil = value;
                OnPropertyChanged(nameof(CommandesTraiteesUtil));
            }
        }
        private bool _isAdmin;
        public bool isAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(isAdmin));
            }
        }
        private bool _isSelectedBook;
        public bool isSelectedBook 
        {
            get
            {
                return _isSelectedBook;
            }
            set
            {
                _isSelectedBook = value;
                OnPropertyChanged(nameof(isSelectedBook));
            }
        }
        private Livre _selectedLivre;
        public Livre selectedLivre
        {
            get
            {
                return _selectedLivre;
            }
            set
            {
                _selectedLivre = value;
                OnPropertyChanged(nameof(selectedLivre));
                if(value is Livre)
                {
                    isSelectedBook = true;
                }
            }
        }
        private ObservableCollection<CommandeDetail> _allCommandesAttente;
        private ObservableCollection<CommandeDetail> _allCommandesTraitee;
        public ObservableCollection<CommandeDetail> allCommandesAttente
        {
            get { return _allCommandesAttente;}
            set { _allCommandesAttente = value; OnPropertyChanged(nameof(allCommandesAttente));}
        }
        public ObservableCollection<CommandeDetail> allCommandesTraitee
        {
            get { return _allCommandesTraitee; }
            set { _allCommandesTraitee = value; OnPropertyChanged(nameof(allCommandesTraitee)); }
        }
        public BibliothequeViewModel(string pathFichier)
        {
            _bibliotheque = new Bibliotheque(pathFichier);
            Livres = new ObservableCollection<Livre>();
            Membres = new ObservableCollection<Membre>();
            allCommandes = new ObservableCollection<CommandeDetail>();
            allCommandesAttente = new ObservableCollection<CommandeDetail>();
            allCommandesTraitee = new ObservableCollection<CommandeDetail>();
            LoadDataFromModel();
            UtilisateurActuel = Membres[0];
        }
        private void LoadDataFromModel()
        {
            foreach (Livre livre in _bibliotheque.LivreList)
            {
                Livres.Add(livre);
            }

            foreach (Membre membre in _bibliotheque.MembreList)
            {
                Membres.Add(membre);
            }
            updateAllCommandes();
        }
        private void updateAllCommandes()
        {
            allCommandesAttente.Clear();
            allCommandesTraitee.Clear();
            foreach (Membre membre in Membres)
            {
                foreach (Commande commande in membre.Commandes)
                {
                    CommandeDetail commandeDetail = ConvertToCommandeDetail(commande, membre.Nom);
                    allCommandes.Add(commandeDetail);
                    if (commandeDetail.Statut.Equals("Attente"))
                    {
                        allCommandesAttente.Add(commandeDetail);
                    }
                    else
                    {
                        allCommandesTraitee.Add(commandeDetail);
                    }
                }

            }
        }
        private CommandeDetail ConvertToCommandeDetail(Commande commande, string nom)
        {
            Livre livre = Livres.FirstOrDefault(l => l.ISBN == commande.ISBN);
            if (livre != null)
            {
                return new CommandeDetail(commande.Statut,commande.ISBN,livre.Auteur,livre.Titre,livre.Annee, nom);
            }
            return null;
        }

        public void retirerCommande(Object obj)
        {
            Commande foundCommand = new Commande();
            CommandeDetail commande = obj as CommandeDetail;
            foreach(Membre membre in Membres)
            {
                if (membre.Nom.Equals(commande.NomMembre))
                {
                    foreach(Commande commandeMembre in membre.Commandes)
                    {
                        if (commandeMembre.ISBN == commande.ISBN)
                        {
                            foundCommand=commandeMembre;
                        }
                    }
                    membre.Commandes.Remove(foundCommand);
                    UtilisateurActuel = membre;
                }
            }
            updateAllCommandes();
        }
        public void ajouterCommande(string titre, string isbn, string auteur, string editeur, string anneeString)
        {
            int.TryParse(anneeString, out int annee);
            Livres.Add(new Livre(titre,auteur,editeur,annee,isbn));
            foreach(Membre membre in Membres)
            {
                if (membre == UtilisateurActuel)
                {
                    membre.Commandes.Add(new Commande("Attente", isbn));
                    UtilisateurActuel = membre;
                }
            }
            updateAllCommandes();
        }
        public void TraiterCommande(CommandeDetail commande)
        {

            allCommandesAttente.Remove(commande);
            updateCommandeUser(commande);
            commande.Statut = "Traitee";
            allCommandesTraitee.Add(commande);

        }
        private void updateCommandeUser(CommandeDetail commandeDetail)
        {
            Commande foundCommand = new Commande();
            Membre foundMember = new Membre();
            foreach(Membre membre in Membres)
            {
                if (membre.Nom.Equals(commandeDetail.NomMembre))
                {
                    foundMember = membre;
                    foreach (Commande commande in foundMember.Commandes)
                    {
                        if (commande.ISBN == commandeDetail.ISBN)
                        {
                            foundCommand = commande;
                        }
                    }
                }
                
            }
            foundMember.Commandes.Remove(foundCommand);
            foundCommand.Statut = "Traitee";
            foundMember.Commandes.Add(foundCommand);
            UtilisateurActuel = UtilisateurActuel;
        }
        public void ChangerUtilisateur()
        {
            UtilisateurActuel = choixUtil;
            isSelectedBook = false;
        }
        public void TransfererLivre()
        {
            UtilisateurActuel.LivresISBN.Remove(selectedLivre.ISBN);
            choixUtil.LivresISBN.Add(selectedLivre.ISBN);
            UtilisateurActuel=UtilisateurActuel;
            isSelectedBook = false;
        }
        private void OnPropertyChanged(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
