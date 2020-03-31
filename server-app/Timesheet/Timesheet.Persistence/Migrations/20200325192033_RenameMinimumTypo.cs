using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheet.Persistence.Migrations
{
    public partial class RenameMinimumTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimumTimeRate",
                table: "Salaries",
                newName: "MinimalTimeRate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimalTimeRate",
                table: "Salaries",
                newName: "MinimumTimeRate");
        }
    }
}
