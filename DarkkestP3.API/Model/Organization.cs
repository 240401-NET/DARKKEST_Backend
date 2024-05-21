using System.ComponentModel.DataAnnotations;

namespace DarkkestP3.API.Model;

public class Organization{
    [Key]
    public int OrgId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}