using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;
using System;

namespace GestionEquipeFoot.Views
{
    public partial class GestionEquipesWindow : Window
    {
        private EquipeViewModel viewModel;
        private Utilisateur utilisateurConnecte;

        public GestionEquipesWindow(Utilisateur utilisateur)
        {
            InitializeComponent();
            utilisateurConnecte = utilisateur;
            viewModel = new EquipeViewModel();
            EquipesDataGrid.ItemsSource = viewModel.Equipes;

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
            AjouterEquipeWindow ajouterWindow = new AjouterEquipeWindow(viewModel);
            ajouterWindow.ShowDialog();
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (EquipesDataGrid.SelectedItem is Equipe equipe)
            {
                ModifierEquipeWindow modifierWindow = new ModifierEquipeWindow(viewModel, equipe);
                modifierWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une équipe à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (EquipesDataGrid.SelectedItem is Equipe equipe)
            {
                var confirmation = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer {equipe.NomEq} ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.Yes)
                {
                    viewModel.SupprimerEquipe(equipe);
                    MessageBox.Show("Équipe supprimée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une équipe à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
