using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Shared.Entities.Facilities;
using Shared.Entities.Projects.Managements;
using Shared.Entities.Projects.RequestProjects;
using Shared.Enums;
using Shared.Serializers;

namespace Shared.Entities.Projects.AProjects;

[JsonDerivedType(typeof(CreateManagementProjectDto))]
[JsonDerivedType(typeof(CreateRequestFundingProjectDto))]
[JsonConverter(typeof(CreateProjectSerializer))]
public class CreateAProjectDto {
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public EProjectState State { get; set; }
    public CreateFacilityDto Facility { get; set; } = null!;
}