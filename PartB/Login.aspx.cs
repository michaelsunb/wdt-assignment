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
using System.Web.Optimization;
using PartB.Models;

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
                return;
            }

            if (!UserModel.ValidateUserNamePassword(matchUsername.Value,matchPassword.Value))
            {
                Incorrect();
            }

            FormsAuthentication.RedirectFromLoginPage(matchUsername.Value, true);
        }

        private void Incorrect()
        {
            LoginLabel.Text = "Invalid Username and/or Password";
            LoginLabel.Attributes.CssStyle.Add("color", "red");
        }
    }
}