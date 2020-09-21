using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FirstAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FirstAuth.Data;

namespace FirstAuth.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly UserManager<IdentityUser> _userManager;

        public readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
        }

      
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
       public IActionResult CreateNewStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewStudent( Student newStudent)
        {
            if (ModelState.IsValid)
            {
                newStudent.AppUserId = _userManager.GetUserId(User);

                _dbContext.Students.Add(newStudent);

                return RedirectToAction("Index");
            }

            return View(newStudent);
            
        }

        [HttpGet]
        public IActionResult ChatApp()
        {
            return View();
        }

        [Authorize(Roles = "TeacherRole")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
