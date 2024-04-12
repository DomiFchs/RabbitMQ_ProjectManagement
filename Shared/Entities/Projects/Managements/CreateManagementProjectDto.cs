using Shared.Entities.Projects.AProjects;
using Shared.Enums;

namespace Shared.Entities.Projects.Managements;

public class CreateManagementProjectDto : CreateAProjectDto{
    public ELawType LawType { get; set; }
}