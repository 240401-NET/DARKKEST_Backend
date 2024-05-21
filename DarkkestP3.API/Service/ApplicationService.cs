using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using DarkkestP3.API.DB;
using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
using DarkkestP3.API.Repository;
using Microsoft.AspNetCore.Identity;


namespace DarkkestP3.API.Service
{
    public class ApplicationService : IApplicationService
    {

        private readonly CommunityDBContext _context;

        public ApplicationService(CommunityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplications()
        {
            // get all applications from the database
            var applications = await _context.Applications.ToListAsync();
            var applicationDTOs = applications.Select(a => new ApplicationDTO
            {
                AppId = a.AppId,
                UserId = a.UserId,
                OppId = a.OppId,
                AppStatus = a.AppStatus,
                History = a.History,
                Notifications = a.Notifications
            });
            return applicationDTOs;
        }

        public Task<ApplicationDTO> GetApplication(int appId)
        {
            var application = _context.Applications.Find(appId);
            var applicationDTO = new ApplicationDTO
            {
                AppId = application.AppId,
                UserId = application.UserId,
                OppId = application.OppId,
                AppStatus = application.AppStatus,
                History = application.History,
                Notifications = application.Notifications
            };
            return Task.FromResult(applicationDTO);
        }

        public Task<IdentityResult> CreateApplication(CreateApplication createApplication)
        {
            var application = new Application
            {
                UserId = createApplication.UserId,
                OppId = createApplication.OppId,
                AppStatus = ApplicationStatus.NotSubmitted,
                History = "",
                Notifications = ""
            };
            _context.Applications.Add(application);
            _context.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateApplication(int appId, UpdateApplication updateApplication)
        {
            var application = _context.Applications.Find(appId);
            application.UserId = updateApplication.UserId;
            application.OppId = updateApplication.OppId;
            application.AppStatus = updateApplication.AppStatus;
            application.History = updateApplication.History;
            application.Notifications = updateApplication.Notifications;
            _context.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteApplication(int appId)
        {
            var application = _context.Applications.Find(appId);
            _context.Applications.Remove(application);
            _context.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplicationsForUser(int userId)
        {
            // find an application by userId
            var applications = _context.Applications.Where(a => a.UserId == userId);
            var applicationDTOs = await applications.Select(a => new ApplicationDTO
            {
                AppId = a.AppId,
                UserId = a.UserId,
                OppId = a.OppId,
                AppStatus = a.AppStatus,
                History = a.History,
                Notifications = a.Notifications
            }).ToListAsync();
            return applicationDTOs;
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplicationsForOpportunity(int opportunityId)
        {
            var applications = _context.Applications.Where(a => a.OppId == opportunityId);
            var applicationDTOs = await applications.Select(a => new ApplicationDTO
            {
                AppId = a.AppId,
                UserId = a.UserId,
                OppId = a.OppId,
                AppStatus = a.AppStatus,
                History = a.History,
                Notifications = a.Notifications
            }).ToListAsync();
            return applicationDTOs;
        }

        public async Task<IdentityResult> SubmitApplication(int appId)
        {
            var application = await _context.Applications.FindAsync(appId);
            if (application == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Application with id {appId} not found" });
            }

            application.AppStatus = ApplicationStatus.Pending;
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ApproveApplication(int appId)
        {
            var application = await _context.Applications.FindAsync(appId);
            if (application == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Application with id {appId} not found" });
            }

            application.AppStatus = ApplicationStatus.Approved;
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();

            // Here you might want to add code to send a notification to the user

            return IdentityResult.Success;
        }
    }
}