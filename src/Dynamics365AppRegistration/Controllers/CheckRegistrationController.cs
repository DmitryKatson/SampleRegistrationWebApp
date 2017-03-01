using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dynamics365AppRegistration.Models;
using Dynamics365AppRegistration.Data;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Get(string tenantid, string appid, int numberusers, bool isEvaluationcompany)
        {
            var result = new CheckRegistration();
            result.AccessLevel = AccessLevel.NoAccess;
            result.NotificationMessage = string.Empty;

            if (string.IsNullOrEmpty(tenantid) || string.IsNullOrEmpty(appid))
            {
                result.NotificationMessage = "Incorrect parameters used to check registration. Please contact the app vendor to get this solved.";
                return this.Ok(result);
            }
            var appInfo = _appSettings.AppInfo.FirstOrDefault<AppInfo>(a => a.id.ToLower() == appid.ToLower());
            if (appInfo == null)
            {
                result.NotificationMessage = "The app id is unknown. Please contact the app vendor to get this solved.";
                return this.Ok(result);
            }

            AppRegistration appRegistration;

            if (isEvaluationcompany)
            {
                result.AccessLevel = AccessLevel.Full;
                return this.Ok(result);
            }

            appRegistration = _context.AppRegistration.FirstOrDefault<AppRegistration>(r => r.TenantId == tenantid && r.AppId == appid);

            if (appRegistration == null)
            {
                result.NotificationMessage = $"Please register the {appInfo.name} app to unlock all features.";
                result.NotificationActionText = "Click here to register";
                result.RedirectUrl = Url.Action("Create", "AppRegistrations", FormMethod.Get, Request.Scheme) + Request.QueryString.Value;
                return this.Ok(result);
            }

            result.AccessLevel = appRegistration.AccessLevel;

            if (appRegistration.NumberRegisteredUsers < numberusers)
            {
                result.NotificationMessage = "The number of registered users is incorrect. Please contact the app vendor to update your registration.";
                result.NotificationActionText = "Click here to update";
                result.RedirectUrl = "TODO";
            }

            return this.Ok(result);
        }
    }
}
