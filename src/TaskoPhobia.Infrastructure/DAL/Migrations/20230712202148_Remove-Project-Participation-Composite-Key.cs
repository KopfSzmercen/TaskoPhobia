using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskoPhobia.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProjectParticipationCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipations_Projects_ProjectId",
                schema: "taskophobia",
                table: "ProjectParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipations_Users_ParticipantId",
                schema: "taskophobia",
                table: "ProjectParticipations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectParticipations",
                schema: "taskophobia",
                table: "ProjectParticipations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "taskophobia",
                table: "ProjectParticipations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParticipantId",
                schema: "taskophobia",
                table: "ProjectParticipations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "taskophobia",
                table: "ProjectParticipations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectParticipations",
                schema: "taskophobia",
                table: "ProjectParticipations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectParticipations_ParticipantId",
                schema: "taskophobia",
                table: "ProjectParticipations",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipations_Projects_ProjectId",
                schema: "taskophobia",
                table: "ProjectParticipations",
                column: "ProjectId",
                principalSchema: "taskophobia",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipations_Users_ParticipantId",
                schema: "taskophobia",
                table: "ProjectParticipations",
                column: "ParticipantId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipations_Projects_ProjectId",
                schema: "taskophobia",
                table: "ProjectParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipations_Users_ParticipantId",
                schema: "taskophobia",
                table: "ProjectParticipations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectParticipations",
                schema: "taskophobia",
                table: "ProjectParticipations");

            migrationBuilder.DropIndex(
                name: "IX_ProjectParticipations_ParticipantId",
                schema: "taskophobia",
                table: "ProjectParticipations");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "taskophobia",
                table: "ProjectParticipations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "taskophobia",
                table: "ProjectParticipations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParticipantId",
                schema: "taskophobia",
                table: "ProjectParticipations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectParticipations",
                schema: "taskophobia",
                table: "ProjectParticipations",
                columns: new[] { "ParticipantId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipations_Projects_ProjectId",
                schema: "taskophobia",
                table: "ProjectParticipations",
                column: "ProjectId",
                principalSchema: "taskophobia",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipations_Users_ParticipantId",
                schema: "taskophobia",
                table: "ProjectParticipations",
                column: "ParticipantId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
