using System.Windows;
using GestionEquipeFoot.ViewModels;

namespace GestionEquipeFoot.Views
{
    public partial class LoginWindow : Window
    {
        private LoginViewModel viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            viewModel = new LoginViewModel();
        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Login = LoginTextBox.Text;
            viewModel.MotDePasse = PasswordBox.Password;

            if (viewModel.Connexion())
            {
                // Connexion réussie
                MainWindow mainWindow = new MainWindow(viewModel.UtilisateurConnecte);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login ou mot de passe incorrect.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
