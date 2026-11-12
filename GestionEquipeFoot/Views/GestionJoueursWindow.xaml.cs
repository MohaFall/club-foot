using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;

namespace GestionEquipeFoot.Views
{
    public partial class GestionJoueursWindow : Window
    {
        private JoueurViewModel viewModel;
        private Utilisateur utilisateurConnecte;

        public GestionJoueursWindow(Utilisateur utilisateur)
        {
            InitializeComponent();
            utilisateurConnecte = utilisateur;
            viewModel = new JoueurViewModel();
            JoueursDataGrid.ItemsSource = viewModel.Joueurs;

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
            AjouterJoueurWindow ajouterWindow = new AjouterJoueurWindow(viewModel);
            ajouterWindow.ShowDialog();
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (JoueursDataGrid.SelectedItem is Joueur joueur)
            {
                ModifierJoueurWindow modifierWindow = new ModifierJoueurWindow(viewModel, joueur);
                modifierWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un joueur à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (JoueursDataGrid.SelectedItem is Joueur joueur)
            {
                var confirmation = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer {joueur.NomJoueur} ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.Yes)
                {
                    viewModel.SupprimerJoueur(joueur);
                    MessageBox.Show("Joueur supprimé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un joueur à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
