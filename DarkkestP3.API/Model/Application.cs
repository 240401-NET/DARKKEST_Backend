

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Darkkest.API.Model;

public class Application {
    [Key]
    int appId;
    // [ForeignKey]
    int userId;
    // [ForeignKey]
    int oppId;
    string appStatus;
    string history;
    string notifications;
}