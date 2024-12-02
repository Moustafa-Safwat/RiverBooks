using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiverBooks.Orderprocessing.Migrations
{
    /// <inheritdoc />
    public partial class InitOrderProcessing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "OrderProcessing");

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "OrderProcessing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BillingAddress_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BillingAddress_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BillingAddress_PostalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BillingAddress_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BillingAddress_Street1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BillingAddress_Street2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_PostalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_Street1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_Street2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "OrderProcessing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "OrderProcessing",
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "OrderProcessing",
                table: "OrderItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "OrderProcessing");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "OrderProcessing");
        }
    }
}
