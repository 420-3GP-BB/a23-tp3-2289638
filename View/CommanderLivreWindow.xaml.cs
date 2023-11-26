using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CommanderLivreWindow.xaml
    /// </summary>
    public partial class CommanderLivreWindow : Window
    {
        public BibliothequeViewModel viewModel;
        public CommanderLivreWindow(BibliothequeViewModel _viewModel)
        {
            InitializeComponent();
            viewModel = _viewModel;
            DataContext = viewModel;
        }

        private void confirmIsEnabled(object sender, TextChangedEventArgs e)
        {
            confirmerButton.IsEnabled = canConfirm();
        }
        private bool canConfirm()
        {
            if (!(String.IsNullOrWhiteSpace(TitreBox.Text)||
                String.IsNullOrWhiteSpace(AuteurBox.Text)||
                String.IsNullOrWhiteSpace(EditeurBox.Text)||
                String.IsNullOrWhiteSpace(AnneeBox.Text)))
            {
                if (ISBNBox.Text.Length == 13 && Regex.IsMatch(ISBNBox.Text, @"^\d+$"))
                {
                    return true;
                } else { return false; }
            } else { return false; }
        }
    }
}
