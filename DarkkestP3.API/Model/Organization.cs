using System.ComponentModel.DataAnnotations;

namespace DarkkestP3.API.Model;

public class Organization{
    [Key]
    int orgId;
    string name;
    string address;
    
}