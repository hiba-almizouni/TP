using Microsoft.EntityFrameworkCore;

namespace RestoManager.Models.RestosModel;

public class RestosDbContext : DbContext
{
    public RestosDbContext(DbContextOptions<RestosDbContext> options)
        : base(options) { }

    public DbSet<Proprietaire> Proprietaires { get; set; } = null!;
    public DbSet<Restaurant>   Restaurants   { get; set; } = null!;
    public DbSet<Avis>         Avis          { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ── Configuration de l'entité Proprietaire ────────────────────────
        modelBuilder.Entity<Proprietaire>(entity =>
        {
            entity.ToTable("TProprietaire", schema: "resto");
            entity.HasKey(e => e.Numero);

            entity.Property(e => e.Nom)
                  .HasColumnName("NomProp")
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(e => e.Email)
                  .HasColumnName("EmailProp")
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(e => e.Gsm)
                  .HasColumnName("GsmProp")
                  .HasMaxLength(8)
                  .IsRequired();
        });

        // ── Configuration de l'entité Restaurant ──────────────────────────
        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.ToTable("TRestaurant", schema: "resto");
            entity.HasKey(e => e.CodeResto);

            entity.Property(e => e.NomResto)
                  .HasColumnName("NomResto")
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(e => e.Specialite)
                  .HasColumnName("SpecResto")
                  .HasMaxLength(20)
                  .IsRequired()
                  .HasDefaultValue("Tunisienne");

            entity.Property(e => e.Ville)
                  .HasColumnName("VilleResto")
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(e => e.Tel)
                  .HasColumnName("TelResto")
                  .HasMaxLength(8)
                  .IsRequired();
        });

        // ── Configuration de l'association 1-* Proprietaire → Restaurant ──
        modelBuilder.Entity<Restaurant>()
            .HasOne(r => r.LeProprio)
            .WithMany(p => p.LesRestos)
            .HasForeignKey(r => r.NumProp)
            .IsRequired()
            .HasConstraintName("Relation_Proprio_Restos");

        // ── Configuration de l'entité Avis ────────────────────────────────
        modelBuilder.Entity<Avis>(entity =>
        {
            entity.ToTable("TAvis", schema: "admin");
            entity.HasKey(e => e.CodeAvis);

            entity.Property(e => e.NomPersonne)
                  .HasColumnName("NomPersonne")
                  .HasMaxLength(30)
                  .IsRequired();

            entity.Property(e => e.Note)
                  .HasColumnName("Note")
                  .IsRequired();

            entity.Property(e => e.Commentaire)
                  .HasColumnName("Commentaire")
                  .HasMaxLength(256)
                  .IsRequired(false);
        });

        // ── Configuration de l'association 1-* Restaurant → Avis ──────────
        modelBuilder.Entity<Avis>()
            .HasOne(a => a.LeResto)
            .WithMany(r => r.LesAvis)
            .HasForeignKey(a => a.NumResto)
            .IsRequired()
            .HasConstraintName("Relation_Resto_Avis");
    }
}
