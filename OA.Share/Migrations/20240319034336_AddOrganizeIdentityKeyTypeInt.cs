using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizeIdentityKeyTypeInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityModel");

            migrationBuilder.CreateTable(
                name: "OrganizeIdentity",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identity = table.Column<string>(type: "varchar(10)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(64)", nullable: false),
                    OrganizeId = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizeIdentity", x => x.Key);
                    table.ForeignKey(
                        name: "FK_OrganizeIdentity_Organizes_OrganizeId",
                        column: x => x.OrganizeId,
                        principalTable: "Organizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizeIdentity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizeIdentity_OrganizeId",
                table: "OrganizeIdentity",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizeIdentity_UserId",
                table: "OrganizeIdentity",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizeIdentity");

            migrationBuilder.CreateTable(
                name: "IdentityModel",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(64)", nullable: false),
                    OrganizeId = table.Column<string>(type: "varchar(64)", nullable: false),
                    Identity = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityModel", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_IdentityModel_Organizes_OrganizeId",
                        column: x => x.OrganizeId,
                        principalTable: "Organizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityModel_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityModel_OrganizeId",
                table: "IdentityModel",
                column: "OrganizeId");
        }
    }
}
