using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarkkestP3.API.Model;

public class Opportunity 
{
    [Key]
    public int OppId { get; set; }
    [ForeignKey("ApplicationUser")]
    public string AppUserId { get; set; } = "";
    public string JobTitle { get; set; } = "";
    public string Description { get; set; } = "";
}