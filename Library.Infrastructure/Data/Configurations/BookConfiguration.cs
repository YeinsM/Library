using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(e => e.BookId)
            .HasName("libro_id");

        builder.Property(e => e.Title)
            .HasColumnName("titulo")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.YearPublished)
            .HasColumnName("año_publicacion")
            .IsRequired();

        builder.Property(e => e.Genre)
            .HasColumnName("genero")
            .HasMaxLength(60);

        builder.HasOne(e => e.Author)
               .WithMany(a => a.Books)
               .HasForeignKey(e => e.AuthorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
