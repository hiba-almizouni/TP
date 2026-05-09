using Microsoft.EntityFrameworkCore;

namespace Hotellerie_Hiba.Models.HotellerieModel
{
    public class HotellerieDbContext : DbContext
    {
        public HotellerieDbContext(DbContextOptions<HotellerieDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Appreciation> Appreciations { get; set; }
    }
}
