using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentreAppBlazor.Server.Migrations
{
    public partial class Added_Expenses2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegDateTime",
                table: "Expenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegDateTime",
                table: "Expenses");
        }
    }
}
