using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MySchool.Models
{
    public class PersonAutoCompleteViewModel
    {
        public string Person_Id { get; set; }
        public string FullName { get; set; }
        public static List<PersonAutoCompleteViewModel> GetPersons(string searchString = null)
        {

            List<Models.PersonAutoCompleteViewModel> records = new List<Models.PersonAutoCompleteViewModel>();

            //To do Prevent input from Sql injection



            try
            {

                string dbConString = "Server=WEB4;Database=ClientDatabase;User ID=sa;Password=hrms;Trusted_Connection=False;";
                string sq_stmt = "select First_name+' '+Last_name as FullName,Person_id from Person";
                if (searchString != null)
                {
                    sq_stmt = string.Format("select First_name+' '+Last_name as FullName,Person_id from Person where First_name+' '+Last_name like '{0}'", searchString);


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
                                var p = new PersonAutoCompleteViewModel()
                                {
                                    
                                    FullName = (string)wizReader["FullName"],
                                    Person_Id = (string)wizReader["Person_id"]

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


    }
}