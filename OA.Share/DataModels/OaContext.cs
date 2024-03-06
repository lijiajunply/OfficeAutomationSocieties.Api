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

    public OaContext(DbContextOptions<OaContext> options)
        : base(options)
    {
        Users = Set<UserModel>();
        Files = Set<FileModel>();
        Projects = Set<ProjectModel>();
        Resources = Set<ResourceModel>();
        Announcements = Set<AnnouncementModel>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().HasMany(x => x.Projects).WithMany(x => x.Members);
        modelBuilder.Entity<ProjectModel>().HasMany(x => x.Files).WithOne(x => x.Owner).IsRequired(false);
    }
}
#pragma warning restore CS1591

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