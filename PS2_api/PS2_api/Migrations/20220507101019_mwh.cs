using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS2_api.Migrations
{
    public partial class mwh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wh",
                table: "Powers",
                newName: "mWh");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "mWh",
                table: "Powers",
                newName: "Wh");
        }
    }
}
