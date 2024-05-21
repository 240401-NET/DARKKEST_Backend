

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Darkkest.API.Model;

public class Profile {
    [Key]
    int profileId;
    [ForeignKey("ApplicationUser")]
    int userId;
    string interersts;
    string skills;
    
}