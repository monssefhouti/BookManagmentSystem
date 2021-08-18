using BookManagment.Models.Entities;
using BookManagment.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagment.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;

        }
        [HttpGet]
        public IActionResult RolesList()
        {
            return View();
        }
        #region CreateRole
        [HttpGet]
        public IActionResult CreateRole()
        {   
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateRole(RoleSViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName
                };
                var res =await roleManager.CreateAsync(role);
                if(res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                    foreach (var error in res.Errors)
                    {
                    ModelState.AddModelError("", error.Description);
                    }
            }
            return View(model);
        }
        #endregion

        #region UpdateRole
        public IActionResult UpdateRole()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateRole(RoleSViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName
                };
                var res = await roleManager.UpdateAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        #endregion
    }
}
