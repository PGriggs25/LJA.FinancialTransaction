using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LJA.FinancialTransaction.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Revenue" },
                    { 2, "Expenses" },
                    { 3, "Assets" },
                    { 4, "Liabilities" }
                });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bank Transfer" },
                    { 2, "Credit Card" },
                    { 3, "Cash" },
                    { 4, "Check" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "SourceId" },
                values: new object[,]
                {
                    { 1, 1.00m, 1, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 1", 1 },
                    { 2, 2.00m, 1, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 2", 2 },
                    { 3, 3.00m, 2, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 3", 1 },
                    { 4, 4.00m, 2, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 4", 2 },
                    { 5, 5.00m, 3, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 5", 1 },
                    { 6, 6.00m, 3, new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 6", 2 },
                    { 7, 7.00m, 3, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 7", 3 },
                    { 8, 8.00m, 4, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 8", 1 },
                    { 9, 9.00m, 4, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 9", 3 },
                    { 10, 10.00m, 4, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description 10", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceId",
                table: "Transactions",
                column: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Sources");
        }
    }
}
