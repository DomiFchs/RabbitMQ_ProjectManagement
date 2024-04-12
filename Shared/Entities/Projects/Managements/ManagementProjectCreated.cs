using Shared.Entities.Projects.AProjects;
using Shared.Enums;

namespace Shared.Entities.Projects.Managements;

public class ManagementProjectCreated : AProjectCreated{
    public ELawType LawType { get; set; }
}