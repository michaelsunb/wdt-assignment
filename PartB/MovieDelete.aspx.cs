using PartB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartB
{
    public partial class MovieDelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MovieModel movieModel = MovieModel.Instance;
                int id = int.Parse(Request.QueryString["id"]);
                Movie movie = movieModel.getMovieByID(id);
                movieModel.SoftDelete(movie);
            }
            catch (Exception ex)
            {
                Label1.Text = ex.StackTrace;
            }
            Response.Redirect("Default.aspx");
        }
    }
}