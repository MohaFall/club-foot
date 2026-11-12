using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;

namespace GestionEquipeFoot.Views
{
    public partial class ModifierEquipeWindow : Window
    {
        private EquipeViewModel viewModel;
        private Equipe equipeAModifier;

        public ModifierEquipeWindow(EquipeViewModel equipeViewModel, Equipe equipe)
        {
            InitializeComponent();
            viewModel = equipeViewModel;
            equipeAModifier = equipe;

            NomEquipeTextBox.Text = equipe.NomEq;
            ClassementTextBox.Text = equipe.PositionEq.ToString(); // ici conversion int -> string pour l'affichage
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NomEquipeTextBox.Text) && !string.IsNullOrWhiteSpace(ClassementTextBox.Text))
            {
                if (int.TryParse(ClassementTextBox.Text, out int position))
                {
                    equipeAModifier.NomEq = NomEquipeTextBox.Text;
                    equipeAModifier.PositionEq = position;

                    viewModel.ModifierEquipe(equipeAModifier);
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
