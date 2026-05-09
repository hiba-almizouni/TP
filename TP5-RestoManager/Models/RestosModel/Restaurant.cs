namespace RestoManager.Models.RestosModel;

public class Restaurant
{
    public Restaurant()
    {
        LesAvis = new HashSet<Avis>();
    }

    public int    CodeResto   { get; set; }
    public string NomResto    { get; set; } = null!;
    public string Specialite  { get; set; } = "Tunisienne";
    public string Ville       { get; set; } = null!;
    public string Tel         { get; set; } = null!;
    public int    NumProp     { get; set; }

    // Propriétés de navigation
    public virtual Proprietaire? LeProprio { get; set; }
    public virtual ICollection<Avis> LesAvis { get; set; }
}
