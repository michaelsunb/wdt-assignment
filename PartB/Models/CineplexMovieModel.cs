using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartB.Models
{
    /// <summary>Struct of Session to link between cineplex and movie and
    /// day of the week and number of seats occupied.
    /// Session has many to 1 relation with Movie and Cineplex</summary>
    public struct CineplexMovie
    {
        public int cineplexMovieId { get; set; }
        public int cineplexId { set; get; }
        public int movieId { set; get; }
    };
    class CineplexMovieModel
    {
        private static string CONNECTION_STRING =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private const int DID_NOT_FIND_CINEPLEX_MOVIE_INDEX = -1;

        private IList<CineplexMovie> cineplexMovies = new List<CineplexMovie>();
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
        public IList<CineplexMovie> CineplexMovie
        {
            get
            {
                return cineplexMovies;
            }
        }

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
        public IList<CineplexMovie> getCineplexMovieIDs(int cineplexID, int movieID)
        {
            IList<CineplexMovie> cineplexMovie = new List<CineplexMovie>();
            cineplexMovies = GetCineplexMovie();
            for (int i = 0; i < cineplexMovies.Count; i++)
            {
                if (cineplexMovies[i].cineplexId.Equals(cineplexID) &&
                    cineplexMovies[i].movieId.Equals(movieID))
                    cineplexMovie.Add(cineplexMovies[i]);
            }

            return cineplexMovie;
        }
        public IList<Movie> getMoviesByID(int cineplexID)
        {
            IList<Movie> movies = new List<Movie>();
            MovieModel movieModel = MovieModel.Instance;
            movieModel.GetMovies();
            cineplexMovies = GetCineplexMovie();
            for (int i = 0; i < cineplexMovies.Count; i++)
            {
                if (cineplexMovies[i].cineplexId.Equals(cineplexID))
                    movies.Add(movieModel.getMovieByID(cineplexMovies[i].movieId));
            }

            return movies;
        }
        public CineplexMovie getCineplexMovieByMovieID(int cineplexMovieID)
        {
            cineplexMovies = GetCineplexMovie();
            for (int i = 0; i < cineplexMovies.Count; i++)
            {
                if (cineplexMovies[i].cineplexMovieId.Equals(cineplexMovieID))
                    return cineplexMovies[i];
            }

            throw new CustomCouldntFindException("Could not find the movie: " + cineplexMovieID);
        }
        public IList<CineplexMovie> GetCineplexMovie()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            
            using (conn = new SqlConnection(CONNECTION_STRING))
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
                        cineplexMovie.cineplexMovieId = (int)rdr["CineplexMovieID"];
                        cineplexMovie.cineplexId = (int)rdr["CineplexID"];
                        cineplexMovie.movieId = (int)rdr["MovieID"];

                        cineplexMovies.Add(cineplexMovie);
                    }
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
        /// <summary>Method to add a session. If similar session then it will not add.</summary>
        /// <param name="cineplexId"> parameter takes a struct Cineplex.</param>
        /// <param name="movieId"> parameter takes a struct Movie.</param>
        /// <param name="dayOfWeek"> parameter takes a string from day of week.</param>
        /// <param name="seatsOccupied"> parameter takes an integer of seats occupied.</param>
        /// <returns>Returns session that has been added or found.</returns>
        public CineplexMovie AddCineplexMovie(int cineplexId, int movieId)
        {
            int cineplexMovieIndex = SearchCineplexMovieIndex(cineplexId, movieId);
            if (cineplexMovieIndex != DID_NOT_FIND_CINEPLEX_MOVIE_INDEX)
                return cineplexMovies[cineplexMovieIndex];

            cineplexMovieIndex = InsertGetId(cineplexId, movieId);

            CineplexMovie cineplexMovie = new CineplexMovie();
            cineplexMovie.cineplexMovieId = cineplexMovieIndex;
            cineplexMovie.cineplexId = cineplexId;
            cineplexMovie.movieId = movieId;

            cineplexMovies.Add(cineplexMovie);

            return cineplexMovie;
        }
        private int InsertGetId(int cineplexId, int movieId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            using (conn = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO [master].[dbo].[CineplexMovie]" +
                        "([CineplexID],[MovieID]) VALUES " +
                        "(@CineplexID,@MovieID)";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@CineplexID", SqlDbType.Int).Value = cineplexId;
                    cmd.Parameters.Add("@MovieID", SqlDbType.Int).Value = movieId;

                    return Convert.ToInt32(cmd.ExecuteScalar());
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
        public CineplexMovie EditCineplexMovie(CineplexMovie oriCineplexMovie,
            int cineplexId, int movieId)
        {
            int cineplexMovieIndex = SearchCineplexMovieIndex(
                oriCineplexMovie.cineplexId, oriCineplexMovie.movieId);
            if (cineplexMovieIndex == DID_NOT_FIND_CINEPLEX_MOVIE_INDEX)
                throw new CustomCouldntFindException("Failed to add coming soon movie!");

            Update(oriCineplexMovie.cineplexMovieId, cineplexId, movieId);

            CineplexMovie cineplexMovie = new CineplexMovie();
            cineplexMovie.cineplexMovieId = oriCineplexMovie.cineplexMovieId;
            cineplexMovie.cineplexId = cineplexId;
            cineplexMovie.movieId = movieId;

            cineplexMovies[cineplexMovieIndex] = cineplexMovie;

            return cineplexMovie;
        }
        private void Update(int cineplexMovieID, int CineplexID, int MovieID)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            using (conn = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE [master].[dbo].[CineplexMovie] SET " +
                        "CineplexID = @CineplexID," +
                        "MovieID = @MovieID " +
                        "WHERE CineplexMovieID = @CineplexMovieID";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@CineplexID", SqlDbType.Int).Value = CineplexID;
                    cmd.Parameters.Add("@MovieID", SqlDbType.Int).Value = MovieID;

                    cmd.Parameters.Add("@CineplexMovieID", SqlDbType.Int).Value = cineplexMovieID;

                    cmd.ExecuteNonQuery();
                    return;
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
        public void RemoveMovie(int cineplexMovieID)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            using (conn = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    GetCineplexMovie();
                    CineplexMovie toBeRemoved =
                        getCineplexMovieByMovieID(cineplexMovieID);
                    for (int i = 0; i < cineplexMovies.Count; i++)
                    {
                        if (cineplexMovies[i].Equals(toBeRemoved))
                        {
                            cineplexMovies.RemoveAt(i);
                        }
                    }

                    conn.Open();
                    string sql = "DELETE FROM [dbo].[CineplexMovie] " +
                        "WHERE CineplexMovieID = @CineplexMovieID";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@CineplexMovieID", SqlDbType.Int).Value = cineplexMovieID;
                    SqlDataReader rdr = cmd.ExecuteReader();
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
