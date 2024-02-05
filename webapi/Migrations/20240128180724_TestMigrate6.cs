using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class TestMigrate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestDescription",
                table: "SubTasks");

            migrationBuilder.DropColumn(
                name: "TestDescription",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TestDescription",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "TestDescription",
                table: "Assignees");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "SubTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AssigneeProject",
                columns: table => new
                {
                    AssigneesId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssigneeProject", x => new { x.AssigneesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_AssigneeProject_Assignees_AssigneesId",
                        column: x => x.AssigneesId,
                        principalTable: "Assignees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssigneeProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssigneeSubTask",
                columns: table => new
                {
                    AssigneesId = table.Column<int>(type: "int", nullable: false),
                    SubTasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssigneeSubTask", x => new { x.AssigneesId, x.SubTasksId });
                    table.ForeignKey(
                        name: "FK_AssigneeSubTask_Assignees_AssigneesId",
                        column: x => x.AssigneesId,
                        principalTable: "Assignees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssigneeSubTask_SubTasks_SubTasksId",
                        column: x => x.SubTasksId,
                        principalTable: "SubTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_BranchId",
                table: "SubTasks",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ProjectId",
                table: "Branches",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigneeProject_ProjectsId",
                table: "AssigneeProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigneeSubTask_SubTasksId",
                table: "AssigneeSubTask",
                column: "SubTasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Projects_ProjectId",
                table: "Branches",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Branches_BranchId",
                table: "SubTasks",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Projects_ProjectId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Branches_BranchId",
                table: "SubTasks");

            migrationBuilder.DropTable(
                name: "AssigneeProject");

            migrationBuilder.DropTable(
                name: "AssigneeSubTask");

            migrationBuilder.DropIndex(
                name: "IX_SubTasks_BranchId",
                table: "SubTasks");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ProjectId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "SubTasks");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Branches");

            migrationBuilder.AddColumn<string>(
                name: "TestDescription",
                table: "SubTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestDescription",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TestDescription",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TestDescription",
                table: "Assignees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
