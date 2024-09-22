using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotesBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30193151-8c24-4a8d-af04-244830ef6e0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eecbf0df-4cd5-4429-b34f-f4c96711cbd2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49dad0c4-e7f7-4e41-9e6b-ddede5cc99dc", null, "User", "USER" },
                    { "6a95b957-cbeb-4822-a7e0-9372238c1445", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49dad0c4-e7f7-4e41-9e6b-ddede5cc99dc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a95b957-cbeb-4822-a7e0-9372238c1445");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30193151-8c24-4a8d-af04-244830ef6e0d", null, "Admin", "ADMIN" },
                    { "eecbf0df-4cd5-4429-b34f-f4c96711cbd2", null, "User", "USER" }
                });
        }
    }
}
