using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.BaseClass
{
    public class Connection
    {
        public string MasterConnection { get; set; }

        public string ClientConnection { get; set; }

        public Connection(string Clientid = "")
        {
            this.MasterConnection = RoleEnvironment.GetConfigurationSettingValue("MasterDatbase");
            if(String.IsNullOrEmpty(Clientid))
            {
                if(MySession.Current.DataBaseConnection!=null)
                {
                    this.ClientConnection = MySession.Current.DataBaseConnection;
                }
            }
            else
            {

            using (SqlConnection conn = new SqlConnection(this.MasterConnection))
            {
                conn.Open();

                String query = "SELECT Connection FROM Clients WHERE Client_id=@Client_id AND EndDate>GetDate()";

                bool clientFound = false;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Client_id", Clientid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            clientFound = true;
                            this.ClientConnection = Convert.ToString(reader["Connection"]);
                            MySession.Current.DataBaseConnection = this.ClientConnection;

                        }
                    }
                }

                if (!clientFound)
                {
                    throw new UnauthorizedAccessException("Cannot login as  - client " + Clientid + " not found or invalid in Master database");
                }

            }

        }
        }

    }
}