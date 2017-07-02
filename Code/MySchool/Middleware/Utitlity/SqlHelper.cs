using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Middleware.Utitlity
{
    public class SqlHelper
    {
        const string sqlSafeRegex = "[^a-zA-Z0-9' .,$|<>=!()\\%-]";

       

        public static String SqlQuote(String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                

                value = value.Replace("'", "''");
            }
            return value;
        }

      

        public static String SqlSafeStringValue(String value)
        {
            
            try
            {
                if (!String.IsNullOrEmpty(value))
                {
                    

                    int pos = value.IndexOf(';');
                    if (pos != -1)
                    {
                        value = value.Substring(0, pos);
                    }

                    

                    value = Regex.Replace(value, sqlSafeRegex, "", RegexOptions.None, TimeSpan.FromSeconds(2));
                }
                return value;
            }
         
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}
