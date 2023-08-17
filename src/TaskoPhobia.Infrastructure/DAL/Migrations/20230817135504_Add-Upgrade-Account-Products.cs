using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskoPhobia.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUpgradeAccountProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountUpgradeProducts",
                schema: "taskophobia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeValue = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUpgradeProducts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountUpgradeProducts_TypeValue",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                column: "TypeValue",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountUpgradeProducts",
                schema: "taskophobia");
        }
    }
}
