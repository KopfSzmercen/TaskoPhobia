using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskoPhobia.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountUpgradeProducts",
                schema: "taskophobia",
                table: "AccountUpgradeProducts");

            migrationBuilder.RenameTable(
                name: "AccountUpgradeProducts",
                schema: "taskophobia",
                newName: "Products",
                newSchema: "taskophobia");

            migrationBuilder.RenameIndex(
                name: "IX_AccountUpgradeProducts_UpgradeTypeValue",
                schema: "taskophobia",
                table: "Products",
                newName: "IX_Products_UpgradeTypeValue");

            migrationBuilder.AlterColumn<string>(
                name: "UpgradeTypeValue",
                schema: "taskophobia",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "taskophobia",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "taskophobia",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "taskophobia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "taskophobia",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "taskophobia",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "taskophobia",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                schema: "taskophobia",
                table: "Orders",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders",
                schema: "taskophobia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "taskophobia",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "taskophobia",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "taskophobia",
                newName: "AccountUpgradeProducts",
                newSchema: "taskophobia");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UpgradeTypeValue",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                newName: "IX_AccountUpgradeProducts_UpgradeTypeValue");

            migrationBuilder.AlterColumn<string>(
                name: "UpgradeTypeValue",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountUpgradeProducts",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                column: "Id");
        }
    }
}
