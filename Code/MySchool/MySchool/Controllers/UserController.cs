using Middleware.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySchool.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult User()
        {
            return PartialView();

        }

        [HttpGet]
        public JsonResult GetUsers(int? page, int? limit,
        string sortBy, string direction, string searchString = null)
        {

            var records = Users.GetUsers(searchString, sortBy, direction);
            var total = records.ToList().Count();



            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value).ToList();
            }

            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(Users userdata, bool isUpdate)
        {
            if (isUpdate)
            {
                Users.UpdateUser(userdata);
            }
            else
            {
                if (string.IsNullOrEmpty(userdata.Person_Id))
                {
                    string[] namestring = userdata.FullName.Split(' ');
                    userdata.Person_Id = Person.GetNewId();
                    Person persondata = new Person();
                    persondata.FirstName = namestring[0];
                    persondata.LastName = namestring[1];
                    persondata.Person_Id = userdata.Person_Id;
                    Person.AddPerson(persondata);
                }
                Users.AddUser(userdata);
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult Remove(Users userdata)
        {
            Users.DeleteUser(userdata);
            return Json(true);
        }
        [HttpPost]
        public JsonResult FindUser(Users userdata)
        {
            if (Users.UserExist(userdata))
                return Json(true);
            else
                return Json(false);
        }
        [HttpPost]
        public JsonResult GetUserList(string Prefix)
        {

            List<Users> ObjList = new List<Users>();
            ObjList = Users.GetUsers();
            //Searching records from list using LINQ query  
            var NameString = (from N in ObjList
                              where N.UserNameString.ToLower().Contains(Prefix.ToLower())
                              select new { N.UserNameString, N.UserName });
            return Json(NameString, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPersonList(string Prefix)
        {

            List<MySchool.Models.PersonAutoCompleteViewModel> ObjList = new List<MySchool.Models.PersonAutoCompleteViewModel>();
            ObjList = MySchool.Models.PersonAutoCompleteViewModel.GetPersons();
            //Searching records from list using LINQ query  
            var NameString = (from N in ObjList
                              where N.FullName.ToLower().Contains(Prefix.ToLower())
                              select new { N.FullName, N.Person_Id });
            return Json(NameString, JsonRequestBehavior.AllowGet);
        }  
    }
}