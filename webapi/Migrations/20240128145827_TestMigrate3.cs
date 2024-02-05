using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class TestMigrate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "SubTasks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SubtaskId",
                table: "SubTasks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProjectName",
                table: "Projects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Projects",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BranchName",
                table: "Branches",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Branches",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Assignees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignees");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SubTasks",
                newName: "TaskName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SubTasks",
                newName: "SubtaskId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projects",
                newName: "ProjectName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Projects",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Branches",
                newName: "BranchName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Branches",
                newName: "BranchId");
        }
    }
}
