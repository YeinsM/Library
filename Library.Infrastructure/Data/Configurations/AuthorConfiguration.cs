using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Autores");

            builder.HasKey(a => a.AuthorId)
                .HasName("autor_id");

            builder.Property(a => a.Name)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Nationality)
                .HasColumnName("nacionalidad")
                .IsRequired()
                .HasMaxLength(70);
        }
    }
}
