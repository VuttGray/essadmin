using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESS.Admin.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ess");

            migrationBuilder.CreateTable(
                name: "MESSAGES",
                schema: "ess",
                columns: table => new
                {
                    RECORD_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SUBJECT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BODY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RECIPIENTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CC_RECIPIENTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BCC_RECIPIENTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ATTACHMENTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRIORITY = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ATTEMPTS = table.Column<int>(type: "int", nullable: false),
                    SENT_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RECORD_STATUS = table.Column<int>(type: "int", nullable: false),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGES", x => x.RECORD_ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                schema: "ess",
                columns: table => new
                {
                    RECORD_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LAST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_LOCKED = table.Column<bool>(type: "bit", nullable: false),
                    RECORD_STATUS = table.Column<int>(type: "int", nullable: false),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.RECORD_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MESSAGES",
                schema: "ess");

            migrationBuilder.DropTable(
                name: "USERS",
                schema: "ess");
        }
    }
}
