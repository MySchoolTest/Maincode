using MySchool.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Middleware.BaseClass;
namespace MySchool.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult UserRoles()
        {
            return View();
        }
        
        public ActionResult Roles()
        {
            return PartialView();
        }
        
    }
}