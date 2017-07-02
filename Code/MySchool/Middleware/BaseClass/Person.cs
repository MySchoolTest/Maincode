using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.BaseClass
{
  public   class Person
    {
        public string Person_Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public List<string> Roles { get; set; }

        public Person GetPersonDetails(string id="")
        {
            return this;
        }
        public static string GetNewId()
        {


            string ret_id = ""; //Define default
            try
            {


                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("Select ltrim(rtrim(max(Person_id)))+1 as Id from person");


                using (SqlConnection dbCon = new SqlConnection(dbConString))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sq_stmt, dbCon))
                    {

                        using (SqlDataReader wizReader = dbCom.ExecuteReader())
                        {
                            while (wizReader.Read())
                            {
                                ret_id=Convert.ToString(wizReader["Id"]);
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
        
       
        public static int AddPerson(Person persondata)
        {


            int ret = -1;
            try
            {

                
                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = string.Format("insert into Person(Person_id,First_name,Last_name) values('{0}','{1}','{2}')", persondata.Person_Id, persondata.FirstName, persondata.LastName);

                  
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
    }
}
