using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DarkkestP3.API.Model;
using DarkkestP3.API.DB;
using DarkkestP3.API.DTO;
using Microsoft.AspNetCore.Identity;

namespace DarkkestP3.API.Service
{
    public class OrganizationService : IOrganizationService
    {
        private readonly CommunityDBContext _context;

        public OrganizationService(CommunityDBContext context)
        {
            _context = context;
        }

        public Task<IdentityResult> RegisterOrganization(RegisterOrganization createOrganization)
        {
            var organization = new Organization
            {
                Name = createOrganization.Name,
                Address = createOrganization.Address
            };

            try
            {
                _context.Organizations.Add(organization);
                _context.SaveChanges();
                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception e)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = e.Message }));
            }
        }
        public Task<IdentityResult> UpdateOrganization(UpdateOrganization updateOrganization)
        {
            var organization = _context.Organizations.Find(updateOrganization.OrgId);

            if (organization == null)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Organization not found" }));
            }

            organization.Name = updateOrganization.Name;
            organization.Address = updateOrganization.Address;

            try
            {
                _context.Organizations.Update(organization);
                _context.SaveChanges();
                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception e)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = e.Message }));
            }
        }
        public Task<IdentityResult> DeleteOrganization(DeleteOrganization deleteOrganization)
        {
            var organization = _context.Organizations.Find(deleteOrganization.OrgId);

            if (organization == null)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Organization not found" }));
            }

            try
            {
                _context.Organizations.Remove(organization);
                _context.SaveChanges();
                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception e)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = e.Message }));
            }
        }
        public Task<OrganizationDTO> GetOrganization(int orgId)
        {
            var organization = _context.Organizations.Find(orgId);

            if (organization == null)
            {
                return Task.FromResult<OrganizationDTO>(null);
            }

            var organizationDTO = new OrganizationDTO
            {
                OrgId = organization.OrgId,
                Name = organization.Name,
                Address = organization.Address
            };

            return Task.FromResult(organizationDTO);
        }
        public Task<OrganizationDTO> GetOrganizationByName(string orgName)
        {
            var organization = _context.Organizations.FirstOrDefault(o => o.Name == orgName);

            if (organization == null)
            {
                return Task.FromResult<OrganizationDTO>(null);
            }

            var organizationDTO = new OrganizationDTO
            {
                OrgId = organization.OrgId,
                Name = organization.Name,
                Address = organization.Address
            };

            return Task.FromResult(organizationDTO);
        }
        public async Task<IEnumerable<OrganizationDTO>> GetOrganizations()
        {
            var organizations = _context.Organizations;
            var organizationDTOs = await organizations.Select(o => new OrganizationDTO
            {
                OrgId = o.OrgId,
                Name = o.Name,
                Address = o.Address
            }).ToListAsync();

            return organizationDTOs;
        }
    }
}