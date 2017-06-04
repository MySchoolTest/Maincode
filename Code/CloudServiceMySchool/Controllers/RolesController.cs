using Middleware.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySchool.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Role()
        {
            return PartialView();

        }

        [HttpGet]
        public JsonResult GetRoles(int? page, int? limit,
        string sortBy, string direction, string searchString = null)
        {

            var records = Roles.GetRoles(searchString, sortBy, direction);
            var total = records.ToList().Count();



            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).ToList();
            }

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(Roles roledata)
        {
            if (Roles.RoleExist(roledata,true))//To verify based on id
            {
                Roles.UpdateRoles(roledata);
            }
            else
            {

                Roles.AddRole(roledata);
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult Remove(Roles roledata)
        {
            Roles.DeleteRoles(roledata);
            return Json(true);
        }
        [HttpPost]
        public JsonResult FindRole(Roles roledata)
        {
            if (Roles.RoleExist(roledata))
                return Json(true);
            else
                return Json(false);
        }
        [HttpPost]
        public JsonResult GetRolesList(string Prefix)
        {

            List<Roles> ObjList = new List<Roles>();
            ObjList = Roles.GetRoles();
            //Searching records from list using LINQ query  
            var NameString = (from N in ObjList
                              where N.RoleName.ToLower().Contains(Prefix.ToLower())
                              select new { N.RoleDescription, N.RoleName });
            return Json(NameString, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNewRoleId()
        {

            string role_id = Roles.GetNewId();
            
            return Json(role_id, JsonRequestBehavior.AllowGet);
        }
    }
}