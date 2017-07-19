using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Dynamics365AppRegistration.Data;
using Dynamics365AppRegistration.Models;


namespace Dynamics365AppRegistration.Controllers
{
    [Authorize]
    public class AppRegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public AppRegistrationsController(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        // GET: AppRegistrations
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppRegistration.ToListAsync());
        }

        // GET: AppRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRegistration = await _context.AppRegistration.SingleOrDefaultAsync(m => m.id == id);
            if (appRegistration == null)
            {
                return NotFound();
            }

            return View(appRegistration);
        }

        // GET: AppRegistrations/Create
        [AllowAnonymous]
        public IActionResult Create(string tenantId, string appId, int numberUsers)
        {
            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(appId))
            {
                return Ok("Incorrect parameters");
            }

            AppRegistration appRegistration;

            appRegistration = _context.AppRegistration.FirstOrDefault<AppRegistration>(r => r.TenantId == tenantId && r.AppId == appId);

            if (appRegistration != null)
            {
                return View("RegistrationSuccessful", appRegistration);
            }

            var appInfo = _appSettings.AppInfo.FirstOrDefault<AppInfo>(a => a.id.ToLower() == appId.ToLower());
            if (appInfo == null)
            {
                return Ok("Unknown app id");
            }

            ViewData["AppName"] = appInfo.name;

            appRegistration = new AppRegistration()
            {
                AppId = appId,
                TenantId = tenantId,
                RegistrationDate = DateTime.Today,
                NumberRegisteredUsers = numberUsers
            };

            if (!ModelState.IsValid)
            {
                return View("Incorrect parameters");
            }

            return View(appRegistration);
        }

        // POST: AppRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppId,CompanyAddress,CompanyCity,CompanyCountry,CompanyName,CompanyPostcode,ContactEmail,ContactName,ContactPhoneNo,NumberRegisteredUsers,RegistrationDate,TenantId")] AppRegistration appRegistration)
        {
            if (ModelState.IsValid)
            {
                appRegistration.AccessLevel = AccessLevel.Full;
                _context.Add(appRegistration);
                await _context.SaveChangesAsync();
                return View("RegistrationSuccessful", appRegistration);
            }
            return View(appRegistration);
        }

        // GET: AppRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRegistration = await _context.AppRegistration.SingleOrDefaultAsync(m => m.id == id);
            if (appRegistration == null)
            {
                return NotFound();
            }
            return View(appRegistration);
        }

        // POST: AppRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,AppId,CompanyAddress,CompanyCity,CompanyCountry,CompanyName,CompanyPostcode,ContactEmail,ContactName,ContactPhoneNo,NumberRegisteredUsers,RegistrationDate,TenantId")] AppRegistration appRegistration)
        {
            if (id != appRegistration.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppRegistrationExists(appRegistration.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(appRegistration);
        }

        // GET: AppRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRegistration = await _context.AppRegistration.SingleOrDefaultAsync(m => m.id == id);
            if (appRegistration == null)
            {
                return NotFound();
            }

            return View(appRegistration);
        }

        // POST: AppRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appRegistration = await _context.AppRegistration.SingleOrDefaultAsync(m => m.id == id);
            _context.AppRegistration.Remove(appRegistration);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AppRegistrationExists(int id)
        {
            return _context.AppRegistration.Any(e => e.id == id);
        }
    }
}
