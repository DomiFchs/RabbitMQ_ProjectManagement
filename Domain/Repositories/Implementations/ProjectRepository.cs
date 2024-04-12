using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configurations;
using Model.Entities.Projects;

namespace Domain.Repositories.Implementations;

public class ProjectRepository(IDbContextFactory<ProjectsDbContext> contextFactory) : ARepository<AProject>(contextFactory),IProjectRepository  {
    
}