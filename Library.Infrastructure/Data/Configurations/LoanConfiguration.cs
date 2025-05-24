using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder) {
            builder.ToTable("Prestamos");

            builder.HasKey(l => l.LoanId)
                .HasName("prestamo_id");

            builder.HasIndex(l => l.DueDate);
            builder.HasIndex(l => l.BookId);

            builder.Property(l => l.LoanDate)
                .HasColumnName("fecha_prestamo")
                .IsRequired();

            builder.Property(l => l.DueDate)
                .HasColumnName("fecha_devolucion");

            builder.Property(l => l.BookId)
                .HasColumnName("libro_id")
                .IsRequired();

            builder.HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
