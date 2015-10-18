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
        private static MovieModel movieModel = MovieModel.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                Movie movie = movieModel.getMovieByID(id);
                if (Page.IsPostBack) return;
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
        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg" };
                    string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                    bool isValidFile = false;
                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (ext == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }

                    if (!isValidFile)
                    {
                        throw new Exception("Did not save!");
                    }

                    FileUpload1.SaveAs(Server.MapPath("~/Content/images/") +
                            FileUpload1.FileName);
                    movieImage.Text = FileUpload1.FileName;
                }

                int id = int.Parse(Request.QueryString["id"]);
                Movie movie = movieModel.getMovieByID(id);

                Movie upMovie = movieModel.EditMovie(movie,
                    movieTitle.Text,
                    shortDescription.Text,
                    longDescription.Text,
                    movieImage.Text,
                    double.Parse(price.Text),
                    1);
                Label1.Text = "";
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                Label1.Text = "Did not save!";
                Label1.Attributes.CssStyle.Add("color", "red");
            }
        }
    }
}