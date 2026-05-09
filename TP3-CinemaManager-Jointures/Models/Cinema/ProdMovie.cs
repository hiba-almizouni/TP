namespace CinemaManager.Models.Cinema;

/// <summary>ViewModel pour les jointures Film-Producteur</summary>
public class ProdMovie
{
    public string? mTitle { get; set; }
    public string? mGenre { get; set; }
    public string? pName  { get; set; }
    public string? pNat   { get; set; }
}
