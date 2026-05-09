using System.ComponentModel.DataAnnotations;

namespace RestoManager.Models.RestosModel;

public class Avis
{
    public int    CodeAvis     { get; set; }
    public string NomPersonne  { get; set; } = null!;

    [Range(1, 5)]
    public int    Note         { get; set; }

    public string? Commentaire { get; set; }
    public int     NumResto    { get; set; }

    // Propriété de navigation
    public virtual Restaurant? LeResto { get; set; }
}
