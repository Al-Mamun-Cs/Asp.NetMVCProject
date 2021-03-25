using AuthATMProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static AuthATMProject.ApplicationSignInManager;

namespace AuthATMProject.Controllers
{
    
    public class AdministratorController : Controller
    {
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;
        public AdministratorController()
        {
        }
        public AdministratorController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            RoleManger = roleManager;
            UserManager = userManager;
        }

        public ApplicationRoleManager RoleManger
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in RoleManger.Roles)
            {
                list.Add(new RoleViewModel(role));
            }
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel obj)
        {
            var role = new ApplicationRole() { Name = obj.Name };
            await RoleManger.CreateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManger.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel obj)
        {
            var role = new ApplicationRole() { Id = obj.Id, Name = obj.Name };
            await RoleManger.UpdateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManger.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManger.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            var role = await RoleManger.FindByIdAsync(id);
            await RoleManger.DeleteAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> EditUserInRole(string id)
        {
            var role = await RoleManger.FindByIdAsync(id);
            ViewBag.RoleId = id;
            ViewBag.RoleName = role.Name;
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role Id-{id} could not found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in UserManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel() { UserId = user.Id, UserName = user.UserName };
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);

        }
        [HttpPost]
        public async Task<ActionResult> EditUserInRole(List<UserRoleViewModel> viewModel, string id)
        {
            var role = await RoleManger.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role Id-{id} could not found";
                return View("NotFound");
            }
            for (int i = 0; i < viewModel.Count; i++)
            {
                var user = await UserManager.FindByIdAsync(viewModel[i].UserId);
                IdentityResult result = null;
                if (viewModel[i].IsSelected && !(await UserManager.IsInRoleAsync(user.Id, role.Name)))
                {
                    result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                }
                else if (!viewModel[i].IsSelected && await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    result = await UserManager.RemoveFromRoleAsync(user.Id, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (viewModel.Count - 1))
                        continue;
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}