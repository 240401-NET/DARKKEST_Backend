using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Darkkest.API.Model;

public class Profile 
{
    [Key]
    public int ProfileId { get; set; }
    [ForeignKey("ApplicationUser")]
    public int UserId { get; set; }
    public string Interersts { get; set; } = "";
    public string Skills { get; set; } = "";
    public string MissionStatement { get; set; } = "";    
}