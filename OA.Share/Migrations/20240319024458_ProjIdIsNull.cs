using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class ProjIdIsNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
                table: "ProjectIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectIdentity",
                type: "varchar(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
                table: "ProjectIdentity",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
                table: "ProjectIdentity",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
