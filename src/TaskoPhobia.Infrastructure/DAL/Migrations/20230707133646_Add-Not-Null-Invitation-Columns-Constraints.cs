using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskoPhobia.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNotNullInvitationColumnsConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Projects_ProjectId",
                schema: "taskophobia",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_ReceiverId",
                schema: "taskophobia",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_SenderId",
                schema: "taskophobia",
                table: "Invitations");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "taskophobia",
                table: "Invitations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "taskophobia",
                table: "Invitations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                schema: "taskophobia",
                table: "Invitations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiverId",
                schema: "taskophobia",
                table: "Invitations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "taskophobia",
                table: "Invitations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Projects_ProjectId",
                schema: "taskophobia",
                table: "Invitations",
                column: "ProjectId",
                principalSchema: "taskophobia",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_ReceiverId",
                schema: "taskophobia",
                table: "Invitations",
                column: "ReceiverId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_SenderId",
                schema: "taskophobia",
                table: "Invitations",
                column: "SenderId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Projects_ProjectId",
                schema: "taskophobia",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_ReceiverId",
                schema: "taskophobia",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_SenderId",
                schema: "taskophobia",
                table: "Invitations");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "taskophobia",
                table: "Invitations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "taskophobia",
                table: "Invitations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                schema: "taskophobia",
                table: "Invitations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiverId",
                schema: "taskophobia",
                table: "Invitations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "taskophobia",
                table: "Invitations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Projects_ProjectId",
                schema: "taskophobia",
                table: "Invitations",
                column: "ProjectId",
                principalSchema: "taskophobia",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_ReceiverId",
                schema: "taskophobia",
                table: "Invitations",
                column: "ReceiverId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_SenderId",
                schema: "taskophobia",
                table: "Invitations",
                column: "SenderId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
