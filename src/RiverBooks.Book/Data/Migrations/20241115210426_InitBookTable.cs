using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RiverBooks.Book.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Books");

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.CheckConstraint("Ck_Price_Positive", "[Price] >= 0");
                });

            migrationBuilder.InsertData(
                schema: "Books",
                table: "Books",
                columns: new[] { "Id", "Author", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("17c61e41-3953-42cd-8f88-d3f698869b35"), "Harper Lee", 12.99m, "To Kill a Mockingbird" },
                    { new Guid("17c61e41-3953-42cd-8f88-d3f698869b38"), "J.D. Salinger", 11.99m, "The Catcher in the Rye" },
                    { new Guid("a89f6cd7-4693-457b-9009-02205dbbfe45"), "F. Scott Fitzgerald", 10.99m, "The Great Gatsby" },
                    { new Guid("e4fa19bf-6981-4e50-a542-7c9b26e9ec31"), "George Orwell", 8.99m, "1984" },
                    { new Guid("e4fa19bf-6981-4e50-a542-7c9b26e9ec34"), "Jane Austen", 9.99m, "Pride and Prejudice" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books",
                schema: "Books");
        }
    }
}
