using Microsoft.WindowsAzure.ServiceRuntime;
using Middleware.BaseClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Utitlity
{
    public static class SqlQuery 
    {
      
 
        public static DataSet GetClientValue(String query)
        {
            
            
            
           
           
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(MySession.Current.DataBaseConnection))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }

                conn.Close();
            }

            return ds;
        }

        public static DataSet GetMasterValue(String query)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }

                conn.Close();
            }

            return ds;
        }

    }
}