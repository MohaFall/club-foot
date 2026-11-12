using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;
using System;

namespace GestionEquipeFoot.Views
{
    public partial class ModifierRencontreWindow : Window
    {
        private RencontreViewModel viewModel;
        private Rencontre rencontre;

        public ModifierRencontreWindow(RencontreViewModel vm, Rencontre r)
        {
            InitializeComponent();
            viewModel = vm;
            rencontre = r;

            DateMatchPicker.SelectedDate = rencontre.DateRencontreRaw.Date;
            HeureMatchTextBox.Text = rencontre.DateRencontreRaw.ToString("HH:mm");


            Score1TextBox.Text = rencontre.ScoreEquipe1 == "À jouer" ? "" : rencontre.ScoreEquipe1;
            Score2TextBox.Text = rencontre.ScoreEquipe2 == "À jouer" ? "" : rencontre.ScoreEquipe2;
        }

        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (DateMatchPicker.SelectedDate.HasValue)
            {
                if (TimeSpan.TryParse(HeureMatchTextBox.Text, out TimeSpan heure))
                {
                    DateTime dateComplete = DateMatchPicker.SelectedDate.Value.Date + heure;

                    int.TryParse(Score1TextBox.Text, out int score1);
                    int.TryParse(Score2TextBox.Text, out int score2);

                    viewModel.ModifierRencontre(rencontre.IdRencontre, dateComplete, score1, score2);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Format de l'heure incorrect. Utilisez HH:mm.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir la date.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
