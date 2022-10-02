using Microsoft.EntityFrameworkCore.Migrations;

namespace CentreAppBlazor.Server.Migrations
{
    public partial class Add_CustTypeDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "CustomerTypes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CustomerTypes");
        }
    }
}
