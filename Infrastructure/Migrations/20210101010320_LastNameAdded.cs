using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class LastNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "AspNetUsers",
                newName: "DisplayLastName");

            migrationBuilder.AddColumn<string>(
                name: "DisplayFirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayFirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DisplayLastName",
                table: "AspNetUsers",
                newName: "DisplayName");
        }
    }
}
