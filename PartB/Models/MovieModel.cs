using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PartB.Models
{
    struct Movie
    {
        public int MovieID {set; get;}
        public string Title {set; get;}
        public string ShortDecription {set; get;}
        public string LongDecription {set; get;}
        public string ImageUrl {set; get;}
        public double price {set; get;}
    }
    public class MovieModel
    {
        private const int DID_NOT_FIND_MOVIE_INDEX = -1;
        private List<Movie> movies = new List<Movie>();
        private static MovieModel instance;

        /// <summary>Private constructor for the singleton pattern.
        /// It is set to private so we cannot instantiate the class
        /// with new.</summary>
        private MovieModel() { }

        /// <summary>Getter to get a single and same instance of Movie Model.</summary>
        /// <returns>Returns a saved instance of Movie Model.</returns>
        public static MovieModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MovieModel();
                }
                return instance;
            }
        }
        /// <summary>Getter to get a list of Movie.</summary>
        /// <returns>Returns list of movies.</returns>
        public List<Movie> Movies
        {
            get
            {
                return GetMovies();
            }
        }
        public List<Movie> GetMovies()
        {
            if (movies.Count > 0)
            {
                return movies;
            }

            SqlConnection conn = null;
            SqlCommand cmd = null;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "select * from [master].[dbo].[Movie]";
                    cmd = new SqlCommand(sql, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    movies = new List<Movie>();
                    while(rdr.Read())
                    {
                        Movie movie = new Movie();
                        movie.MovieID = (int)rdr[0];
                        movie.Title = (string)rdr[1];
                        movie.ShortDecription = (string)rdr[2];
                        movie.LongDecription = (string)rdr[3];
                        movie.ImageUrl = (string)rdr[3];
                        movie.price = (double)rdr[4];

                        movies.Add(movie);
                    }
                    return movies;
                }
                catch (Exception ex)
                {
                    return movies;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                    if (conn != null)
                    {
                        conn.Dispose();
                    }
                }
            }
        }
        public Movie getMovieByID(int movieID)
        {
            movies = GetMovies();
            for (int i = 0; i < movies.Count; i++)
            {
                if (movies[i].MovieID.Equals(movieID))
                    return movies[i];
            }

            throw new CustomCouldntFindException("Could not find the movie: " + movieID);
        }
        public Movie AddMovie(string title, string shortDescription, string longDescription,
            string imageUrl, double price)
        {
            int movieIndex = SearchMovieIndex(title,shortDescription,longDescription,imageUrl,price);
            if (movieIndex != DID_NOT_FIND_MOVIE_INDEX) return movies[movieIndex];

            Movie movie = new Movie();
            movie.Title = title;
            movie.ShortDecription = shortDescription;
            movie.LongDecription = longDescription;
            movie.ImageUrl = imageUrl;
            movie.price = price;

            movies.Add(movie);

            return movie;
        }
        public int SearchMovieIndex(string title, string shortDescription, string longDescription,
            string imageUrl, double price)
        {
            for (int i = 0; i < movies.Count; i++)
            {
                if (movies[i].Title.Equals(title) &&
                    movies[i].ShortDecription.Equals(shortDescription) &&
                    movies[i].LongDecription.Equals(longDescription) &&
                    movies[i].ImageUrl.Equals(imageUrl) &&
                    movies[i].price.Equals(price))
                    return i;
            }
            return DID_NOT_FIND_MOVIE_INDEX;
        }
    }
}