using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.BaseClass
{
    public class Users
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Person_Id { get; set; }
        public string UserNameString { get; set; }
        public static List<Users> GetUsers(string searchString = null, string sortBy = null, string direction = null)
        {

            List<Users> records = new List<Users>();

            //To do Prevent input from Sql injection

            sortBy = ModeltoDb(sortBy);

            try
            {

                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = "select Username,[Password],First_name+' '+Last_name as FullName,Person.Person_id from Person join Users on Person.Person_id=Users.Person_id";
                if (searchString != null)
                {
                    sq_stmt = string.Format("select Username,[Password],First_name+' '+Last_name as FullName,Person.Person_id from Person join Users on Person.Person_id=Users.Person_id where UserName='{0}'", searchString);


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
                                var p = new Users()
                                {
                                    UserName = (string)wizReader["Username"],
                                    FullName = (string)wizReader["FullName"],
                                    Password = (string)wizReader["Password"],
                                    UserNameString = (string)wizReader["FullName"] + "(" + (string)wizReader["Username"] + ")",
                                    Person_Id = wizReader["Person_id"].ToString()

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

        public static bool UserExist(Users userdata)
        {


            bool exist = false;
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("Select [Username] from Users where [Username]='{0}'", userdata.UserName);


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
        public static int UpdateUser(Users userdata)
        {


            int ret = -1;
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("update [Users] set Username='{0}',[Password]='{1}' where Person_id='{2}'", userdata.UserName, userdata.Password, userdata.Person_Id);


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

        public static int AddUser(Users userdata)
        {


            int ret = -1;
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("insert into [Users] values('{0}','{1}','{2}')", userdata.Person_Id, userdata.UserName, userdata.Password);


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

        public static int DeleteUser(Users userdata)
        {


            int ret = -1;
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("delete from [Users] where Username='{0}'", userdata.UserName);


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

                case "Person_Id":
                    dbfield = "Person_id";
                    break;
                case "FullName":
                    dbfield = "First_name+' '+Last_name";
                    break;
                case "UserName":
                    dbfield = "Username";
                    break;
                case "Password":
                    dbfield = "Password";
                    break;


            }

            return dbfield;
        }
    }
}
