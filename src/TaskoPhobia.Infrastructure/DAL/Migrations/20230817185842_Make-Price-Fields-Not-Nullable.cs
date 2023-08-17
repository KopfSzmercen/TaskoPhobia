using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskoPhobia.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MakePriceFieldsNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeValue",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                newName: "UpgradeTypeValue");

            migrationBuilder.RenameIndex(
                name: "IX_AccountUpgradeProducts_TypeValue",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                newName: "IX_AccountUpgradeProducts_UpgradeTypeValue");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpgradeTypeValue",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                newName: "TypeValue");

            migrationBuilder.RenameIndex(
                name: "IX_AccountUpgradeProducts_UpgradeTypeValue",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                newName: "IX_AccountUpgradeProducts_TypeValue");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                schema: "taskophobia",
                table: "AccountUpgradeProducts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
