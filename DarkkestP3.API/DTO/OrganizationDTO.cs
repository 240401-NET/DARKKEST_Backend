using DarkkestP3.API.Model;

namespace DarkkestP3.API.DTO
{
    public class OrganizationDTO
    {
        public int OrgId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class RegisterOrganization
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class UpdateOrganization
    {
        public int OrgId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class DeleteOrganization
    {
        public int OrgId { get; set; }
    }
}