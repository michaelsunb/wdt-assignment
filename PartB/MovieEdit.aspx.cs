using PartB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartB
{
    public partial class MovieEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                Movie movie = MovieModel.Instance.getMovieByID(id);
                movieTitle.Text = movie.Title;
                shortDescription.Text = movie.ShortDecription;
                longDescription.Text = movie.LongDecription;
                price.Text = movie.price.ToString();
                movieImage.Text = movie.ImageUrl;
            }
            catch (Exception ex)
            {
                Response.Redirect("Default.aspx");
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}