using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class IdKeyString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectIdentities_Key",
                table: "ProjectIdentities");

            migrationBuilder.DropIndex(
                name: "IX_OrganizeIdentities_Key",
                table: "OrganizeIdentities");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ProjectIdentities",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "OrganizeIdentities",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Key",
                table: "ProjectIdentities",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Key",
                table: "OrganizeIdentities",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)")
                .Annotation("Sqlite:Autoincrement", true);

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
    }
}
