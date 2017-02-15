using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dynamics365AppRegistration.Models;
using Dynamics365AppRegistration.Data;
using Microsoft.Extensions.Options;

namespace Dynamics365AppRegistration.Controllers
{
    [Produces("application/json")]
    [Route("api/CheckRegistration")]
    public class CheckRegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;


        public CheckRegistrationController(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        // GET: api/CheckRegistration
        [HttpGet]
        public IActionResult Get(string tenantId, string appId, string CompanyName, int numberUsers)
        {
            var result = new CheckRegistration();
            result.AccessLevel = AccessLevel.NoAccess;
            result.Notification = string.Empty;

            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(CompanyName))
            {
                result.Notification = "Incorrect parameters used to check registration.";
                return this.Ok(result);
            }
            var appInfo = _appSettings.AppInfo.FirstOrDefault<AppInfo>(a => a.id == appId);
            if (appInfo == null)
            {
                result.Notification = "The used app id is unknown. Please contact the app vendor to get this solved.";
                return this.Ok(result);
            }

            AppRegistration appRegistration;

            appRegistration = _context.AppRegistration.FirstOrDefault<AppRegistration>(r => r.TenantId == tenantId && r.AppId == appId && r.D365CompanyName == CompanyName);

            if (appRegistration == null)
            {
                result.Notification = $"Please register the {appInfo.name} app to unlock all features.";
                return this.Ok(result);
            }

            result.AccessLevel = appRegistration.AccessLevel;

            if (appRegistration.NumberRegisteredUsers < numberUsers)
            {
                result.Notification = "The number of registered users is incorrect. Please contact the app vendor to update your registration.";
            }

            return this.Ok(result);
        }
    }
}
