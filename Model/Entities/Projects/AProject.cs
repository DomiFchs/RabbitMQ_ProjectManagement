using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace Model.Entities.Projects;

[Table("PROJECTS_BT")]
public abstract class AProject {
    
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PROJECT_ID")]
    public int Id { get; set; }
    
    [Column("PROJECT_TITLE")]
    [StringLength(200)]
    public string Title { get; set; } = null!;
    
    [Column("PROJECT_DESCRIPTION")]
    [StringLength(2000)]
    public string Description { get; set; } = null!;

    [Column("PROJECT_STATE")]
    public EProjectState State { get; set; }
    
    [Column("FACILITY_ID")]
    public int FacilityId { get; set; }
    public Facility Facility { get; set; } = null!;
}