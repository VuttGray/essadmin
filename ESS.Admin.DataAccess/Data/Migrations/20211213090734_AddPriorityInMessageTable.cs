using Microsoft.EntityFrameworkCore.Migrations;

namespace ESS.Admin.DataAccess.Data.Migrations
{
    public partial class AddPriorityInMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PRIORITY",
                table: "MESSAGES",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PRIORITY",
                table: "MESSAGES");
        }
    }
}
