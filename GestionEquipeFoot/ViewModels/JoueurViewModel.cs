using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GestionEquipeFoot.Models;

namespace GestionEquipeFoot.ViewModels
{
    public class JoueurViewModel : INotifyPropertyChanged
    {
        private string connectionString = Database.ConnectionString;

        public ObservableCollection<Joueur> Joueurs { get; set; }

        public JoueurViewModel()
        {
            Joueurs = new ObservableCollection<Joueur>();
            ChargerJoueurs();
        }

        public void ChargerJoueurs()
        {
            Joueurs.Clear();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    @"SELECT j.idJoueur, j.NomJoueur, j.PrenomJoueur, j.NumJoueur, j.PosteJoueur, e.NomEq
              FROM joueur j
              JOIN appartient a ON j.idJoueur = a.idJoueur
              JOIN equipe e ON a.idEq = e.idEq", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Joueurs.Add(new Joueur
                        {
                            IdJoueur = reader.GetInt32("idJoueur"),
                            NomJoueur = reader.GetString("NomJoueur"),
                            PrenomJoueur = reader.GetString("PrenomJoueur"),
                            NumJoueur = reader.GetInt32("NumJoueur"),
                            PosteJoueur = reader.GetString("PosteJoueur"),
                            NomEquipe = reader.GetString("NomEq")
                        });
                    }
                }
            }
        }


        public void AjouterJoueur(Joueur joueur)
        {
            int nouvelIdJoueur = 0;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var insertJoueur = new MySqlCommand(
                    "INSERT INTO joueur (NomJoueur, PrenomJoueur, NumJoueur, PosteJoueur) VALUES (@nom, @prenom, @num, @poste)",
                    connection);
                insertJoueur.Parameters.AddWithValue("@nom", joueur.NomJoueur);
                insertJoueur.Parameters.AddWithValue("@prenom", joueur.PrenomJoueur);
                insertJoueur.Parameters.AddWithValue("@num", joueur.NumJoueur);
                insertJoueur.Parameters.AddWithValue("@poste", joueur.PosteJoueur);
                insertJoueur.ExecuteNonQuery();

                // Récupérer l'id du nouveau joueur
                nouvelIdJoueur = (int)insertJoueur.LastInsertedId;

                // Insérer dans appartient
                var insertAppartient = new MySqlCommand(
                    "INSERT INTO appartient (idJoueur, idEq) VALUES (@idJoueur, @idEq)",
                    connection);
                insertAppartient.Parameters.AddWithValue("@idJoueur", nouvelIdJoueur);
                insertAppartient.Parameters.AddWithValue("@idEq", joueur.IdEquipe);
                insertAppartient.ExecuteNonQuery();
            }

            ChargerJoueurs();
        }


        public void ModifierJoueur(Joueur joueur)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var updateJoueur = new MySqlCommand(
                    "UPDATE joueur SET NomJoueur = @nom, PrenomJoueur = @prenom, NumJoueur = @num, PosteJoueur = @poste WHERE idJoueur = @id",
                    connection);
                updateJoueur.Parameters.AddWithValue("@nom", joueur.NomJoueur);
                updateJoueur.Parameters.AddWithValue("@prenom", joueur.PrenomJoueur);
                updateJoueur.Parameters.AddWithValue("@num", joueur.NumJoueur);
                updateJoueur.Parameters.AddWithValue("@poste", joueur.PosteJoueur);
                updateJoueur.Parameters.AddWithValue("@id", joueur.IdJoueur);
                updateJoueur.ExecuteNonQuery();

                // Modifier appartient
                var updateAppartient = new MySqlCommand(
                    "UPDATE appartient SET idEq = @idEq WHERE idJoueur = @idJoueur",
                    connection);
                updateAppartient.Parameters.AddWithValue("@idEq", joueur.IdEquipe);
                updateAppartient.Parameters.AddWithValue("@idJoueur", joueur.IdJoueur);
                updateAppartient.ExecuteNonQuery();
            }

            ChargerJoueurs();
        }


        public void SupprimerJoueur(Joueur joueur)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Supprimer d'abord dans appartient
                var deleteAppartient = new MySqlCommand(
                    "DELETE FROM appartient WHERE idJoueur = @idJoueur",
                    connection);
                deleteAppartient.Parameters.AddWithValue("@idJoueur", joueur.IdJoueur);
                deleteAppartient.ExecuteNonQuery();

                // Supprimer dans joueur
                var deleteJoueur = new MySqlCommand(
                    "DELETE FROM joueur WHERE idJoueur = @id",
                    connection);
                deleteJoueur.Parameters.AddWithValue("@id", joueur.IdJoueur);
                deleteJoueur.ExecuteNonQuery();
            }

            ChargerJoueurs();
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
