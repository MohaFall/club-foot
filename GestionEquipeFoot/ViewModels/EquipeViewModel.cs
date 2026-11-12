using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GestionEquipeFoot.Models;
using System.Collections.Generic;

namespace GestionEquipeFoot.ViewModels
{
    public class EquipeViewModel : INotifyPropertyChanged
    {
        private string connectionString = Database.ConnectionString;

        private ObservableCollection<Equipe> _equipes;
        public ObservableCollection<Equipe> Equipes
        {
            get { return _equipes; }
            set { _equipes = value; OnPropertyChanged(nameof(Equipes)); }
        }

        public EquipeViewModel()
        {
            Equipes = new ObservableCollection<Equipe>();
            ChargerEquipes();
        }

        public void ChargerEquipes()
        {
            Equipes.Clear();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT idEq, NomEq, PositionEq, image FROM equipe", connection);

                using (var reader = command.ExecuteReader())
                {
                    var tempList = new List<Equipe>();
                    while (reader.Read())
                    {
                        tempList.Add(new Equipe
                        {
                            IdEq = reader.GetInt32("idEq"),
                            NomEq = reader.GetString("NomEq"),
                            PositionEq = reader.GetInt32("PositionEq"), 
                            Photo = "/image/" + reader.GetString("image")
                        });
                    }

                    var equipesTriees = tempList.OrderBy(e => e.PositionEq).ToList();

                    foreach (var equipe in equipesTriees)
                    {
                        Equipes.Add(equipe);
                    }
                }
            }
        }


        public void AjouterEquipe(Equipe equipe)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO equipe (NomEq, PositionEq, image) VALUES (@nom, @position, @photo)", connection);
                command.Parameters.AddWithValue("@nom", equipe.NomEq);
                command.Parameters.AddWithValue("@position", equipe.PositionEq);
                command.Parameters.AddWithValue("@photo", "default.png"); // on force une image par défaut

                command.ExecuteNonQuery();
            }

            // Recharge la liste
            ChargerEquipes();
        }

        public void ModifierEquipe(Equipe equipe)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE equipe SET NomEq = @nom, PositionEq = @position WHERE idEq = @id", connection);
                command.Parameters.AddWithValue("@nom", equipe.NomEq);
                command.Parameters.AddWithValue("@position", equipe.PositionEq);
                command.Parameters.AddWithValue("@id", equipe.IdEq);

                command.ExecuteNonQuery();
            }

            ChargerEquipes();
        }

        public void SupprimerEquipe(Equipe equipe)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM equipe WHERE idEq = @id", connection);
                command.Parameters.AddWithValue("@id", equipe.IdEq);

                command.ExecuteNonQuery();
            }

            ChargerEquipes();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
