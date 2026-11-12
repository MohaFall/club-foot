using System.Windows;
using GestionEquipeFoot.Models;

namespace GestionEquipeFoot.Views
{
    public partial class MainWindow : Window
    {
        private Utilisateur utilisateurConnecte;

        public MainWindow(Utilisateur utilisateur)
        {
            InitializeComponent();
            utilisateurConnecte = utilisateur;
            InitialiserInterface();
        }

        private void InitialiserInterface()
        {
            BienvenueText.Text = $"Bienvenue {utilisateurConnecte.Login} ({utilisateurConnecte.Role})";

            if (utilisateurConnecte.Role == "admin")
            {
                AdminPanel.Visibility = Visibility.Visible;
            }
            else
            {
                AdminPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnEquipes_Click(object sender, RoutedEventArgs e)
        {
            EquipeWindow equipeWindow = new EquipeWindow(utilisateurConnecte);
            equipeWindow.Show();
            this.Close();
        }

        private void BtnJoueurs_Click(object sender, RoutedEventArgs e)
        {
            JoueurWindow joueurWindow = new JoueurWindow(utilisateurConnecte);
            joueurWindow.Show();
            this.Close();
        }

        private void BtnRencontres_Click(object sender, RoutedEventArgs e)
        {
            RencontreWindow rencontreWindow = new RencontreWindow(utilisateurConnecte);
            rencontreWindow.Show();
            this.Close();
        }

        private void BtnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void BtnGestionAdmin_Click(object sender, RoutedEventArgs e)
        {
            GestionAdminWindow gestionAdminWindow = new GestionAdminWindow(utilisateurConnecte);
            gestionAdminWindow.Show();
            this.Close();
        }


    }
}
