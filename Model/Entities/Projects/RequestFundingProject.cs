using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Projects;

[Table("REQUEST_FUNDING_PROJECTS")]
public class RequestFundingProject : AProject{

    [Column("IS_FWF_FUNDED")]
    public bool IsFwfFunded { get; set; }
    
    [Column("IS_FFG_FUNDED")]
    public bool IsFfgFunded { get; set; }
}