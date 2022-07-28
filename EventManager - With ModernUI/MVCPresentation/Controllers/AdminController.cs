using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVCPresentation;
using MVCPresentation.Models;
using DataObjects;
using LogicLayer;

namespace MVCPresentation.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.Email).ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            var usrMgr = new LogicLayer.UserManager();
            var allRoles = usrMgr.RetrieveAllRoles();
            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;
            return View(applicationUser);
        }

        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (role == "Administrator")
            {
                var AdminUsers = userManager.Users.ToList().Where(u => userManager.IsInRole(u.Id, role)).ToList().Count();
                if (AdminUsers < 2)
                {
                    ViewBag.Error = "Cannot remove last administrator";
                } else
                {
                    userManager.RemoveFromRole(id, role);
                    RemoveRoleFromDesktop(role, user);
                }
            } else
            {
                userManager.RemoveFromRole(id, role);
                RemoveRoleFromDesktop(role, user);
            }

            var usrMgr = new LogicLayer.UserManager();
            var allRoles = usrMgr.RetrieveAllRoles();
            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;
            return View("Details", user);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/2/25
        /// 
        /// Description:
        /// Helper method to remove roles from desktop
        /// 
        /// </summary>
        /// <param name="role">String representing the role to remove</param>
        /// <param name="user">The ApplicationUser linked to the role on web</param>
        private static void RemoveRoleFromDesktop(string role, ApplicationUser user)
        {
            if (user.Email != null)
            {
                try
                {
                    var usrMgr = new LogicLayer.UserManager();
                    User newUser = usrMgr.RetrieveUserByEmail(user.Email);
                    usrMgr.RemoveUserRole(newUser.UserID, role);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public ActionResult AddRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            userManager.AddToRole(id, role);

            if (user.Email != null)
            {
                try
                {
                    var usrMgr = new UserManager();
                    User newUser = usrMgr.RetrieveUserByEmail(user.Email);
                    usrMgr.AddUserRole(newUser.UserID, role);
                }
                catch (Exception ex)
                {
                }
            }
            return RedirectToAction("Details", "Admin", new { id = user.Id });
        }


    }
}
