﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OA.Share.DataModels;

#nullable disable

namespace OA.Share.Migrations
{
    [DbContext(typeof(OaContext))]
    [Migration("20240315085404_AddAnnContext")]
    partial class AddAnnContext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("OA.Share.DataModels.AnnouncementModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("OA.Share.DataModels.FileModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsFolder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OwnerId")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("OA.Share.DataModels.GanttModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("EndTime")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ToDo")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("GanttModel");
                });

            modelBuilder.Entity("OA.Share.DataModels.IdentityModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("UserId");

                    b.HasIndex("OwnerId");

                    b.ToTable("IdentityModel");
                });

            modelBuilder.Entity("OA.Share.DataModels.OrganizeModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Introduce")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Organizes");
                });

            modelBuilder.Entity("OA.Share.DataModels.ProjectModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("OrganizeModelId")
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizeModelId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("OA.Share.DataModels.ResourceModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("EndTime")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("OA.Share.DataModels.UserModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OrganizeModelUserModel", b =>
                {
                    b.Property<string>("MemberUserId")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("OrganizesId")
                        .HasColumnType("varchar(256)");

                    b.HasKey("MemberUserId", "OrganizesId");

                    b.HasIndex("OrganizesId");

                    b.ToTable("OrganizeModelUserModel");
                });

            modelBuilder.Entity("ProjectModelUserModel", b =>
                {
                    b.Property<string>("MembersUserId")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ProjectsId")
                        .HasColumnType("varchar(256)");

                    b.HasKey("MembersUserId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("ProjectModelUserModel");
                });

            modelBuilder.Entity("OA.Share.DataModels.AnnouncementModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.OrganizeModel", "Owner")
                        .WithMany("Announcements")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("OA.Share.DataModels.FileModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.ProjectModel", "Owner")
                        .WithMany("Files")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("OA.Share.DataModels.GanttModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.ProjectModel", "Project")
                        .WithMany("GanttList")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OA.Share.DataModels.UserModel", "User")
                        .WithMany("TaskNotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OA.Share.DataModels.IdentityModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.OrganizeModel", "Owner")
                        .WithMany("MemberIdentity")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("OA.Share.DataModels.ProjectModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.OrganizeModel", null)
                        .WithMany("Projects")
                        .HasForeignKey("OrganizeModelId");
                });

            modelBuilder.Entity("OA.Share.DataModels.ResourceModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.OrganizeModel", "Owner")
                        .WithMany("Resources")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("OrganizeModelUserModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.UserModel", null)
                        .WithMany()
                        .HasForeignKey("MemberUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OA.Share.DataModels.OrganizeModel", null)
                        .WithMany()
                        .HasForeignKey("OrganizesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectModelUserModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.UserModel", null)
                        .WithMany()
                        .HasForeignKey("MembersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OA.Share.DataModels.ProjectModel", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OA.Share.DataModels.OrganizeModel", b =>
                {
                    b.Navigation("Announcements");

                    b.Navigation("MemberIdentity");

                    b.Navigation("Projects");

                    b.Navigation("Resources");
                });

            modelBuilder.Entity("OA.Share.DataModels.ProjectModel", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("GanttList");
                });

            modelBuilder.Entity("OA.Share.DataModels.UserModel", b =>
                {
                    b.Navigation("TaskNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
