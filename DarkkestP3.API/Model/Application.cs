

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Darkkest.API.Model;

public class Application {
    [Key]
    public int AppId { get; set; }
    [ForeignKey("ApplicationUser")]
    public int UserId { get; set; }
    [ForeignKey("Opportunity")]
    public int OppId { get; set; }
    public string AppStatus { get; set; }
    public string History { get; set;}
    public string Notifications { get; set; }
}