using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;

namespace PartB
{
    public partial class Login : System.Web.UI.Page
    {
        private const string pattern = @"^[a-zA-Z0-9@#$%&*+\-_(),+':;?.,!\[\]\\/]+$";

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void EnterCredentials_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(pattern);
            Match matchUsername = regex.Match(Username.Text);
            Match matchPassword = regex.Match(Password.Text);
            if (!matchUsername.Success || !matchPassword.Success)
            {
                Incorrect();
            } 
            else
            {
                SqlConnection conn = null;
                SqlCommand cmd = null;
                string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (conn = new SqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        string sql = "select pword from [master].[dbo].[UserCredential] where uname = @email";
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@email", matchUsername.Value);
                        string pwd = (string)cmd.ExecuteScalar();

                        if (PasswordHash.PasswordHash.ValidatePassword(matchPassword.Value, pwd))
                        {
                            LoginLabel.Text = "Success! " + matchUsername.Value;
                            Session["username"] = matchUsername.Value;
                            if (Session["username"] != null)
                                FormsAuthentication.RedirectFromLoginPage(matchUsername.Value, true);
                        }
                        else
                            Incorrect();
                    }
                    catch (Exception ex)
                    {
                        Incorrect();

                        LoginLabel.Text += ex.Message;
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

        private void Incorrect()
        {
            LoginLabel.Text = "Invalid Username and/or Password";
            LoginLabel.Attributes.CssStyle.Add("color", "red");
        }
    }
}