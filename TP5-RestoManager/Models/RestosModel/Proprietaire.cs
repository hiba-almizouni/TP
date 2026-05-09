namespace RestoManager.Models.RestosModel;

public class Proprietaire
{
    public Proprietaire()
    {
        LesRestos = new HashSet<Restaurant>();
    }

    public int    Numero { get; set; }
    public string Nom    { get; set; } = null!;
    public string Email  { get; set; } = null!;
    public string Gsm    { get; set; } = null!;

    // Propriété de navigation 1-*
    public virtual ICollection<Restaurant> LesRestos { get; set; }
}
