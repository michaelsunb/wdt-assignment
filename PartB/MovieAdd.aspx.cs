using PartB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartB
{
    public partial class MovieAdd : System.Web.UI.Page
    {
        private static MovieModel movieModel = MovieModel.Instance;
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
                Movie upMovie = MovieModel.Instance.AddMovie(
                    movieTitle.Text,
                    shortDescription.Text,
                    longDescription.Text,
                    movieImage.Text,
                    double.Parse(price.Text));
                Label1.Text = "";
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                Label1.Text = ex.StackTrace;
                //Label1.Text = "Did not save!";
                Label1.Attributes.CssStyle.Add("color", "red");
            }
        }
    }
}