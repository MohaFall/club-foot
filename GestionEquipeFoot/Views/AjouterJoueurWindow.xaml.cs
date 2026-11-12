using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace GestionEquipeFoot.Views
{
    public partial class AjouterJoueurWindow : Window
    {
        private JoueurViewModel viewModel;
        private Dictionary<string, int> equipesDictionnaire;

        public AjouterJoueurWindow(JoueurViewModel joueurViewModel)
        {
            InitializeComponent();
            viewModel = joueurViewModel;
            ChargerEquipes();
        }

        private void ChargerEquipes()
        {
            equipesDictionnaire = new Dictionary<string, int>();
            string connectionString = Database.ConnectionString;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT idEq, NomEq FROM equipe", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("idEq");
                        string nom = reader.GetString("NomEq");
                        equipesDictionnaire.Add(nom, id);
                        EquipeComboBox.Items.Add(nom);
                    }
                }
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NomJoueurTextBox.Text) &&
                !string.IsNullOrWhiteSpace(PrenomJoueurTextBox.Text) &&
                !string.IsNullOrWhiteSpace(NumJoueurTextBox.Text) &&
                !string.IsNullOrWhiteSpace(PosteJoueurTextBox.Text) &&
                EquipeComboBox.SelectedItem != null)
            {
                int idEquipe = equipesDictionnaire[EquipeComboBox.SelectedItem.ToString()];

                Joueur nouveauJoueur = new Joueur
                {
                    NomJoueur = NomJoueurTextBox.Text,
                    PrenomJoueur = PrenomJoueurTextBox.Text,
                    NumJoueur = int.Parse(NumJoueurTextBox.Text),
                    PosteJoueur = PosteJoueurTextBox.Text,
                    IdEquipe = idEquipe
                };

                viewModel.AjouterJoueur(nouveauJoueur);
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
