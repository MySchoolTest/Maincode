using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.BaseClass
{
    public class Roles
    {
        public string Role_Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
       

        public static List<Roles> GetRoles(string searchString = null, string sortBy = null, string direction = null)
        {

            List<Roles> records = new List<Roles>();

            //To do Prevent input from Sql injection

            sortBy = ModeltoDb(sortBy);

            try
            {

                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = "select Role_id,Role_name,Role_desc from Roles";
                if (searchString != null)
                {
                    sq_stmt = string.Format("select Role_id,Role_name,Role_desc from Roles where Role_name like '%{0}%'", searchString);


                }
                if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
                {
                    sq_stmt = sq_stmt + " order by " + sortBy + " " + direction;
                }
                using (SqlConnection dbCon = new SqlConnection(dbConString))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sq_stmt, dbCon))
                    {


                        using (SqlDataReader wizReader = dbCom.ExecuteReader())
                        {
                            while (wizReader.Read())
                            {
                                var p = new Roles()
                                {
                                  Role_Id = (string)wizReader["Role_Id"],
                                    RoleName = (string)wizReader["Role_name"],
                                   RoleDescription = (string)wizReader["Role_desc"]
                                   

                                };

                                records.Add(p);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return records;
        }

        public static bool RoleExist(Roles roledata,bool keyid=false)
        {


            bool exist = false;
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("Select Role_name from Roles where Role_name='{0}'", roledata.RoleName);

                if(keyid)
                {
                    sq_stmt = string.Format("Select Role_name from Roles where Role_Id='{0}'", roledata.Role_Id);
                }
                using (SqlConnection dbCon = new SqlConnection(dbConString))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sq_stmt, dbCon))
                    {

                        using (SqlDataReader wizReader = dbCom.ExecuteReader())
                        {
                            while (wizReader.Read())
                            {
                                exist = true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return exist;

        }
        public static int UpdateRoles(Roles roledata)
        {


            int ret = -1;
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("update Roles set Role_name='{0}',Role_desc='{1}' where Role_id='{2}'", roledata.RoleName, roledata.RoleDescription, roledata.Role_Id);


                using (SqlConnection dbCon = new SqlConnection(dbConString))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sq_stmt, dbCon))
                    {

                        ret = dbCom.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ret;

        }

        public static int AddRole(Roles roledata)
        {


            int ret = -1;
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("insert into Roles(Role_id,Role_name,Role_desc) values('{0}','{1}','{2}')", roledata.Role_Id, roledata.RoleName, roledata.RoleDescription);


                using (SqlConnection dbCon = new SqlConnection(dbConString))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sq_stmt, dbCon))
                    {

                        ret = dbCom.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ret;

        }

        public static int DeleteRoles(Roles roledata)
        {


            int ret = -1;
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("delete from Roles where Role_id='{0}'", roledata.Role_Id);


                using (SqlConnection dbCon = new SqlConnection(dbConString))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sq_stmt, dbCon))
                    {

                        ret = dbCom.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ret;

        }

        public static string ModeltoDb(string modelfield)
        {
            string dbfield = modelfield;
            switch (modelfield)
            {

                case "Role_Id":
                    dbfield = "Role_id";
                    break;
                case "RoleName":
                    dbfield = "Role_name";
                    break;
                case "RoleDescription":
                    dbfield = "Role_desc";
                    break;
                


            }

            return dbfield;
        }

        public static string GetNewId()
        {


            string ret_id = ""; //Define default
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("Select ltrim(rtrim(max(Role_id)))+1 as Id from Roles");


                using (SqlConnection dbCon = new SqlConnection(dbConString))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sq_stmt, dbCon))
                    {

                        using (SqlDataReader wizReader = dbCom.ExecuteReader())
                        {
                            while (wizReader.Read())
                            {
                                ret_id = Convert.ToString(wizReader["Id"]);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ret_id;

        }
    }
}
