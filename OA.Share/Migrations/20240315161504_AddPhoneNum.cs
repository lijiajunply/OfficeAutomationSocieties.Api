using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNum",
                table: "Users",
                type: "varchar(13)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNum",
                table: "Users");
        }
    }
}
