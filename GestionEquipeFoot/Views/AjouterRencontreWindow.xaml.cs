using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;
using System.Linq;

namespace GestionEquipeFoot.Views
{
    public partial class AjouterRencontreWindow : Window
    {
        private RencontreViewModel viewModel;

        public AjouterRencontreWindow(RencontreViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;

            // Charger les équipes dans les ComboBox
            var equipes = new EquipeViewModel().Equipes.ToList();
            Equipe1ComboBox.ItemsSource = equipes;
            Equipe2ComboBox.ItemsSource = equipes;
            Equipe1ComboBox.DisplayMemberPath = "NomEq";
            Equipe2ComboBox.DisplayMemberPath = "NomEq";
        }

        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (Equipe1ComboBox.SelectedItem is Equipe equipe1 && Equipe2ComboBox.SelectedItem is Equipe equipe2 && DateMatchPicker.SelectedDate.HasValue)
            {
                if (equipe1.IdEq == equipe2.IdEq)
                {
                    MessageBox.Show("Vous devez choisir deux équipes différentes.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                viewModel.AjouterRencontre(equipe1.IdEq, equipe2.IdEq, DateMatchPicker.SelectedDate.Value);

                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
