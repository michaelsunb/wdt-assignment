using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace PartB
{
    public partial class Login : System.Web.UI.Page
    {
        private const string pattern = @"^[a-zA-Z0-9@#$%&*+\-_(),+':;?.,!\[\]\\/]+$";

        AssignmentEntities db = new AssignmentEntities();

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
                var userLogin = from d in db.UserCredentials select d;
                userLogin = userLogin.Where(x => x.uname == matchUsername.Value);

                try {
                    if(PasswordHash.PasswordHash.ValidatePassword(
                        matchPassword.Value, userLogin.Single().pword
                        )) {
                    LoginLabel.Text = "Success! " + userLogin.ToList()[0];
                    Session["username"] = userLogin.Single().uname.ToString();
                    if (Session["username"] != null)
                        FormsAuthentication.RedirectFromLoginPage(userLogin.Single().uname, true);
                    }
                    else
                        Incorrect();
                } catch (Exception ex) {
                    Incorrect();
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