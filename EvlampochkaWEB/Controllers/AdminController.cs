using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvlampochkaWEB.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EvlampochkaWEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

       public async Task<IActionResult> Access(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);
            ViewBag.Role = role.First();
            return View(user);

        }

        public async Task<IActionResult> ChangeRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
           

            using (var transaction = _context.Database.BeginTransaction())
            {
                IdentityResult result = IdentityResult.Success;
                if(userRoles.First()=="admin")
                {
                    result = await _userManager.RemoveFromRoleAsync(user, "admin");
                    if (result == IdentityResult.Success)
                    {
                        result = await _userManager.AddToRoleAsync(user, "user");
                    }
                }
                else 
                {
                    result = await _userManager.RemoveFromRoleAsync(user, "user");
                    if (result == IdentityResult.Success)
                    {
                        result = await _userManager.AddToRoleAsync(user, "admin");
                    }
                }
                
                if (result == IdentityResult.Success)
                {                   
                        transaction.Commit();
                }
            }
            var role = await _userManager.GetRolesAsync(user);
            ViewBag.Role = role.First();

            return View("Edit",user);
        }

        public async Task <IActionResult> Delete(string id)
        {
            return View(await _userManager.FindByIdAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var logins = await _userManager.GetLoginsAsync(user);
           
            using(var transaction=_context.Database.BeginTransaction())
            {
                IdentityResult result = IdentityResult.Success;
                foreach(var login in logins)
                {
                     result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (result != IdentityResult.Success)
                        break;

                }
                if(result==IdentityResult.Success)
                {
                    foreach(var role in userRoles)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, role);
                        if (result != IdentityResult.Success)
                            break;
                    }
                }
                if(result==IdentityResult.Success)
                {
                    result = await _userManager.DeleteAsync(user);
                    if (result == IdentityResult.Success)
                        transaction.Commit();
                }
            }

            return View("Index", _userManager.Users.ToList());
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);
            ViewBag.Role = role.First();
            return View(await _userManager.FindByIdAsync(id));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(IdentityUser user)
        {
            var updateUser = await _userManager.FindByIdAsync(user.Id);
            if(updateUser!=null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    updateUser.Email = user.Email;
                    updateUser.UserName = user.UserName;
                    var result = await _userManager.UpdateAsync(updateUser);                  
                    if (result == IdentityResult.Success)
                        transaction.Commit();
                }
            }           
            
             return View("Index", _userManager.Users.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(IdentityUser user, string password, string role)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                user.UserName = user.Email;
                var result = await _userManager.CreateAsync(user, password);
                if (result == IdentityResult.Success)
                {
                    var newUser = await _userManager.FindByEmailAsync(user.Email);
                    result = await _userManager.AddToRoleAsync(newUser, role);
                }
                if (result == IdentityResult.Success)
                    transaction.Commit();
            }
            
            return View("Index", _userManager.Users.ToList());
        }

    }
}
