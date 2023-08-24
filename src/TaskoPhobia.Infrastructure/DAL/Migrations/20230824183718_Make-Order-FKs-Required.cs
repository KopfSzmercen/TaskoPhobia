using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskoPhobia.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MakeOrderFKsRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                schema: "taskophobia",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerId",
                schema: "taskophobia",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                schema: "taskophobia",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                schema: "taskophobia",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                schema: "taskophobia",
                table: "Orders",
                column: "ProductId",
                principalSchema: "taskophobia",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CustomerId",
                schema: "taskophobia",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                schema: "taskophobia",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerId",
                schema: "taskophobia",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                schema: "taskophobia",
                table: "Orders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                schema: "taskophobia",
                table: "Orders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                schema: "taskophobia",
                table: "Orders",
                column: "ProductId",
                principalSchema: "taskophobia",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CustomerId",
                schema: "taskophobia",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
