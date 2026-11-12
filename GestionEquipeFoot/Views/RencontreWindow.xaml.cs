using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;
using System.ComponentModel; // pour utiliser SortDescription

namespace GestionEquipeFoot.Views
{
    public partial class RencontreWindow : Window
    {
        private RencontreViewModel viewModel;
        private Utilisateur utilisateurConnecte;

        public RencontreWindow(Utilisateur utilisateur)
        {
            InitializeComponent();
            utilisateurConnecte = utilisateur;
            viewModel = new RencontreViewModel();

            // Lier la liste des rencontres au DataGrid
            RencontresDataGrid.ItemsSource = viewModel.Rencontres;

            // Appliquer un tri automatique sur la DateRencontre (croissant)
            RencontresDataGrid.Items.SortDescriptions.Clear();
            RencontresDataGrid.Items.SortDescriptions.Add(
                new SortDescription("DateRencontreRaw", ListSortDirection.Ascending)
            );
            RencontresDataGrid.Items.Refresh();

        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(utilisateurConnecte);
            mainWindow.Show();
            this.Close();
        }
    }
}
