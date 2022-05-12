using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS2_api.Migrations
{
    public partial class time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "positionTime",
                table: "Waypoints",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "positionTime",
                table: "Waypoints",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");
        }
    }
}
