using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class AddOrgIdTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizeIdentity_Organizes_OrganizeId",
                table: "OrganizeIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizeIdentity_Users_UserId",
                table: "OrganizeIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
                table: "ProjectIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectIdentity_Users_UserId",
                table: "ProjectIdentity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectIdentity",
                table: "ProjectIdentity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizeIdentity",
                table: "OrganizeIdentity");

            migrationBuilder.RenameTable(
                name: "ProjectIdentity",
                newName: "ProjectIdentities");

            migrationBuilder.RenameTable(
                name: "OrganizeIdentity",
                newName: "OrganizeIdentities");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectIdentity_UserId",
                table: "ProjectIdentities",
                newName: "IX_ProjectIdentities_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectIdentity_ProjectId",
                table: "ProjectIdentities",
                newName: "IX_ProjectIdentities_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizeIdentity_UserId",
                table: "OrganizeIdentities",
                newName: "IX_OrganizeIdentities_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizeIdentity_OrganizeId",
                table: "OrganizeIdentities",
                newName: "IX_OrganizeIdentities_OrganizeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectIdentities",
                table: "ProjectIdentities",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizeIdentities",
                table: "OrganizeIdentities",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizeIdentities_Organizes_OrganizeId",
                table: "OrganizeIdentities",
                column: "OrganizeId",
                principalTable: "Organizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizeIdentities_Users_UserId",
                table: "OrganizeIdentities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectIdentities_Projects_ProjectId",
                table: "ProjectIdentities",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectIdentities_Users_UserId",
                table: "ProjectIdentities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizeIdentities_Organizes_OrganizeId",
                table: "OrganizeIdentities");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizeIdentities_Users_UserId",
                table: "OrganizeIdentities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectIdentities_Projects_ProjectId",
                table: "ProjectIdentities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectIdentities_Users_UserId",
                table: "ProjectIdentities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectIdentities",
                table: "ProjectIdentities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizeIdentities",
                table: "OrganizeIdentities");

            migrationBuilder.RenameTable(
                name: "ProjectIdentities",
                newName: "ProjectIdentity");

            migrationBuilder.RenameTable(
                name: "OrganizeIdentities",
                newName: "OrganizeIdentity");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectIdentities_UserId",
                table: "ProjectIdentity",
                newName: "IX_ProjectIdentity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectIdentities_ProjectId",
                table: "ProjectIdentity",
                newName: "IX_ProjectIdentity_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizeIdentities_UserId",
                table: "OrganizeIdentity",
                newName: "IX_OrganizeIdentity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizeIdentities_OrganizeId",
                table: "OrganizeIdentity",
                newName: "IX_OrganizeIdentity_OrganizeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectIdentity",
                table: "ProjectIdentity",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizeIdentity",
                table: "OrganizeIdentity",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizeIdentity_Organizes_OrganizeId",
                table: "OrganizeIdentity",
                column: "OrganizeId",
                principalTable: "Organizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizeIdentity_Users_UserId",
                table: "OrganizeIdentity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectIdentity_Projects_ProjectId",
                table: "ProjectIdentity",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectIdentity_Users_UserId",
                table: "ProjectIdentity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
