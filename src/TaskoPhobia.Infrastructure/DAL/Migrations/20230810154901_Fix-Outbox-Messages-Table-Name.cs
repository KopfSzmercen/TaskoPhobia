using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskoPhobia.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixOutboxMessagesTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutboxMessage",
                schema: "taskophobia",
                table: "OutboxMessage");

            migrationBuilder.RenameTable(
                name: "OutboxMessage",
                schema: "taskophobia",
                newName: "OutboxMessages",
                newSchema: "taskophobia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutboxMessages",
                schema: "taskophobia",
                table: "OutboxMessages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OutboxMessages",
                schema: "taskophobia",
                table: "OutboxMessages");

            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                schema: "taskophobia",
                newName: "OutboxMessage",
                newSchema: "taskophobia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutboxMessage",
                schema: "taskophobia",
                table: "OutboxMessage",
                column: "Id");
        }
    }
}
