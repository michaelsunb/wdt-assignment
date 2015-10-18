using PartB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartB
{
    public partial class Booking : System.Web.UI.Page
    {
        private const string REMOVE_COLUMN = " ";

        private CineplexModel cineplexModel = CineplexModel.Instance;
        private MovieModel movieModel = MovieModel.Instance;
        private CineplexMovieModel cmModel = CineplexMovieModel.Instance;

        private IList<Movie> movies = null;
        private int cineplexId = -1;
        private int movieId = -1;

        public IList<Seating> seatRowColumnRows = new List<Seating>();
        System.Data.DataTable movieTable = new System.Data.DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                movies = movieModel.GetMovies();
                if (!IsPostBack) return;

                createMovieDropDownList();
                cineplexId = int.Parse(cineplexLocation.SelectedItem.Value);
                movieId =int.Parse(movieTitle.SelectedItem.Value);

                try
                {
                    seatRowColumnRows = new List<Seating>();
                    foreach (CineplexMovie cm in cmModel.getCineplexMovieIDs(cineplexId, movieId))
                    {
                        seatRowColumnRows.Add(SeatingModel.Instance.getSeats(cm));
                    }
                }
                catch (CustomCouldntFindException ccfe)
                {
                    seatRowColumnRows = new List<Seating>();
                }

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

    }
}