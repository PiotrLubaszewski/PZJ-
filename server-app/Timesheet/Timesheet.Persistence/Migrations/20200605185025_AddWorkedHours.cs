using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheet.Persistence.Migrations
{
    public partial class AddWorkedHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkedHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HoursQuantity = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkedHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkedHours_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkedHours_ProjectTasks_ProjectTaskId",
                        column: x => x.ProjectTaskId,
                        principalTable: "ProjectTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkedHours_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkedHours_ProjectId",
                table: "WorkedHours",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkedHours_ProjectTaskId",
                table: "WorkedHours",
                column: "ProjectTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkedHours_UserId",
                table: "WorkedHours",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkedHours");
        }
    }
}
