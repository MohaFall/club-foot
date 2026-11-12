using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;

namespace GestionEquipeFoot.Views
{
    public partial class AjouterEquipeWindow : Window
    {
        private EquipeViewModel viewModel;

        public AjouterEquipeWindow(EquipeViewModel equipeViewModel)
        {
            InitializeComponent();
            viewModel = equipeViewModel;
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NomEquipeTextBox.Text) && !string.IsNullOrWhiteSpace(ClassementTextBox.Text))
            {
                if (int.TryParse(ClassementTextBox.Text, out int position))
                {
                    Equipe nouvelleEquipe = new Equipe
                    {
                        NomEq = NomEquipeTextBox.Text,
                        PositionEq = position,
                        Photo = "/image/default.png" // Valeur par défaut
                    };

                    viewModel.AjouterEquipe(nouvelleEquipe);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Le classement doit être un nombre entier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
