using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PartB.Models
{
    public class UserModel
    {
        public static Boolean ValidateUserNamePassword(string username, string password)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "select pword from [master].[dbo].[UserCredential] where uname = @username";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    string pwd = (string)cmd.ExecuteScalar();

                    return PasswordHash.PasswordHash.ValidatePassword(password, pwd);
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                    if (conn != null)
                    {
                        conn.Dispose();
                    }
                }
            }
        }
    }
}