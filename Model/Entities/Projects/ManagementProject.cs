using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace Model.Entities.Projects;

[Table("MANAGEMENT_PROJECTS")]
public class ManagementProject : AProject{
    
    [Column("LAW_TYPE")]
    public ELawType LawType { get; set; }
}