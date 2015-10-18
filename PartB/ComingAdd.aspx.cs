using PartB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartB
{
    public partial class ComingAdd : System.Web.UI.Page
    {
        private static ComingSoonModel movieModel = ComingSoonModel.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (movieTitle.Text == "" ||
                    shortDescription.Text == "" ||
                    longDescription.Text == "")
                    throw new Exception("Did not save!");

                ComingSoon upMovie = movieModel.AddMovie(
                    movieTitle.Text,
                    shortDescription.Text,
                    longDescription.Text,
                    "MovieComingSoon.png");
                Label1.Text = "";
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                Label1.Text = "ERROR: " + ex.Message.ToString();
                //Label1.Text = "Did not save!";
                Label1.Attributes.CssStyle.Add("color", "red");
            }
        }
    }
}