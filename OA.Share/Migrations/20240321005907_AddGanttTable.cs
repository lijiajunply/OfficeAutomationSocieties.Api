using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class AddGanttTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GanttModel_Projects_ProjectId",
                table: "GanttModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GanttModel_Users_UserId",
                table: "GanttModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GanttModel",
                table: "GanttModel");

            migrationBuilder.RenameTable(
                name: "GanttModel",
                newName: "GanttList");

            migrationBuilder.RenameIndex(
                name: "IX_GanttModel_UserId",
                table: "GanttList",
                newName: "IX_GanttList_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GanttModel_ProjectId",
                table: "GanttList",
                newName: "IX_GanttList_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GanttList",
                table: "GanttList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GanttList_Projects_ProjectId",
                table: "GanttList",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GanttList_Users_UserId",
                table: "GanttList",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GanttList_Projects_ProjectId",
                table: "GanttList");

            migrationBuilder.DropForeignKey(
                name: "FK_GanttList_Users_UserId",
                table: "GanttList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GanttList",
                table: "GanttList");

            migrationBuilder.RenameTable(
                name: "GanttList",
                newName: "GanttModel");

            migrationBuilder.RenameIndex(
                name: "IX_GanttList_UserId",
                table: "GanttModel",
                newName: "IX_GanttModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GanttList_ProjectId",
                table: "GanttModel",
                newName: "IX_GanttModel_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GanttModel",
                table: "GanttModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GanttModel_Projects_ProjectId",
                table: "GanttModel",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GanttModel_Users_UserId",
                table: "GanttModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
