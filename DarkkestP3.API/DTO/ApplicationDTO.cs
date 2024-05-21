using DarkkestP3.API.Model;

namespace DarkkestP3.API.DTO
{
    public class ApplicationDTO
    {
        public int AppId { get; set; }
        public int UserId { get; set; }
        public int OppId { get; set; }
        public ApplicationStatus AppStatus { get; set; }
        public string History { get; set; } = "";
        public string Notifications { get; set; } = "";
    }
    public class CreateApplication
    {
        public int UserId { get; set; }
        public int OppId { get; set; }
        public ApplicationStatus AppStatus { get; set; }
        public string History { get; set; } = "";
        public string Notifications { get; set; } = "";
    }

    public class UpdateApplication
    {
        public int AppId { get; set; }
        public int UserId { get; set; }
        public int OppId { get; set; }
        public ApplicationStatus AppStatus { get; set; }
        public string History { get; set; } = "";
        public string Notifications { get; set; } = "";
    }
}