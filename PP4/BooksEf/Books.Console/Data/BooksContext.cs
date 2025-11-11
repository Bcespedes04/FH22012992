using Microsoft.EntityFrameworkCore;
using Books.Console.Models;

namespace Books.Console.Data;

public class BooksContext : DbContext
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Title> Titles => Set<Title>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<TitleTag> TitlesTags => Set<TitleTag>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // ./data/books.db
        var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
        Directory.CreateDirectory(dataDir);
        var dbPath = Path.Combine(dataDir, "books.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Cambiar el nombre de la tabla TitleTag a TitlesTags
        modelBuilder.Entity<TitleTag>()
            .ToTable("TitlesTags");

        // 2. Orden de las columnas de Title: TitleId, AuthorId, TitleName
        modelBuilder.Entity<Title>(entity =>
        {
            entity.Property(e => e.TitleId).HasColumnOrder(0);
            entity.Property(e => e.AuthorId).HasColumnOrder(1);
            entity.Property(e => e.TitleName).HasColumnOrder(2);
        });

        // Relaciones explícitas (no es obligatorio pero queda claro)
        modelBuilder.Entity<Title>()
            .HasOne(t => t.Author)
            .WithMany(a => a.Titles)
            .HasForeignKey(t => t.AuthorId);

        modelBuilder.Entity<TitleTag>()
            .HasOne(tt => tt.Title)
            .WithMany(t => t.TitleTags)
            .HasForeignKey(tt => tt.TitleId);

        modelBuilder.Entity<TitleTag>()
            .HasOne(tt => tt.Tag)
            .WithMany(t => t.TitleTags)
            .HasForeignKey(tt => tt.TagId);
    }
}
