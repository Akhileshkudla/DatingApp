using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class UpdateNamesOfSome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LookingFOr",
                table: "Users",
                newName: "LookingFor");

            migrationBuilder.RenameColumn(
                name: "Intrests",
                table: "Users",
                newName: "Interests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LookingFor",
                table: "Users",
                newName: "LookingFOr");

            migrationBuilder.RenameColumn(
                name: "Interests",
                table: "Users",
                newName: "Intrests");
        }
    }
}
