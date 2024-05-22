using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarkkestP3.API.Model;

public class Profile 
{
    [Key]
    public int ProfileId { get; set; }
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; } = "";
    public string Interersts { get; set; } = "";
    public string Skills { get; set; } = "";
    public string MissionStatement { get; set; } = "";    
}