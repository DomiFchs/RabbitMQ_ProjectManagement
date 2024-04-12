using System.Text.Json.Serialization;
using Shared.Entities.Facilities;
using Shared.Entities.Projects.Managements;
using Shared.Entities.Projects.RequestProjects;
using Shared.Enums;
using Shared.Serializers;

namespace Shared.Entities.Projects.AProjects;

[JsonDerivedType(typeof(ManagementProjectCreated))]
[JsonDerivedType(typeof(RequestFundingProjectCreated))]
[JsonConverter(typeof(ProjectCreatedSerializer))]
public class AProjectCreated {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public EProjectState State { get; set; }
    public DefaultFacilityDto Facility { get; set; } = null!;
}