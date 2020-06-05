using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheet.Persistence.Migrations
{
    public partial class WorkedHoursHourQuantityDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HoursQuantity",
                table: "WorkedHours",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HoursQuantity",
                table: "WorkedHours",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
