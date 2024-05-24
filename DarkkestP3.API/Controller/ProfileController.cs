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
    private readonly IUserService _userService;

    public ProfileController(IProfileService profileService, IUserService userService)
    {
        _profileService = profileService;
        _userService = userService;
    }

    [HttpPost("/profile/create")]
    public async Task<IActionResult> CreateUserProfile([FromBody] NewProfile newProfileDTO)
    {
        newProfileDTO.userId = GetUserId();
        var result = await _profileService.CreateUserProfile(newProfileDTO);

        if(result!=null)
        {
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpGet("/profile/get")]
    public async Task<IActionResult> GetUserProfileByUserId()
    {
        string userId = GetUserId();
        var result = await _profileService.GetUserProfileByUserId(userId);

        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

    [HttpPut("/profile/update")]
    public async Task<IActionResult> UpdateUserProfile(UpdateProfile updateProfile)
    {
        updateProfile.userId = GetUserId();
        var result =  await _profileService.UpdateUserProfile(updateProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

    [HttpPatch("/profile/updateinterests")]
    public async Task<IActionResult> UpdateUserProfileInterests([FromBody]PatchProfileInterests patchProfile)
    {
        patchProfile.userId = GetUserId();
        var result = await _profileService.UpdateUserProfileInterests(patchProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

       [HttpPatch("/profile/updateskills")]
    public async Task<IActionResult> UpdateUserProfileSkills([FromBody]PatchProfileSkills patchProfile)
    {
        patchProfile.userId = GetUserId();
        var result = await _profileService.UpdateUserProfileSkills(patchProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

       [HttpPatch("/profile/updatemissionstatement")]
    public async Task<IActionResult> UpdateUserProfileMissionStatement([FromBody]PatchProfileMissionStatement patchProfile)
    {
        patchProfile.userId = GetUserId();
        var result = await _profileService.UpdateUserProfileMissionStatement(patchProfile);
        if(result!=null)
        {
        return Ok(result);
        }
        return BadRequest();
    }

    [HttpDelete("/profile/delete")]
    public async Task<IActionResult> DeleteUserProfile()
    {
        string userId = GetUserId();
        var result = await _profileService.DeleteUserProfile(userId);
        string userName = GetUserName();    
        if(result!=null)
        {
        return Ok($"deleted profile for : {userName}");
        }
        return BadRequest();
    }

        private string GetUserId()
    {
        return _userService.GetUserIdByName(HttpContext.User.Identity!.Name!);
    }
        private string GetUserName()
    {
        return HttpContext.User.Identity!.Name!;
    }
}