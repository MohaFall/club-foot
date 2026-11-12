using System;

public class Rencontre
{
    public int IdRencontre { get; set; }
    public DateTime DateRencontreRaw { get; set; }
    public string DateRencontre => DateRencontreRaw.ToString("dd/MM/yyyy HH:mm");

    public string Equipe1 { get; set; }
    public string Equipe2 { get; set; }
    public string ScoreEquipe1 { get; set; }
    public string ScoreEquipe2 { get; set; }
}
