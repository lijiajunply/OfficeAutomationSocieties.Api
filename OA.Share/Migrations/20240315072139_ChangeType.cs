using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class ChangeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "IdentityModel",
                type: "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "IdentityModel",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");
        }
    }
}
