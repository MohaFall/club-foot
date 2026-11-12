using System.Windows;
using GestionEquipeFoot.ViewModels;
using GestionEquipeFoot.Models;

namespace GestionEquipeFoot.Views
{
    public partial class GestionRencontresWindow : Window
    {
        private RencontreViewModel viewModel;
        private Utilisateur utilisateurConnecte;

        public GestionRencontresWindow(Utilisateur utilisateur)
        {
            InitializeComponent();
            utilisateurConnecte = utilisateur;
            viewModel = new RencontreViewModel();
            RencontresDataGrid.ItemsSource = viewModel.Rencontres;

            InitialiserInterface();
        }

        private void InitialiserInterface()
        {
            if (utilisateurConnecte.Role != "admin")
            {
                BtnAjouter.Visibility = Visibility.Collapsed;
                BtnModifier.Visibility = Visibility.Collapsed;
                BtnSupprimer.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            GestionAdminWindow gestionAdminWindow = new GestionAdminWindow(utilisateurConnecte);
            gestionAdminWindow.Show();
            this.Close();
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterRencontreWindow ajouterWindow = new AjouterRencontreWindow(viewModel);
            ajouterWindow.ShowDialog();
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (RencontresDataGrid.SelectedItem is Rencontre rencontre)
            {
                ModifierRencontreWindow modifierWindow = new ModifierRencontreWindow(viewModel, rencontre);
                modifierWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une rencontre à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (RencontresDataGrid.SelectedItem is Rencontre rencontre)
            {
                var confirmation = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer ce match ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.Yes)
                {
                    viewModel.SupprimerRencontre(rencontre);
                    MessageBox.Show("Rencontre supprimée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une rencontre à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
