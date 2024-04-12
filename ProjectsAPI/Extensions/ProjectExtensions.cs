using Shared.Entities.Projects.AProjects;
using Shared.Entities.Projects.Managements;
using Shared.Entities.Projects.RequestProjects;

namespace ProjectManagement_MOM.Extensions;

public static class ProjectExtensions {
    
    
    public static AProjectCreated Map(this CreateAProjectDto project) {
        return project switch {
            CreateManagementProjectDto managementProject => managementProject.Map(),
            CreateRequestFundingProjectDto requestFundingProject => requestFundingProject.Map(),
            _ => throw new ArgumentException("Unknown project type.")
        };
    }
    
    public static ManagementProjectCreated Map(this CreateManagementProjectDto project) {
        return new ManagementProjectCreated {
            Title = project.Title,
            Description = project.Description,
            State = project.State,
            LawType = project.LawType
        };
    }
    
    public static RequestFundingProjectCreated Map(this CreateRequestFundingProjectDto project) {
        return new RequestFundingProjectCreated {
            Title = project.Title,
            Description = project.Description,
            State = project.State,
            IsFfgFunded = project.IsFfgFunded,
            IsFwfFunded = project.IsFwfFunded
        };
    }
    
}