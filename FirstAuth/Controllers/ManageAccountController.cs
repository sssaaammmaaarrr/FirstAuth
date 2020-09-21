using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAuth.Data;
using FirstAuth.Models;
using FirstAuth.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstAuth.Controllers
{
    public class ManageAccountController : Controller
    {
        public readonly ApplicationDbContext _dbContext;
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public readonly RoleManager<IdentityRole> _roleManager;

        public ManageAccountController(ApplicationDbContext dbContext 
                                       , UserManager<IdentityUser> userManager,
                                       SignInManager<IdentityUser> signInManager,
                                       RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult RegisterNewAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterNewStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewStudent(RegisterStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { Email = model.Email, UserName = model.StudentName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Student newStudent = new Student() { Age = model.Age, AppUserId = user.Id ,StudentName=model.StudentName };
                    _dbContext.Students.Add(newStudent);
                    _dbContext.SaveChanges();
                    await _userManager.AddToRoleAsync(user, "studentRole");
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("index", "home");
                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( IdentityUser user)
        {
            var u =  await _userManager.FindByEmailAsync(user.Email);
            if(u.PasswordHash == user.PasswordHash)
            {
                await _signInManager.SignInAsync(u, true);
                return RedirectToAction("index", "home");
            }
            return View(user);

            
        }

        [HttpGet]
        public IActionResult RegisterNewTeacher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewTeacher(RegisterTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser() { UserName = model.username, Email = model.Email };

                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var newTeacher = new Teacher() { name = model.name, subject = model.subject, age = model.age, TUserId = newUser.Id };
                    _dbContext.teachers.Add(newTeacher);
                    _dbContext.SaveChanges();
                    await _userManager.AddToRoleAsync(newUser, "TeacherRole");
                     await _signInManager.SignInAsync(newUser, true);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
   
            }

    
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoles()
        {
            var StuRole = new IdentityRole() { Name = "StudentRole" };
            var TeachRole = new IdentityRole() { Name = "TeacherRole" };
            var result1 = await _roleManager.CreateAsync(StuRole);
            var result2 = await _roleManager.CreateAsync(TeachRole);

            if(result1.Succeeded && result2.Succeeded)
            {
                return RedirectToAction("index", "Home");
            }

            return Content("there is an erro");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
