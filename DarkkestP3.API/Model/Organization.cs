using System.ComponentModel.DataAnnotations;

namespace Darkkest.API.Model;

public class Organization{
    [Key]
    int orgId;
    string name;
    string address;
    
}