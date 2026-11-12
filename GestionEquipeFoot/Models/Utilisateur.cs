namespace GestionEquipeFoot.Models
{
    public class Utilisateur
    {
        public int IdUt { get; set; }
        public string Login { get; set; }
        public string Mdp { get; set; }
        public string Role { get; set; } // "Admin ou "Joueur"
    }
}
