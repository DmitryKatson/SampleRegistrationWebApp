using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dynamics365AppRegistration.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Dynamics365AppRegistration.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "AppRegistrations");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
