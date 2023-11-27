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
    /// Interaction logic for UtilisateurChoix.xaml
    /// </summary>
    public partial class UtilisateurChoix : Window
    {
        public BibliothequeViewModel _viewModel;
        bool IsChange;
        public UtilisateurChoix(BibliothequeViewModel viewModel, bool isChangeUser)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            IsChange = isChangeUser;
        }
        public void confirmerClick(object sender, RoutedEventArgs e)
        {
            if (IsChange)
            {
                _viewModel.ChangerUtilisateur();
            } else
            {
                _viewModel.TransfererLivre();
            }

            this.Close();
        }
    }
}
