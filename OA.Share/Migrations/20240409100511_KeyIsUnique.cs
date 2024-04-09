using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class KeyIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectIdentities_Key",
                table: "ProjectIdentities",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizeIdentities_Key",
                table: "OrganizeIdentities",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectIdentities_Key",
                table: "ProjectIdentities");

            migrationBuilder.DropIndex(
                name: "IX_OrganizeIdentities_Key",
                table: "OrganizeIdentities");
        }
    }
}
