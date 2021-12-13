using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESS.Admin.DataAccess.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MESSAGES",
                columns: table => new
                {
                    RECORD_ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SUBJECT = table.Column<string>(type: "TEXT", nullable: true),
                    BODY = table.Column<string>(type: "TEXT", nullable: true),
                    RECIPIENTS = table.Column<string>(type: "TEXT", nullable: true),
                    CC_RECIPIENTS = table.Column<string>(type: "TEXT", nullable: true),
                    BCC_RECIPIENTS = table.Column<string>(type: "TEXT", nullable: true),
                    ATTACHMENTS = table.Column<string>(type: "TEXT", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ATTEMPTS = table.Column<int>(type: "INTEGER", nullable: false),
                    SENT_DATE = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RECORD_STATUS = table.Column<int>(type: "INTEGER", nullable: false),
                    IS_DELETED = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGES", x => x.RECORD_ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    RECORD_ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "TEXT", nullable: true),
                    LAST_NAME = table.Column<string>(type: "TEXT", nullable: true),
                    EMAIL = table.Column<string>(type: "TEXT", nullable: true),
                    IS_LOCKED = table.Column<bool>(type: "INTEGER", nullable: false),
                    RECORD_STATUS = table.Column<int>(type: "INTEGER", nullable: false),
                    IS_DELETED = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.RECORD_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MESSAGES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
