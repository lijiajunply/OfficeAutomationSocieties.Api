using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Share.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGantt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gantt",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "GanttModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(256)", nullable: false),
                    User = table.Column<string>(type: "varchar(256)", nullable: false),
                    StartTime = table.Column<string>(type: "varchar(256)", nullable: false),
                    EndTime = table.Column<string>(type: "varchar(256)", nullable: false),
                    ToDo = table.Column<string>(type: "varchar(256)", nullable: false),
                    ProjectModelId = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GanttModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GanttModel_Projects_ProjectModelId",
                        column: x => x.ProjectModelId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GanttModel_ProjectModelId",
                table: "GanttModel",
                column: "ProjectModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GanttModel");

            migrationBuilder.AddColumn<string>(
                name: "Gantt",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
