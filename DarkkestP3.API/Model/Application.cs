

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Darkkest.API.Model;

public class Application {
    [Key]
    int appId;
    [ForeignKey("ApplicationUser")]
    int userId;
    [ForeignKey("Opportunity")]
    int oppId;
    string appStatus;
    string history;
    string notifications;
}