

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Darkkest.API.Model;

public class Profile 
{
    [Key]
    int ProfileId { get; set; }
    [ForeignKey("ApplicationUser")]
    int UserId{ get; set; }
    string Interersts { get; set; } = "";
    string Skills { get; set; } = "";
    string MissionStatement { get; set; } = "";
    
}