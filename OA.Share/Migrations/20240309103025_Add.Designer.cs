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
    [Migration("20240309103025_Add")]
    partial class Add
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
                        .HasColumnType("TEXT");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("OA.Share.DataModels.ProjectModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Gantt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

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

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("OA.Share.DataModels.UserModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Identity")
                        .IsRequired()
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
                    b.HasOne("OA.Share.DataModels.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OA.Share.DataModels.FileModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.ProjectModel", "Owner")
                        .WithMany("Files")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("OA.Share.DataModels.ResourceModel", b =>
                {
                    b.HasOne("OA.Share.DataModels.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("OA.Share.DataModels.ProjectModel", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
