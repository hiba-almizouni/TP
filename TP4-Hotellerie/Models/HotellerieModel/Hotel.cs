using System.ComponentModel.DataAnnotations;

namespace Hotellerie_Hiba.Models.HotellerieModel
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de l'hôtel est obligatoire.")]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = "Le nom doit contenir entre 3 et 20 caractères.")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nombre d'étoiles est obligatoire.")]
        [Range(1, 5, ErrorMessage = "Le nombre d'étoiles doit être compris entre 1 et 5.")]
        public int Etoiles { get; set; }

        [Required(ErrorMessage = "La ville est obligatoire.")]
        public string Ville { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le site web est obligatoire.")]
        [Url(ErrorMessage = "Veuillez entrer une URL valide (ex: https://www.monhotel.com).")]
        [Display(Name = "Site Web")]
        public string SiteWeb { get; set; } = string.Empty;

        [Display(Name = "Téléphone")]
        public string? Tel { get; set; }

        public string? Pays { get; set; } = "Tunisie";

        // Propriété de navigation – relation 1-N
        public ICollection<Appreciation>? Appreciations { get; set; }
    }
}
