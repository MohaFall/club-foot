using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;

namespace GestionEquipeFoot.Views
{
    public partial class JoueurWindow : Window
    {
        private JoueurViewModel viewModel;
        private Utilisateur utilisateurConnecte;

        public JoueurWindow(Utilisateur utilisateur)
        {
            InitializeComponent();
            utilisateurConnecte = utilisateur;
            viewModel = new JoueurViewModel();
            JoueursDataGrid.ItemsSource = viewModel.Joueurs;
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(utilisateurConnecte);
            mainWindow.Show();
            this.Close();
        }
    }
}
