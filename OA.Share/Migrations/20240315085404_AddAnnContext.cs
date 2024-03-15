using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class AddAnnContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Context",
                table: "Announcements",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Context",
                table: "Announcements",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");
        }
    }
}
