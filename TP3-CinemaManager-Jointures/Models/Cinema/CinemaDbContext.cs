using Microsoft.EntityFrameworkCore;

namespace CinemaManager.Models.Cinema;

public partial class CinemaDbContext : DbContext
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        : base(options) { }

    public virtual DbSet<Producer> Producers { get; set; } = null!;
    public virtual DbSet<Movie> Movies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producer>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Nationality).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(30);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(30);
            entity.Property(e => e.Genre).HasMaxLength(20);

            entity.HasOne(d => d.Producer)
                  .WithMany(p => p.Movies)
                  .HasForeignKey(d => d.ProducerId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Movie_Prod");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
