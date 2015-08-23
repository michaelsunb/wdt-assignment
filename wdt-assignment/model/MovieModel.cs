using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace wdt_assignment.model
{
    /// <summary>Struct of Movie to store title, price and time.
    /// Movie has 1 to many relation with Session.</summary>
    struct Movie
    {
        public string title;
        public double price;
        public string time;
    };

    class MovieModel
    {
        private const int DID_NOT_FIND_MOVIE_INDEX = -1;
        private List<Movie> movies = new List<Movie>();
        private static MovieModel instance;

        /// <summary>Private constructor for the singleton pattern.
        /// It is set to private so we cannot instantiate the class
        /// with new.</summary>
        private MovieModel() {}

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
                return movies;
            }
        }

        /// <summary>Method to add a movie. If similar movie then it will not add.</summary>
        /// <param name="title"> parameter takes a string for title.</param>
        /// <param name="price"> parameter takes a double for price.</param>
        /// <param name="time"> parameter takes a string for time.</param>
        /// <returns>Returns movie that has been added or found.</returns>
        public Movie AddMovie(string title, double price, string time)
        {
            int movieIndex = SearchMovieIndex(title, price, time);
            if (movieIndex != DID_NOT_FIND_MOVIE_INDEX) return movies[movieIndex];

            Movie movie = new Movie();
            movie.title = title;
            movie.price = price;
            movie.time = time;
            movies.Add(movie);

            return movie;
        }

        /// <summary>Iterates through the list of movies to find if there is same 
        /// as parameters. Returns the index if found, otherwise -1 representing 
        /// did not find.</summary>
        /// <param name="title"> parameter takes a string for title.</param>
        /// <param name="price"> parameter takes a double for price.</param>
        /// <param name="time"> parameter takes a string for time.</param>
        /// <returns>Returns index of movie found or -1 representing not found.</returns>
        public int SearchMovieIndex(string title, double price, string time)
        {
            for (int i = 0; i < movies.Count; i++)
            {
                if (movies[i].title.Equals(title) &&
                    movies[i].price.Equals(price) &&
                    movies[i].time.Equals(time))
                    return i;
            }
            return DID_NOT_FIND_MOVIE_INDEX;
        }
    }
}
