﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskoPhobia.Infrastructure.DAL.Contexts;

#nullable disable

namespace TaskoPhobia.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(TaskoPhobiaWriteDbContext))]
    partial class TaskoPhobiaWriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("taskophobia")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskoPhobia.Core.Entities.Invitations.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("BlockSendingMoreInvitations")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Invitations", "taskophobia");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.ProjectParticipation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("JoinDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectParticipations", "taskophobia");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.ProjectTasks.ProjectSummary", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.ToTable("Projects", "taskophobia");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.ProjectTasks.ProjectTask", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("AssignmentsLimit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTasks", "taskophobia");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.ProjectTasks.TaskAssignment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssigneeId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskAssignments", "taskophobia");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.Projects.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Projects", "taskophobia");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users", "taskophobia");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.Invitations.Invitation", b =>
                {
                    b.HasOne("TaskoPhobia.Core.Entities.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskoPhobia.Core.Entities.Users.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskoPhobia.Core.Entities.Users.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.ProjectParticipation", b =>
                {
                    b.HasOne("TaskoPhobia.Core.Entities.Users.User", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskoPhobia.Core.Entities.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.ProjectTasks.ProjectSummary", b =>
                {
                    b.HasOne("TaskoPhobia.Core.Entities.Projects.Project", null)
                        .WithOne("ProjectSummary")
                        .HasForeignKey("TaskoPhobia.Core.Entities.ProjectTasks.ProjectSummary", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.ProjectTasks.ProjectTask", b =>
                {
                    b.HasOne("TaskoPhobia.Core.Entities.ProjectTasks.ProjectSummary", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("TaskoPhobia.Core.ValueObjects.TaskTimeSpan", "TimeSpan", b1 =>
                        {
                            b1.Property<Guid>("ProjectTaskId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("EndDate");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("StartDate");

                            b1.HasKey("ProjectTaskId");

                            b1.ToTable("ProjectTasks", "taskophobia");

                            b1.WithOwner()
                                .HasForeignKey("ProjectTaskId");
                        });

                    b.Navigation("Project");

                    b.Navigation("TimeSpan");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.ProjectTasks.TaskAssignment", b =>
                {
                    b.HasOne("TaskoPhobia.Core.Entities.Users.User", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskoPhobia.Core.Entities.ProjectTasks.ProjectTask", null)
                        .WithMany("Assignments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignee");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.Projects.Project", b =>
                {
                    b.HasOne("TaskoPhobia.Core.Entities.Users.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.ProjectTasks.ProjectTask", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("TaskoPhobia.Core.Entities.Projects.Project", b =>
                {
                    b.Navigation("ProjectSummary")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
