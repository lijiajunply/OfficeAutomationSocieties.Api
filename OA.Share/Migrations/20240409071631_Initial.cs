using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(64)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    Introduce = table.Column<string>(type: "varchar(512)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(64)", nullable: false),
                    RegistrationTime = table.Column<string>(type: "varchar(32)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    PhoneNum = table.Column<string>(type: "varchar(13)", nullable: false),
                    Password = table.Column<string>(type: "varchar(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(64)", nullable: false),
                    Context = table.Column<string>(type: "varchar(500)", nullable: false),
                    Title = table.Column<string>(type: "varchar(25)", nullable: false),
                    OwnerId = table.Column<string>(type: "varchar(64)", nullable: false),
                    Time = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_Organizes_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Organizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(64)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    Introduce = table.Column<string>(type: "varchar(512)", nullable: false),
                    OrganizeModelId = table.Column<string>(type: "varchar(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Organizes_OrganizeModelId",
                        column: x => x.OrganizeModelId,
                        principalTable: "Organizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(64)", nullable: false),
                    OwnerId = table.Column<string>(type: "varchar(64)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    Introduce = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreateTime = table.Column<string>(type: "varchar(64)", nullable: false),
                    StartTime = table.Column<string>(type: "varchar(64)", nullable: true),
                    EndTime = table.Column<string>(type: "varchar(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Organizes_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Organizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizeIdentities",
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
                    table.PrimaryKey("PK_OrganizeIdentities", x => x.Key);
                    table.ForeignKey(
                        name: "FK_OrganizeIdentities_Organizes_OrganizeId",
                        column: x => x.OrganizeId,
                        principalTable: "Organizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizeIdentities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(64)", nullable: false),
                    Path = table.Column<string>(type: "varchar(256)", nullable: false),
                    Url = table.Column<string>(type: "varchar(256)", nullable: false),
                    IsFolder = table.Column<bool>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<string>(type: "varchar(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Projects_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GanttList",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(64)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(64)", nullable: false),
                    ProjectId = table.Column<string>(type: "varchar(64)", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    StartTime = table.Column<string>(type: "varchar(64)", nullable: false),
                    EndTime = table.Column<string>(type: "varchar(64)", nullable: false),
                    ToDo = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GanttList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GanttList_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GanttList_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectIdentities",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identity = table.Column<string>(type: "varchar(10)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(64)", nullable: false),
                    ProjectId = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIdentities", x => x.Key);
                    table.ForeignKey(
                        name: "FK_ProjectIdentities_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectIdentities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_OwnerId",
                table: "Announcements",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_OwnerId",
                table: "Files",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GanttList_ProjectId",
                table: "GanttList",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GanttList_UserId",
                table: "GanttList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizeIdentities_OrganizeId",
                table: "OrganizeIdentities",
                column: "OrganizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizeIdentities_UserId",
                table: "OrganizeIdentities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIdentities_ProjectId",
                table: "ProjectIdentities",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIdentities_UserId",
                table: "ProjectIdentities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizeModelId",
                table: "Projects",
                column: "OrganizeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_OwnerId",
                table: "Resources",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "GanttList");

            migrationBuilder.DropTable(
                name: "OrganizeIdentities");

            migrationBuilder.DropTable(
                name: "ProjectIdentities");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizes");
        }
    }
}
