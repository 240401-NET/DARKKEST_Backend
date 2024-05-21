using DarkkestP3.API.DTO;
using DarkkestP3.API.Migrations.CommunityDB;
using DarkkestP3.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarkkestP3.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class OpportunityController : ControllerBase
{
    private readonly IOpportunityService _oppService;
    private readonly IUserService _userService;

    public OpportunityController(IOpportunityService OppService, IUserService userService)
    {
        _oppService = OppService;
        _userService = userService;
    }

    [HttpGet("/opportunity"), Authorize]
    public IActionResult GetAllOpps()
    {
        var opps = _oppService.GetAllOpps();
        return Ok(opps);
    }

    [HttpGet("/opportunity/{id}"), Authorize]
    public IActionResult GetOppById(int id)
    {
        var opps = _oppService.GetOppById(id);

        if(opps is null) return NotFound();
        return Ok(opps);
    }

    [HttpPost("/opportunity"), Authorize]
    public IActionResult CreateOpp([FromBody] NewOpp newOpp)
    {
        var userId = _userService.GetUserIdByName(HttpContext.User.Identity!.Name!);
        if(userId is null) return BadRequest();

        var opp = _oppService.CreateOpp(newOpp, userId);
        if(opp is null) return BadRequest();
        return Ok(opp);
    }    

    [HttpPut("/opportunity/{id}"), Authorize]
    public IActionResult UpdateOpp([FromBody] Opportunity updateOpp)
    {
        
        
        throw new NotImplementedException();
    }

    [HttpDelete]
    public IActionResult DeleteOpp()
    {
        throw new NotImplementedException();
    }
}