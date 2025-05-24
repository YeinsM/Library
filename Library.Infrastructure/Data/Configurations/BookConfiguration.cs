using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Libros");

        builder.HasKey(b => b.BookId)
            .HasName("libro_id");

        builder.HasIndex(b => b.AuthorId);

        builder.Property(b => b.AuthorId)
            .HasColumnName("autor_id")
            .IsRequired();

        builder.Property(b => b.Title)
            .HasColumnName("titulo")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.YearPublished)
            .HasColumnName("año_publicacion")
            .IsRequired();

        builder.Property(b => b.Genre)
            .HasColumnName("genero")
            .HasMaxLength(60);

        builder.HasOne(b => b.Author)
               .WithMany(a => a.Books)
               .HasForeignKey(b => b.AuthorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
