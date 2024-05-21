// create using statements for everything we'll need for a controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Darkkest.API.Model;
using Darkkest.API.DTO;
using Darkkest.API.Service;
using Microsoft.AspNetCore.Mvc;



namespace Darkkest.API.Controller
{
    // create a class that will be our controller
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        // create a constructor that takes in an IApplicationService
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // create a method that will return all applications
        [HttpGet("/applications")]
        public async Task<IActionResult> GetApplications()
        {
            var applications = await _applicationService.GetApplications();
            return Ok(applications);
        }

        // create a method that will return a single application
        [HttpGet("/applications/{appId}")]
        public async Task<IActionResult> GetApplication(int appId)
        {
            var application = await _applicationService.GetApplication(appId);
            return Ok(application);
        }

        // create a method that will create a new application
        [HttpPost("/applications")]
        public async Task<IActionResult> CreateApplication([FromBody] CreateApplication createApplication)
        {
            var result = await _applicationService.CreateApplication(createApplication);

            if(result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }

        // create a method that will update an application
        [HttpPut("/applications/{appId}")]
        public async Task<IActionResult> UpdateApplication(int appId, [FromBody] UpdateApplication updateApplication)
        {
            var result = await _applicationService.UpdateApplication(appId, updateApplication);

            if(result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }

        // create a method that will delete an application
        [HttpDelete("/applications/{appId}")]
        public async Task<IActionResult> DeleteApplication(int appId)
        {
            var result = await _applicationService.DeleteApplication(appId);

            if(result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("/applications/{appId}/submit")]
        public async Task<IActionResult> SubmitApplication(int appId)
        {
            var result = await _applicationService.SubmitApplication(appId);

            if(result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("/applications/{appId}/approve")]
        public async Task<IActionResult> ApproveApplication(int appId)
        {
            var result = await _applicationService.ApproveApplication(appId);

            if(result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }

        // create a method that will return all applications for a user
        [HttpGet("/applications/user/{userId}")]
        public async Task<IActionResult> GetApplicationsForUser(int userId)
        {
            var applications = await _applicationService.GetApplicationsForUser(userId);
            return Ok(applications);
        }

        // create a method that will return all applications for an opportunity
        [HttpGet("/applications/opportunity/{oppId}")]
        public async Task<IActionResult> GetApplicationsForOpportunity(int oppId)
        {
            var applications = await _applicationService.GetApplicationsForOpportunity(oppId);
            return Ok(applications);
        }
        
    }
}