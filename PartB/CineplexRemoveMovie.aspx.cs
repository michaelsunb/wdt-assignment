using PartB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartB
{
    public partial class CineplexRemoveMovie : System.Web.UI.Page
    {
        private static CineplexMovieModel cmModel = CineplexMovieModel.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                cmModel.RemoveMovie(id);
            }
            catch (Exception ex)
            {
                Label1.Text = ex.StackTrace;
            }
            Response.Redirect("CineplexAddMovie.aspx");
        }
    }
}