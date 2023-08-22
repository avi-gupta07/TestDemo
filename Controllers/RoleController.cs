using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestDemo.Models;

namespace TestDemo.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public ApplicationRoleManager RoleManager
        {
            get {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }

            private set {            
            _roleManager = value;   
            }
        }

        // GET: Role
       

       [Authorize(Roles= "Manager")]
        public ActionResult Index()
        {
            var roles = RoleManager.Roles.ToList();
            
            List< RoleViewModel> roleList = new List<RoleViewModel>();

            foreach(var role in roles)
            {
                RoleViewModel roleViewModel = new RoleViewModel();
                roleViewModel.RoleId = role.Id;
                roleViewModel.RoleName = role.Name;

                roleList.Add(roleViewModel);
            }
            return View(roleList);
        }
        [Authorize(Roles = "Manager")]
        public ActionResult AddRole() { 
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(RoleViewModel model)
        {
            IdentityRole role = new IdentityRole {Name = model.RoleName };
            IdentityResult result = RoleManager.Create(role);

            if (result.Succeeded)
            {               
                return RedirectToAction("Index");
            }
            return View(model);

        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult EditRole(string roleId)
        {
            if(roleId != null)
            {
                IdentityRole foundRole = RoleManager.FindById(roleId);
                RoleViewModel role = new RoleViewModel() { RoleId =foundRole.Id , RoleName = foundRole.Name};
                return View(role);
            }

            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult EditRole(RoleViewModel model)
        {
            IdentityRole foundRole = RoleManager.FindById(model.RoleId);
            if (foundRole == null)
            {
                return HttpNotFound();
            }

            if (foundRole.Name != model.RoleName)
            {
                foundRole.Name = model.RoleName;    
            }

            IdentityResult result = RoleManager.Update(foundRole);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [Authorize(Roles = "Manager")]
        public ActionResult DeleteRole (string roleId)
        {
            if (roleId != null)
            {
                IdentityRole foundRole = RoleManager.FindById(roleId);
                RoleViewModel role = new RoleViewModel() { RoleId = foundRole.Id, RoleName = foundRole.Name };
                return View(role);

            }
            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult DeleteRole(RoleViewModel model) {
            IdentityRole foundRole = RoleManager.FindById(model.RoleId);
            if(foundRole == null)
            {
                return HttpNotFound();
            }

            IdentityResult result = RoleManager.Delete(foundRole);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }


        
        [CustomAuthorize(ClaimType = "Role", ClaimValue = "Moderator")]
        public ActionResult TestingMethod()
        {
            

            return View();
        }
     
    }
}