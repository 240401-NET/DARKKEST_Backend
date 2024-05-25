using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
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

    [HttpGet("/opportunity")]
    public IActionResult GetAllOpps()
    {
        var opps = _oppService.GetAllOpps();
        return Ok(opps);
    }

    [HttpGet("/opportunity/user"), Authorize]
    public IActionResult GetUserOpps()
    {
        return Ok("You have gotten into this endpoint");
        
        if(HttpContext is null) return BadRequest("HttpContext is null here!");
        else return Ok(HttpContext);

        var userId = GetUserId();
        if(userId is null) return BadRequest();

        var opps = _oppService.GetUserOpps(userId);
        return Ok(opps);
    }

    [HttpGet("/opportunity/{id}")]
    public IActionResult GetOppById(int id)
    {
        if(HttpContext is null) return BadRequest("HttpContext is null here!");
        
        var opps = _oppService.GetOppById(id);

        if(opps is null) return NotFound();
        return Ok(opps);
    }

    [HttpPost("/opportunity")]
    public IActionResult CreateOpp([FromBody] NewOpp newOpp)
    {
        var userId = GetUserId();
        if(userId is null) return BadRequest();

        var opp = _oppService.CreateOpp(newOpp, userId);
        return Ok(opp);
    }    

    [HttpPut("/opportunity")]
    public IActionResult UpdateOpp([FromBody] UpdateOpp updateOpp)
    {
        var userId = GetUserId();
        if(userId is null) return BadRequest();
        
        var opp = _oppService.UpdateOpp(updateOpp, userId);
        if(opp is null) return BadRequest("This opportunity does not belong to you!");
        return Ok(opp);
    }

    [HttpDelete("opportunity/{id}")]
    public IActionResult DeleteOpp(int id)
    {
        var userId = GetUserId();
        if(userId is null) return BadRequest();

        var opp = _oppService.GetOppById(id);
        if(opp is null || opp.AppUserId != userId) return BadRequest("Opportunity does not exist or it does not belong to you!");

        return Ok(_oppService.DeleteOpp(id));
    }

    private string GetUserId()
    {
        return _userService.GetUserIdByName(HttpContext.User.Identity!.Name!);
    }
}