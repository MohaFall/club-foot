namespace GestionEquipeFoot.Models
{
    public class Joueur
    {
        public int IdJoueur { get; set; }
        public string NomJoueur { get; set; }
        public string PrenomJoueur { get; set; }
        public int NumJoueur { get; set; }
        public string PosteJoueur { get; set; }
        public string NomEquipe { get; set; }
        public int IdEquipe { get; set; } // pour faire le lien

    }
}
