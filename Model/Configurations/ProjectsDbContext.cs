using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Entities.Projects;

namespace Model.Configurations;

public class ProjectsDbContext(DbContextOptions<ProjectsDbContext> options) : DbContext(options) {

    public DbSet<AProject> Projects { get; set; } = null!;
    public DbSet<Facility> Facilities { get; set; } = null!;
    public DbSet<ManagementProject> ManagementProjects { get; set; } = null!;
    public DbSet<RequestFundingProject> RequestFundingProjects { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder) {

        builder.Entity<AProject>().HasOne(p => p.Facility).WithMany().HasForeignKey(p => p.FacilityId);

    }
}