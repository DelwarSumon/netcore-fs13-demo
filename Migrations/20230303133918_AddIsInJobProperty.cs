using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NETCoreDemo.Migrations
{
    public partial class AddIsInJobProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_in_job",
                table: "students",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_in_job",
                table: "students");
        }
    }
}
