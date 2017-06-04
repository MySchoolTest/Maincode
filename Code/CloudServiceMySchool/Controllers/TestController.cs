using Middleware.BaseClass;
using Middleware.Utitlity;
using MySchool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySchool.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckMasterDatabase()
        {
            TestModel modelobj=new TestModel();
            modelobj.Message = "Connection has not been established";
            Connection mascon=new Connection();
            string connstring = mascon.MasterConnection;
            SqlConnection conn = new SqlConnection(connstring);
            try
            {
                conn.Open();
                modelobj.Message = "Connection is sucessfull";
            }
            finally
            {
                conn.Close();
            }
            
            return View("index", modelobj);
        }

        public ActionResult CheckClientDatabase(string id= "")
        {
            TestModel modelobj = new TestModel();
            modelobj.Message = "Connection has not been established";
            Connection conobj = new Connection(id);
            string connstring = conobj.ClientConnection;
            SqlConnection conn = new SqlConnection(connstring);
            try
            {
                conn.Open();
                modelobj.Message = "Connection is sucessfull";
            }
            finally
            {
                conn.Close();
            }
          DataSet ds=SqlQuery.GetClientValue("select * from person");
            return View("index", modelobj);
        }
    }
}