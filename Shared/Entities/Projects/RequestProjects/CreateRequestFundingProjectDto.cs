using Shared.Entities.Projects.AProjects;

namespace Shared.Entities.Projects.RequestProjects;

public class CreateRequestFundingProjectDto : CreateAProjectDto{
    public bool IsFwfFunded { get; set; }
    public bool IsFfgFunded { get; set; }
}