

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Darkkest.API.Model;

public class Opportunity {
    [Key]
    int oppId;
    [ForeignKey("Organization")]
    int orgId;
    string jobTitle;
    string description;
}