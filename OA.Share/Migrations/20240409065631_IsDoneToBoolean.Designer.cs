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
    [Migration("20240409065631_IsDoneToBoolean")]
    partial class IsDoneToBoolean
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("OA.Share.DataModels.AnnouncementModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("OA.Share.DataModels.FileModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(64)");

                    b.Property<bool>("IsFolder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OwnerId")
                        .HasColumnType("varchar(64)");

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
                        .HasColumnType("varchar(64)");

                    b.Property<string>("EndTime")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("ToDo")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("GanttList");
                });

            modelBuilder.Entity("OA.Share.DataModels.OrganizeIdentity", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("OrganizeId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.HasKey("Key");

                    b.HasIndex("OrganizeId");

                    b.HasIndex("UserId");

                    b.ToTable("OrganizeIdentities");
                });

            modelBuilder.Entity("OA.Share.DataModels.OrganizeModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Introduce")
                        .IsRequired()
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Organizes");
                });

            modelBuilder.Entity("OA.Share.DataModels.ProjectIdentity", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.HasKey("Key");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectIdentities");
                });

            modelBuilder.Entity("OA.Share.DataModels.ProjectModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Introduce")
                        .IsRequired()
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("OrganizeModelId")
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizeModelId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("OA.Share.DataModels.ResourceModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("CreateTime")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("EndTime")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Introduce")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("StartTime")
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("OA.Share.DataModels.UserModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(16)");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("varchar(13)");

                    b.Property<string>("RegistrationTime")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
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

            modelBuilder.Entity("OA.Share.DataModels.OrganizeIdentity", b =>
                {
                    b.HasOne("OA.Share.DataModels.OrganizeModel", "Organize")
                        .WithMany("MemberIdentity")
                        .HasForeignKey("OrganizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OA.Share.DataModels.UserModel", "User")
                        .WithMany("Organizes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organize");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OA.Share.DataModels.ProjectIdentity", b =>
                {
                    b.HasOne("OA.Share.DataModels.ProjectModel", "Project")
                        .WithMany("Members")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OA.Share.DataModels.UserModel", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
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

                    b.Navigation("Members");
                });

            modelBuilder.Entity("OA.Share.DataModels.UserModel", b =>
                {
                    b.Navigation("Organizes");

                    b.Navigation("Projects");

                    b.Navigation("TaskNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
