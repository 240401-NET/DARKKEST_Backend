using Microsoft.AspNetCore.Authorization;

using DarkkestP3.API.DTO;
using DarkkestP3.API.Service;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace DarkkestP3.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase 
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost("/profile/create"), Authorize]
    public async Task<IActionResult> CreateUserProfile([FromBody] NewProfile newProfileDTO)
    {
        var result = await _profileService.CreateUserProfile(newProfileDTO);

        if(result!=null)
        {
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpGet("/profile/get/{userId}"), Authorize]
    public async Task<IActionResult> GetUserProfileByUserId(string userId)
    {
        var result = await _profileService.GetUserProfileByUserId(userId);

        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

    [HttpPut("/profile/update"), Authorize]
    public async Task<IActionResult> UpdateUserProfile(UpdateProfile updateProfile)
    {
        var result =  await _profileService.UpdateUserProfile(updateProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

    [HttpPatch("/profile/updateinterests"), Authorize]
    public async Task<IActionResult> UpdateUserProfileInterests([FromBody]PatchProfileInterests patchProfile)
    {
        var result = await _profileService.UpdateUserProfileInterests(patchProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

       [HttpPatch("/profile/updateskills"), Authorize]
    public async Task<IActionResult> UpdateUserProfileSkills([FromBody]PatchProfileSkills patchProfile)
    {
        var result = await _profileService.UpdateUserProfileSkills(patchProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

       [HttpPatch("/profile/updatemissionstatement"), Authorize]
    public async Task<IActionResult> UpdateUserProfileMissionStatement([FromBody]PatchProfileMissionStatement patchProfile)
    {
        var result = await _profileService.UpdateUserProfileMissionStatement(patchProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

    [HttpDelete("/profile/delete/{userId}"), Authorize]
    public async Task<IActionResult> DeleteUserProfile(string userId)
    {
        var result = await _profileService.DeleteUserProfile(userId);    
        if(result!=null)
        {
        return Ok($"deleted profile: {result}");
        }
        return BadRequest();
    }
}