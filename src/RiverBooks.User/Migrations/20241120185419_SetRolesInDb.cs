using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RiverBooks.User.Migrations;

  /// <inheritdoc />
  public partial class SetRolesInDb : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.InsertData(
              schema: "User",
              table: "AspNetRoles",
              columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
              values: new object[,]
              {
                  { "1245222e-0adf-4218-bf77-1a2a31010e65", null, "Admin", "ADMIN" },
                  { "e7b56b86-d8ec-4eea-bc19-d73bca66481f", null, "User", "USER" }
              });
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DeleteData(
              schema: "User",
              table: "AspNetRoles",
              keyColumn: "Id",
              keyValue: "1245222e-0adf-4218-bf77-1a2a31010e65");

          migrationBuilder.DeleteData(
              schema: "User",
              table: "AspNetRoles",
              keyColumn: "Id",
              keyValue: "e7b56b86-d8ec-4eea-bc19-d73bca66481f");
      }
  }
