using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        public BibliothequeViewModel _viewModel;
        
        public MainWindow()
        {
            string fichierBibliotheque = "bibliotheque.xml";
            string pathMesDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pathDossier = $"{pathMesDocuments}{DIR_SEPARATOR}Fichiers-3GP";
            string pathFichier = $"{pathDossier}{DIR_SEPARATOR}{fichierBibliotheque}";
            _viewModel = new BibliothequeViewModel(pathFichier);
            InitializeComponent();
            DataContext = _viewModel;
        }
        private void changerUtilClick(object sender, RoutedEventArgs e)
        {
            UtilisateurChoix window = new UtilisateurChoix(_viewModel, true);
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        private void transfererLivreClick(object sender, RoutedEventArgs e)
        {
            UtilisateurChoix window = new UtilisateurChoix(_viewModel, false);
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        private void quitterClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void adminClick(object sender, RoutedEventArgs e)
        {
            AdminWindow window = new AdminWindow(_viewModel);
            window.Owner = this;
            window.WindowStartupLocation= WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        private void annulerCommandeClick(object sender, RoutedEventArgs e)
        {
            _viewModel.retirerCommande(attenteListBox.SelectedItem);
        }
        private void nvCommandeClick(object sender, RoutedEventArgs e)
        {
            CommanderLivreWindow window = new CommanderLivreWindow(_viewModel);
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
    }
}