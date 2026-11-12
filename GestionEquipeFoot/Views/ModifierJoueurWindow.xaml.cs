using System.Windows;
using GestionEquipeFoot.Models;
using GestionEquipeFoot.ViewModels;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace GestionEquipeFoot.Views
{
    public partial class ModifierJoueurWindow : Window
    {
        private JoueurViewModel viewModel;
        private Joueur joueurAModifier;
        private Dictionary<string, int> equipesDictionnaire;

        public ModifierJoueurWindow(JoueurViewModel joueurViewModel, Joueur joueur)
        {
            InitializeComponent();
            viewModel = joueurViewModel;
            joueurAModifier = joueur;
            ChargerEquipes();
            RemplirChamps();
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

        private void RemplirChamps()
        {
            NomJoueurTextBox.Text = joueurAModifier.NomJoueur;
            PrenomJoueurTextBox.Text = joueurAModifier.PrenomJoueur;
            NumJoueurTextBox.Text = joueurAModifier.NumJoueur.ToString();
            PosteJoueurTextBox.Text = joueurAModifier.PosteJoueur;
            EquipeComboBox.SelectedItem = joueurAModifier.NomEquipe;
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NomJoueurTextBox.Text) &&
                !string.IsNullOrWhiteSpace(PrenomJoueurTextBox.Text) &&
                !string.IsNullOrWhiteSpace(NumJoueurTextBox.Text) &&
                !string.IsNullOrWhiteSpace(PosteJoueurTextBox.Text) &&
                EquipeComboBox.SelectedItem != null)
            {
                int idEquipe = equipesDictionnaire[EquipeComboBox.SelectedItem.ToString()];

                joueurAModifier.NomJoueur = NomJoueurTextBox.Text;
                joueurAModifier.PrenomJoueur = PrenomJoueurTextBox.Text;
                joueurAModifier.NumJoueur = int.Parse(NumJoueurTextBox.Text);
                joueurAModifier.PosteJoueur = PosteJoueurTextBox.Text;
                joueurAModifier.IdEquipe = idEquipe;

                viewModel.ModifierJoueur(joueurAModifier);
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
