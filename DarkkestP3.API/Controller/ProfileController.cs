using Darkkest.API.DTO;
using Darkkest.API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Darkkest.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase 
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost("/create")]
    public async Task<IActionResult> CreateUserProfile([FromBody] NewProfile newProfileDTO)
    {
        var result = await _profileService.CreateUserProfile(newProfileDTO);

        if(result!=null)
        {
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpGet("get/{userId}")]
    public async Task<IActionResult> GetUserProfileByUserId(int userId)
    {
        var result = await _profileService.GetUserProfileByUserId(userId);

        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

    [HttpPut("/update/{userId}")]
    public async Task<IActionResult> UpdateUserProfile(int userId)
    {
        var result =  await _profileService.UpdateUserProfile(userId);
        if(result.Succeeded)
        {
        return Ok(result);
        }
        return BadRequest(result.Errors);
    }

    [HttpDelete("/delete/{userId}")]
    public async Task<IActionResult> DeleteUserProfile(int userId)
    {
        var result = await _profileService.DeleteUserProfile(userId);    
        if(result.Succeeded)
        {
        return Ok(result);
        }
        return BadRequest(result.Errors);
    }
}