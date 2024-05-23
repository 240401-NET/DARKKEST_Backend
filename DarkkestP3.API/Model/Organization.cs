using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarkkestP3.API.Model;

public class Organization{
    [Key]
    public int OrgId { get; set; }
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; } = "";
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
}