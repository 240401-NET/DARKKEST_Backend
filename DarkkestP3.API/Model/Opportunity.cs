

using System.ComponentModel.DataAnnotations;

namespace Darkkest.API.Model;

public class Opportunity {
    [Key]
    int oppId;
    [Key]
    int orgId;
    string jobTitle;
    string description;
}