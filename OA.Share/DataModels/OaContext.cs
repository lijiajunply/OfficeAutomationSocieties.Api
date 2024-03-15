﻿using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OA.Share.DataModels;

#pragma warning disable CS1591
public sealed class OaContext : DbContext
{
    public DbSet<UserModel> Users { get; init; }
    public DbSet<FileModel> Files { get; init; }
    public DbSet<ProjectModel> Projects { get; init; }
    public DbSet<ResourceModel> Resources { get; init; }
    public DbSet<AnnouncementModel> Announcements { get; init; }
    public DbSet<OrganizeModel> Organizes { get; init; }

    public OaContext(DbContextOptions<OaContext> options)
        : base(options)
    {
        Users = Set<UserModel>();
        Files = Set<FileModel>();
        Projects = Set<ProjectModel>();
        Resources = Set<ResourceModel>();
        Announcements = Set<AnnouncementModel>();
        Organizes = Set<OrganizeModel>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().HasMany(x => x.Projects)
            .WithMany(x => x.Members);
        modelBuilder.Entity<ProjectModel>().HasMany(x => x.Files)
            .WithOne(x => x.Owner).IsRequired(false);
        modelBuilder.Entity<OrganizeModel>().HasMany(x => x.Member)
            .WithMany(x => x.Organizes);
        modelBuilder.Entity<OrganizeModel>().HasMany(x => x.Resources)
            .WithOne(x => x.Owner).IsRequired();
        modelBuilder.Entity<OrganizeModel>().HasMany(x => x.Announcements)
            .WithOne(x => x.Owner).IsRequired();
        modelBuilder.Entity<ProjectModel>().HasMany(x => x.GanttList)
            .WithOne(x => x.Project).IsRequired();
        modelBuilder.Entity<UserModel>().HasMany(x => x.TaskNotes)
            .WithOne(x => x.User).IsRequired();
        modelBuilder.Entity<OrganizeModel>().HasMany(x => x.MemberIdentity)
            .WithOne(x => x.Owner).IsRequired();
    }
}
#pragma warning restore CS1591

public static class ContextStatic
{
    public static string HashEncryption(this string str)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(str + DateTime.Now.ToString("s"))))
            .Replace("/", "-");

    public static string Base64Encryption(this string str) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
}

// ReSharper disable once UnusedType.Global
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OaContext>
{
    public OaContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OaContext>();
        optionsBuilder.UseSqlite("Data Source=Data.db");
        return new OaContext(optionsBuilder.Options);
    }
}