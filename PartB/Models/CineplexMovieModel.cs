using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartB.Models
{
    /// <summary>Struct of Session to link between cineplex and movie and
    /// day of the week and number of seats occupied.
    /// Session has many to 1 relation with Movie and Cineplex</summary>
    struct CineplexMovie
    {
        public int cineplexMovieId { get; set; }
        public int cineplexId { set; get; }
        public int movieId { set; get; }
    };
    class CineplexMovieModel
    {
        private const int DID_NOT_FIND_CINEPLEX_MOVIE_INDEX = -1;

        private List<CineplexMovie> cineplexMovies = new List<CineplexMovie>();
        private static CineplexMovieModel instance;

        /// <summary>Private constructor for the singleton pattern.
        /// It is set to private so we cannot instantiate the class
        /// with new.</summary>
        private CineplexMovieModel() { }

        /// <summary>Getter to get a single and same instance of Session Model.</summary>
        /// <returns>Returns a saved instance of Session Model.</returns>
        public static CineplexMovieModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CineplexMovieModel();
                }
                return instance;
            }
        }

        /// <summary>Getter to get a list of Session.</summary>
        /// <returns>Returns list of sessions.</returns>
        public List<CineplexMovie> CineplexMovie
        {
            get
            {
                return cineplexMovies;
            }
        }

        /// <summary>Method to add a session. If similar session then it will not add.</summary>
        /// <param name="cineplexId"> parameter takes a struct Cineplex.</param>
        /// <param name="movieId"> parameter takes a struct Movie.</param>
        /// <param name="dayOfWeek"> parameter takes a string from day of week.</param>
        /// <param name="seatsOccupied"> parameter takes an integer of seats occupied.</param>
        /// <returns>Returns session that has been added or found.</returns>
        public CineplexMovie AddCineplexMovie(int cineplexId, int movieId)
        {
            int sessionIndex = SearchCineplexMovieIndex(cineplexId, movieId);
            if (sessionIndex != DID_NOT_FIND_CINEPLEX_MOVIE_INDEX) return cineplexMovies[sessionIndex];

            CineplexMovie cineplexMovie = new CineplexMovie();
            cineplexMovie.cineplexId = cineplexId;
            cineplexMovie.movieId = movieId;

            cineplexMovies.Add(cineplexMovie);

            return cineplexMovie;
        }

        /// <summary>Iterates through the list of session to find if there is same 
        /// as parameters. Returns the index if found, otherwise -1 representing 
        /// did not find.</summary>
        /// <param name="cineplexId"> parameter takes a struct Cineplex.</param>
        /// <param name="movieId"> parameter takes a struct Movie.</param>
        /// <param name="dayOfWeek"> parameter takes a string from day of week.</param>
        /// <param name="seatsOccupied"> parameter takes an integer of seats occupied.</param>
        /// <returns>Returns index of session found or -1 representing not found.</returns>
        public int SearchCineplexMovieIndex(int cineplexId, int movieId)
        {
            for (int i = 0; i < cineplexMovies.Count; i++)
            {
                if (cineplexMovies[i].cineplexId.Equals(cineplexId) &&
                    cineplexMovies[i].movieId.Equals(movieId))
                    return i;
            }
            return DID_NOT_FIND_CINEPLEX_MOVIE_INDEX;
        }
        public CineplexMovie getCineplexMovieByID(int cineplexMovieID)
        {
            cineplexMovies = GetSeating();
            for (int i = 0; i < cineplexMovies.Count; i++)
            {
                if (cineplexMovies[i].cineplexMovieId.Equals(cineplexMovieID))
                    return cineplexMovies[i];
            }

            throw new CustomCouldntFindException("Could not find the movie: " + cineplexMovieID);
        }
        public List<CineplexMovie> GetSeating()
        {
            if (cineplexMovies.Count > 0)
            {
                return cineplexMovies;
            }

            SqlConnection conn = null;
            SqlCommand cmd = null;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "select * from [master].[dbo].[CineplexMovie]";
                    cmd = new SqlCommand(sql, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    cineplexMovies = new List<CineplexMovie>();
                    while (rdr.Read())
                    {
                        CineplexMovie cineplexMovie = new CineplexMovie();
                        cineplexMovie.cineplexMovieId = (int)rdr[0];
                        cineplexMovie.cineplexId = (int)rdr[1];
                        cineplexMovie.movieId = (int)rdr[2];

                        cineplexMovies.Add(cineplexMovie);
                    }
                    return cineplexMovies;
                }
                catch (Exception ex)
                {
                    return cineplexMovies;
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
    }
}
