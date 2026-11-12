using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GestionEquipeFoot.Models;
using System;

namespace GestionEquipeFoot.ViewModels
{
    public class RencontreViewModel : INotifyPropertyChanged
    {
        private string connectionString = Database.ConnectionString;

        public ObservableCollection<Rencontre> Rencontres { get; set; }

        public RencontreViewModel()
        {
            Rencontres = new ObservableCollection<Rencontre>();
            ChargerRencontres();
        }

        public void ChargerRencontres()
        {
            Rencontres.Clear();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var command = new MySqlCommand(
                @"SELECT r.idRencontre, r.dateRencontre,
                 eq1.NomEq AS Equipe1, p1.score AS Score1,
                 eq2.NomEq AS Equipe2, p2.score AS Score2
                  FROM rencontre r
                  JOIN participe p1 ON r.idRencontre = p1.idRencontre
                  JOIN participe p2 ON r.idRencontre = p2.idRencontre AND p1.idEq <> p2.idEq
                  JOIN equipe eq1 ON p1.idEq = eq1.idEq
                  JOIN equipe eq2 ON p2.idEq = eq2.idEq
                  WHERE p1.idEq < p2.idEq
                  ORDER BY r.dateRencontre;", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int score1 = reader.IsDBNull(reader.GetOrdinal("Score1")) ? -1 : reader.GetInt32("Score1");
                        int score2 = reader.IsDBNull(reader.GetOrdinal("Score2")) ? -1 : reader.GetInt32("Score2");

                        Rencontre rencontre = new Rencontre
                        {
                            IdRencontre = reader.GetInt32("idRencontre"),
                            DateRencontreRaw = reader.GetDateTime("dateRencontre"),
                            Equipe1 = reader.GetString("Equipe1"),
                            Equipe2 = reader.GetString("Equipe2"),
                            ScoreEquipe1 = (score1 < 0 && score2 < 0) ? "À jouer" : score1.ToString(),
                            ScoreEquipe2 = (score1 < 0 && score2 < 0) ? "À jouer" : score2.ToString()
                        };

                        Rencontres.Add(rencontre);
                    }
                }
            }
        }

        public void AjouterRencontre(int idEq1, int idEq2, DateTime dateRencontre)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Insérer la rencontre
                var insertRencontre = new MySqlCommand(
                    "INSERT INTO rencontre (dateRencontre) VALUES (@date)", connection);
                insertRencontre.Parameters.AddWithValue("@date", dateRencontre);
                insertRencontre.ExecuteNonQuery();

                int nouvelIdRencontre = (int)insertRencontre.LastInsertedId;

                // Insérer dans participe les deux équipes
                var insertParticipe1 = new MySqlCommand(
                    "INSERT INTO participe (idRencontre, idEq, score) VALUES (@idRencontre, @idEq, NULL)", connection);
                insertParticipe1.Parameters.AddWithValue("@idRencontre", nouvelIdRencontre);
                insertParticipe1.Parameters.AddWithValue("@idEq", idEq1);
                insertParticipe1.ExecuteNonQuery();

                var insertParticipe2 = new MySqlCommand(
                    "INSERT INTO participe (idRencontre, idEq, score) VALUES (@idRencontre, @idEq, NULL)", connection);
                insertParticipe2.Parameters.AddWithValue("@idRencontre", nouvelIdRencontre);
                insertParticipe2.Parameters.AddWithValue("@idEq", idEq2);
                insertParticipe2.ExecuteNonQuery();
            }

            ChargerRencontres();
        }

        public void ModifierRencontre(int idRencontre, DateTime dateRencontre, int score1, int score2)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Modifier la date
                var updateRencontre = new MySqlCommand(
                    "UPDATE rencontre SET dateRencontre = @date WHERE idRencontre = @id", connection);
                updateRencontre.Parameters.AddWithValue("@date", dateRencontre);
                updateRencontre.Parameters.AddWithValue("@id", idRencontre);
                updateRencontre.ExecuteNonQuery();

                // Récupérer les idEq des deux équipes participants
                var getEquipes = new MySqlCommand(
                    "SELECT idEq FROM participe WHERE idRencontre = @idRencontre ORDER BY idEq ASC", connection);
                getEquipes.Parameters.AddWithValue("@idRencontre", idRencontre);

                int idEquipe1 = 0;
                int idEquipe2 = 0;

                using (var reader = getEquipes.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        idEquipe1 = reader.GetInt32("idEq");
                    }
                    if (reader.Read())
                    {
                        idEquipe2 = reader.GetInt32("idEq");
                    }
                }

                // Mettre à jour le score pour équipe 1
                var updateScore1 = new MySqlCommand(
                    "UPDATE participe SET score = @score WHERE idRencontre = @idRencontre AND idEq = @idEquipe", connection);
                updateScore1.Parameters.AddWithValue("@score", score1);
                updateScore1.Parameters.AddWithValue("@idRencontre", idRencontre);
                updateScore1.Parameters.AddWithValue("@idEquipe", idEquipe1);
                updateScore1.ExecuteNonQuery();

                // Mettre à jour le score pour équipe 2
                var updateScore2 = new MySqlCommand(
                    "UPDATE participe SET score = @score WHERE idRencontre = @idRencontre AND idEq = @idEquipe", connection);
                updateScore2.Parameters.AddWithValue("@score", score2);
                updateScore2.Parameters.AddWithValue("@idRencontre", idRencontre);
                updateScore2.Parameters.AddWithValue("@idEquipe", idEquipe2);
                updateScore2.ExecuteNonQuery();
            }

            ChargerRencontres();
        }


        public void SupprimerRencontre(Rencontre rencontre)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Supprimer dans participe
                var deleteParticipe = new MySqlCommand(
                    "DELETE FROM participe WHERE idRencontre = @idRencontre", connection);
                deleteParticipe.Parameters.AddWithValue("@idRencontre", rencontre.IdRencontre);
                deleteParticipe.ExecuteNonQuery();

                // et supprimer dans rencontre
                var deleteRencontre = new MySqlCommand(
                    "DELETE FROM rencontre WHERE idRencontre = @idRencontre", connection);
                deleteRencontre.Parameters.AddWithValue("@idRencontre", rencontre.IdRencontre);
                deleteRencontre.ExecuteNonQuery();
            }

            ChargerRencontres();
        }






        public event PropertyChangedEventHandler PropertyChanged;
    }
}
