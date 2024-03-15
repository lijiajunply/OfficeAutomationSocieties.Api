using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class AddOrg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_UserId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_GanttModel_Projects_ProjectModelId",
                table: "GanttModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Users_UserId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_GanttModel_ProjectModelId",
                table: "GanttModel");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "GanttModel");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Resources",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_UserId",
                table: "Resources",
                newName: "IX_Resources_OwnerId");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "GanttModel",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Announcements",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_UserId",
                table: "Announcements",
                newName: "IX_Announcements_OwnerId");

            migrationBuilder.AddColumn<string>(
                name: "OrganizeModelId",
                table: "Projects",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "GanttModel",
                type: "varchar(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Organizes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(256)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false),
                    Introduce = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizeModelUserModel",
                columns: table => new
                {
                    MemberUserId = table.Column<string>(type: "varchar(256)", nullable: false),
                    OrganizesId = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizeModelUserModel", x => new { x.MemberUserId, x.OrganizesId });
                    table.ForeignKey(
                        name: "FK_OrganizeModelUserModel_Organizes_OrganizesId",
                        column: x => x.OrganizesId,
                        principalTable: "Organizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizeModelUserModel_Users_MemberUserId",
                        column: x => x.MemberUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizeModelId",
                table: "Projects",
                column: "OrganizeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GanttModel_ProjectId",
                table: "GanttModel",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GanttModel_UserId",
                table: "GanttModel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizeModelUserModel_OrganizesId",
                table: "OrganizeModelUserModel",
                column: "OrganizesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Organizes_OwnerId",
                table: "Announcements",
                column: "OwnerId",
                principalTable: "Organizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Organizes_OrganizeModelId",
                table: "Projects",
                column: "OrganizeModelId",
                principalTable: "Organizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Organizes_OwnerId",
                table: "Resources",
                column: "OwnerId",
                principalTable: "Organizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Organizes_OwnerId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_GanttModel_Projects_ProjectId",
                table: "GanttModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GanttModel_Users_UserId",
                table: "GanttModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Organizes_OrganizeModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Organizes_OwnerId",
                table: "Resources");

            migrationBuilder.DropTable(
                name: "OrganizeModelUserModel");

            migrationBuilder.DropTable(
                name: "Organizes");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OrganizeModelId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_GanttModel_ProjectId",
                table: "GanttModel");

            migrationBuilder.DropIndex(
                name: "IX_GanttModel_UserId",
                table: "GanttModel");

            migrationBuilder.DropColumn(
                name: "OrganizeModelId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "GanttModel");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Resources",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_OwnerId",
                table: "Resources",
                newName: "IX_Resources_UserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "GanttModel",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Announcements",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_OwnerId",
                table: "Announcements",
                newName: "IX_Announcements_UserId");

            migrationBuilder.AddColumn<string>(
                name: "ProjectModelId",
                table: "GanttModel",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GanttModel_ProjectModelId",
                table: "GanttModel",
                column: "ProjectModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_UserId",
                table: "Announcements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GanttModel_Projects_ProjectModelId",
                table: "GanttModel",
                column: "ProjectModelId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Users_UserId",
                table: "Resources",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
