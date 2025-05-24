using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nacionalidad = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("autor_id", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    autor_id = table.Column<int>(type: "int", nullable: false),
                    año_publicacion = table.Column<int>(type: "int", nullable: false),
                    genero = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("libro_id", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Libros_Autores_autor_id",
                        column: x => x.autor_id,
                        principalTable: "Autores",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libro_id = table.Column<int>(type: "int", nullable: false),
                    fecha_prestamo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_devolucion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("prestamo_id", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_Prestamos_Libros_libro_id",
                        column: x => x.libro_id,
                        principalTable: "Libros",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_autor_id",
                table: "Libros",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_fecha_devolucion",
                table: "Prestamos",
                column: "fecha_devolucion");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_libro_id",
                table: "Prestamos",
                column: "libro_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
