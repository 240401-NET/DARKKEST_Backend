// using DarkkestP3.API.DTO;
// using DarkkestP3.API.Service;
// using Microsoft.AspNetCore.Mvc;

// namespace DarkkestP3.API.Controller;

// [Route("api/[controller]")]
// [ApiController]
// public class ApplicationUserController : ControllerBase
// {    
//     private readonly IUserService _userService;

//     public ApplicationUserController(IUserService userService)
//     {
//         _userService = userService;
//     }

//     [HttpPost("/register")]
//     public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registration)
//     {
//         var result = await _userService.RegisterUser(registration);

//         if(result.Succeeded)
//         {
//             return Ok(result);
//         }        
//         return BadRequest(result.Errors);
//     }

//     [HttpPost("/login")]
//     public async Task<IActionResult> LoginUser([FromBody] LoginUser login)
//     {        
//         var result = await _userService.LoginUser(login);

//         if(!result.Succeeded)
//         {
//             return Unauthorized();
//         }

//         return Ok();
//     }

//     [HttpPost("/logout")]
//     public IActionResult Logout()
//     {
//         _userService.Logout();
//         return NoContent();
//     }
// }