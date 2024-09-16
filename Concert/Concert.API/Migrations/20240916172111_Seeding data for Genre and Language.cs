using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Concert.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforGenreandLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5fb21249-08bc-4585-ae71-48392889955f"), "EBM" },
                    { new Guid("9fc5f185-c6c3-4bcd-90c0-74e35304d69c"), "Reagge" },
                    { new Guid("f3a2308a-3774-4882-8cab-e1b52ce0b48a"), "EDM" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("42dc4be9-24e1-450d-a7f2-c0ae9d722880"), "French" },
                    { new Guid("a783935f-bace-44d2-88c6-bfac4f3f331e"), "English" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5fb21249-08bc-4585-ae71-48392889955f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("9fc5f185-c6c3-4bcd-90c0-74e35304d69c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f3a2308a-3774-4882-8cab-e1b52ce0b48a"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("42dc4be9-24e1-450d-a7f2-c0ae9d722880"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("a783935f-bace-44d2-88c6-bfac4f3f331e"));
        }
    }
}
