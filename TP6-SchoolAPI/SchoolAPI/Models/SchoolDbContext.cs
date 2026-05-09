using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Models;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options) { }

    public DbSet<School> Schools { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<School>().HasData(
            new School { Id = 1, Name = "ENISo", Sections = "IA, GTE, GMP",
                Director = "Directeur ENISo", Rating = 3.5, WebSite = "http://www.eniso.rnu.tn" },
            new School { Id = 2, Name = "ENIM", Sections = "Mécanique, Énergétique, Textile",
                Director = "Directeur ENIM", Rating = 2.8, WebSite = null },
            new School { Id = 3, Name = "ENIT", Sections = "Télécom, Info, Indus",
                Director = "Directeur ENIT", Rating = 4.0, WebSite = "http://www.enit.rnu.tn" }
        );
    }
}
