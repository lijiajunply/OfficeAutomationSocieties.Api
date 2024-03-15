using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identity",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "IdentityModel",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(10)", nullable: false),
                    Identity = table.Column<string>(type: "varchar(10)", nullable: false),
                    OwnerId = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityModel", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_IdentityModel_Organizes_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Organizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityModel_OwnerId",
                table: "IdentityModel",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityModel");

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "Users",
                type: "varchar(256)",
                nullable: false,
                defaultValue: "");
        }
    }
}
