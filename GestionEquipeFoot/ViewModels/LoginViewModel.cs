using MySql.Data.MySqlClient;
using System.ComponentModel;
using GestionEquipeFoot.Models;

namespace GestionEquipeFoot.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string connectionString = Database.ConnectionString;

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }

        private string _motDePasse;
        public string MotDePasse
        {
            get { return _motDePasse; }
            set { _motDePasse = value; OnPropertyChanged(nameof(MotDePasse)); }
        }

        public Utilisateur UtilisateurConnecte { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string nomPropriete)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }

        public bool Connexion()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // Find the account, then compare the typed password with the stored hash
                var command = new MySqlCommand("SELECT * FROM utilisateur WHERE login = @login", connection);
                command.Parameters.AddWithValue("@login", Login);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read() && PasswordHasher.Verify(MotDePasse ?? "", reader.GetString("mdp")))
                    {
                        UtilisateurConnecte = new Utilisateur
                        {
                            IdUt = reader.GetInt32("idUt"),
                            Login = reader.GetString("login"),
                            Role = reader.GetString("typeCompte")
                        };
                        return true; // Connexion réussie
                    }
                }
            }
            return false; // Connexion échouée
        }
    }
}
