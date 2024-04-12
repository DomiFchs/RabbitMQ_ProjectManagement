using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("FACILITIES")]
public class Facility {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("FACILITY_ID")]
    public int Id { get; set; }
    
    [Column("FACILITY_NAME")]
    [StringLength(200)]
    public string Name { get; set; } = null!;
}