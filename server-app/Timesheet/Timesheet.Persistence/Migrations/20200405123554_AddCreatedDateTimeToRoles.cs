using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Timesheet.Persistence.Entities.Identities;

namespace Timesheet.Persistence.Migrations
{
    public partial class AddCreatedDateTimeToRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "CreatedDateTime",
                keyValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                column: "CreatedDateTime",
                value: new DateTime(2020, 3, 20));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "AspNetRoles");
        }
    }
}
