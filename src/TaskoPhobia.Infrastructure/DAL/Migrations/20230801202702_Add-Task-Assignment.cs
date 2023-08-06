using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskoPhobia.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskAssignment : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_OwnerId",
                schema: "taskophobia",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                schema: "taskophobia",
                table: "ProjectTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "taskophobia",
                table: "ProjectTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignmentsLimit",
                schema: "taskophobia",
                table: "ProjectTasks",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                schema: "taskophobia",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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

            migrationBuilder.CreateTable(
                name: "TaskAssignments",
                schema: "taskophobia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_ProjectTasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "taskophobia",
                        principalTable: "ProjectTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Users_AssigneeId",
                        column: x => x.AssigneeId,
                        principalSchema: "taskophobia",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_AssigneeId",
                schema: "taskophobia",
                table: "TaskAssignments",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_TaskId",
                schema: "taskophobia",
                table: "TaskAssignments",
                column: "TaskId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_OwnerId",
                schema: "taskophobia",
                table: "Projects",
                column: "OwnerId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                schema: "taskophobia",
                table: "ProjectTasks",
                column: "ProjectId",
                principalSchema: "taskophobia",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_OwnerId",
                schema: "taskophobia",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                schema: "taskophobia",
                table: "ProjectTasks");

            migrationBuilder.DropTable(
                name: "TaskAssignments",
                schema: "taskophobia");

            migrationBuilder.DropColumn(
                name: "AssignmentsLimit",
                schema: "taskophobia",
                table: "ProjectTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                schema: "taskophobia",
                table: "ProjectTasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                schema: "taskophobia",
                table: "Projects",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_OwnerId",
                schema: "taskophobia",
                table: "Projects",
                column: "OwnerId",
                principalSchema: "taskophobia",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                schema: "taskophobia",
                table: "ProjectTasks",
                column: "ProjectId",
                principalSchema: "taskophobia",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
