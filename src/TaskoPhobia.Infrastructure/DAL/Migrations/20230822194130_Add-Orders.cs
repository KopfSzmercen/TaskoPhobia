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

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                schema: "taskophobia",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                schema: "taskophobia",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    Amount = table.Column<int>(type: "integer", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "taskophobia",
                        principalTable: "Users",
                        principalColumn: "Id");
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

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountUpgradeProducts",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                column: "Id");
        }
    }
}
