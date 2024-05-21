using DarkkestP3.API.Model;
using DarkkestP3.API.DTO;
using DarkkestP3.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Darkkest.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {    
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpPost("/org/register")]
        public async Task<IActionResult> RegisterOrganization([FromBody] RegisterOrganization registration)
        {
            var result = await _organizationService.RegisterOrganization(registration);

            if(result.Succeeded)
            {
                return Ok(result);
            }        
            return BadRequest(result.Errors);
        }

        [HttpPut("/org/update")]
        public async Task<IActionResult> UpdateOrganization([FromBody] UpdateOrganization update)
        {
            var result = await _organizationService.UpdateOrganization(update);

            if(result.Succeeded)
            {
                return Ok(result);
            }        
            return BadRequest(result.Errors);
        }

        [HttpDelete("/org/delete")]
        public async Task<IActionResult> DeleteOrganization([FromBody] DeleteOrganization delete)
        {
            var result = await _organizationService.DeleteOrganization(delete);

            if(result.Succeeded)
            {
                return Ok(result);
            }        
            return BadRequest(result.Errors);
        }

        [HttpGet("/org/{orgId}")]
        public async Task<IActionResult> GetOrganization(int orgId)
        {
            var organization = await _organizationService.GetOrganization(orgId);
            return Ok(organization);
        }

        [HttpGet("/org/{Name}")]
        public async Task<IActionResult> GetOrganizationByName(string name)
        {
            var organization = await _organizationService.GetOrganizationByName(name);
            if (organization != null)
            {
                return Ok(organization);
            } 
            else{
                return NotFound();
            }
        }

        [HttpGet("/orgs")]
        public async Task<IActionResult> GetOrganizations()
        {
            var organizations = await _organizationService.GetOrganizations();
            return Ok(organizations);
        }
    }
}