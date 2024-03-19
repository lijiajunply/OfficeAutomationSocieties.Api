using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class AddProjIdKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
                table: "ProjectIdentity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectIdentity",
                table: "ProjectIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectIdentity",
                type: "varchar(64)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "ProjectIdentity",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectIdentity",
                table: "ProjectIdentity",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIdentity_UserId",
                table: "ProjectIdentity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
                table: "ProjectIdentity",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
                table: "ProjectIdentity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectIdentity",
                table: "ProjectIdentity");

            migrationBuilder.DropIndex(
                name: "IX_ProjectIdentity_UserId",
                table: "ProjectIdentity");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "ProjectIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectIdentity",
                type: "varchar(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectIdentity",
                table: "ProjectIdentity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
                table: "ProjectIdentity",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
