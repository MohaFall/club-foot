using System.Windows;
using GestionEquipeFoot.ViewModels;
using GestionEquipeFoot.Models;

namespace GestionEquipeFoot.Views
{
    public partial class EquipeWindow : Window
    {
        private EquipeViewModel viewModel;
        private Utilisateur utilisateurConnecte;

        public EquipeWindow(Utilisateur utilisateur)
        {
            InitializeComponent();
            utilisateurConnecte = utilisateur;
            viewModel = new EquipeViewModel();
            EquipesDataGrid.ItemsSource = viewModel.Equipes;
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(utilisateurConnecte);
            mainWindow.Show();
            this.Close();
        }
    }
}
