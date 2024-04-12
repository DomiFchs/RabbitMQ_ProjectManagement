using Shared.Entities.Projects.AProjects;

namespace Shared.Entities.Projects.RequestProjects;

public class RequestFundingProjectCreated : AProjectCreated {
    public bool IsFwfFunded { get; set; }
    public bool IsFfgFunded { get; set; }
}