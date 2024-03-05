using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OA.Share.DataModels;

public sealed class OaContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<FileModel> Files { get; set; }
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<ResourceModel> Resources { get; set; }
    public DbSet<AnnouncementModel> Announcements { get; set; }

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

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OaContext>
{
    public OaContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OaContext>();
        optionsBuilder.UseSqlite("Data Source=Data.db");
        return new OaContext(optionsBuilder.Options);
    }
}