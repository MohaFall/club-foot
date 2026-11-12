using System.Windows;
using GestionEquipeFoot.Models;

namespace GestionEquipeFoot.Views
{
    public partial class GestionAdminWindow : Window
    {
        private Utilisateur utilisateurConnecte;

        public GestionAdminWindow(Utilisateur utilisateur)
        {
            InitializeComponent();
            utilisateurConnecte = utilisateur;
        }

        private void BtnGererEquipes_Click(object sender, RoutedEventArgs e)
        {
            GestionEquipesWindow gestionEquipesWindow = new GestionEquipesWindow(utilisateurConnecte);
            gestionEquipesWindow.Show();
            this.Close();
        }

        private void BtnGererJoueurs_Click(object sender, RoutedEventArgs e)
        {
            GestionJoueursWindow gestionJoueursWindow = new GestionJoueursWindow(utilisateurConnecte);
            gestionJoueursWindow.Show();
            this.Close();
        }

        private void BtnGererRencontres_Click(object sender, RoutedEventArgs e)
        {
            GestionRencontresWindow gestionRencontresWindow = new GestionRencontresWindow(utilisateurConnecte);
            gestionRencontresWindow.Show();
            this.Close();
        }


        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(utilisateurConnecte);
            mainWindow.Show();
            this.Close();
        }
    }
}
