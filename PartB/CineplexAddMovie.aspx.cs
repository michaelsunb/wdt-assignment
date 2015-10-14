using PartB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartB
{
    public partial class CineplexAddMovie : System.Web.UI.Page
    {
        private const string REMOVE_COLUMN = " ";

        private static CineplexModel cineplexModel = CineplexModel.Instance;
        private static MovieModel movieModel = MovieModel.Instance;
        private static CineplexMovieModel cmModel = CineplexMovieModel.Instance;

        private static List<Movie> movies = null;
        private static int cineplexId = -1;
        private static int movieId = -1;

        System.Data.DataTable movieTable = new System.Data.DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                movies = movieModel.GetMovies();
                if (IsPostBack)
                {
                    createMovieDropDownList();
                    cineplexId = int.Parse(cineplexLocation.SelectedItem.Value);
                    movieId =int.Parse(movieTitle.SelectedItem.Value);
                }

                CreateMovieGrid();
                AddMovieRowsToGridById(cineplexId);
                MovieGridView1.DataSource = movieTable;
                MovieGridView1.DataBind();

                if (IsPostBack) return;

                cineplexLocation.DataSource = cineplexModel.GetCineplex();
                cineplexLocation.DataTextField = "Location";
                cineplexLocation.DataValueField = "CineplexID";
                cineplexLocation.DataBind();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.StackTrace;
                //Label1.Text = "Did not save!";
                Label1.Attributes.CssStyle.Add("color", "red");
            }
        }

        private void createMovieDropDownList()
        {
            foreach (Movie movie in cmModel.getMoviesByID(cineplexId))
            {
                for (int i = 0; i < movies.Count; i++)
                {
                    if (movies[i].Title.Equals(movie.Title))
                    {
                        movies.RemoveAt(i);
                    }
                }
            }

            movieTitle.DataSource = movies;
            movieTitle.DataTextField = "Title";
            movieTitle.DataValueField = "MovieID";
            movieTitle.DataBind();
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            try
            {
                cmModel.AddCineplexMovie(
                    int.Parse(cineplexLocation.SelectedItem.Value),
                    movieId);
                Response.Redirect("CineplexAddMovie.aspx",false);
            }
            catch (Exception ex)
            {
                Label1.Text = ex.StackTrace;
                //Label1.Text = "Did not save!";
                Label1.Attributes.CssStyle.Add("color", "red");
            }
        }

        private void CreateMovieGrid()
        {
            System.Data.DataColumn tColumn = null;

            tColumn = new System.Data.DataColumn("Movie ID", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Title", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Short Description", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Long Description", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Price ($)", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Image", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn(REMOVE_COLUMN, System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
        }
        private void AddMovieRowsToGridById(int id = -1)
        {
            if (id == -1) return;

            movies = cmModel.getMoviesByID(id);

            foreach (Movie movie in movies)
            {
                if (movie.status == 1)
                {
                    List<CineplexMovie> cmList = cmModel.GetCineplexMovie();
                    int cmIndex = cmModel.SearchCineplexMovieIndex(
                        cineplexId, movie.MovieID
                        );
                    movieTable.Rows.Add(
                        cmList[cmIndex].cineplexMovieId,
                        movie.Title,
                        movie.ShortDecription,
                        movie.LongDecription,
                        movie.price.ToString(),
                        movie.ImageUrl);
                }
            }
        }
        protected void MovieGridView_RowDataBound(object sender,
            System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton elb = new LinkButton();
                elb.Text = "Remove";
                elb.PostBackUrl =
                    "CineplexRemoveMovie.aspx?id=" + e.Row.Cells[0].Text;
                e.Row.Cells[6].Controls.Add(elb);
            }
        }
    }
}