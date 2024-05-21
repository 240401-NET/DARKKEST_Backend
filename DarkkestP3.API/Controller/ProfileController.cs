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

    [HttpPut("/update")]
    public async Task<IActionResult> UpdateUserProfile(UpdateProfile updateProfile)
    {
        var result =  await _profileService.UpdateUserProfile(updateProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

    [HttpPatch("/updateinterests")]
    public async Task<IActionResult> UpdateUserProfileInterests([FromBody]PatchProfileInterests patchProfile)
    {
        var result = await _profileService.UpdateUserProfileInterests(patchProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

       [HttpPatch("/updateskills")]
    public async Task<IActionResult> UpdateUserProfileSkills([FromBody]PatchProfileSkills patchProfile)
    {
        var result = await _profileService.UpdateUserProfileSkills(patchProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

       [HttpPatch("/updatemissionstatement")]
    public async Task<IActionResult> UpdateUserProfileMissionStatement([FromBody]PatchProfileMissionStatement patchProfile)
    {
        var result = await _profileService.UpdateUserProfileMissionStatement(patchProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
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