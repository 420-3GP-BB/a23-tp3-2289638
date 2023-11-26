using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public BibliothequeViewModel viewModel;
        public AdminWindow(BibliothequeViewModel _viewModel)
        {
            InitializeComponent();
            viewModel=_viewModel;
            DataContext = viewModel;
        }
        private void revenirClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void commandeDouble_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                CommandeDetail selectedCommandeDetail = listBox.SelectedItem as CommandeDetail;
                if (selectedCommandeDetail != null)
                {
                    viewModel.TraiterCommande(selectedCommandeDetail);
                }
            }
        }
    }
}
