using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace wdt_assignment.model
{
    struct Movie
    {
        public string title;
        public double price;
        public Cineplex cineplex;
    };
    class MovieModel
    {
        private List<Movie> movies = new List<Movie>();
        private static MovieModel instance;

        private MovieModel() {}

        public static MovieModel GetInstance
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

        public List<Movie> GetMovies()
        {
            return movies;
        }

        public Movie AddMovie(string title, double price, Cineplex cineplex)
        {
            int movieIndex = SearchMovieIndex(title);
            if (movieIndex != -1) return movies[movieIndex];

            Movie movie = new Movie();
            movie.title = title;
            movie.price = price;
            movie.cineplex = cineplex;
            movies.Add(movie);

            return movie;
        }

        public int SearchMovieIndex(string title)
        {
            for (int i = 0; i < movies.Count; i++)
            {
                if (movies[i].title.Equals(title))
                    return i;
            }
            return -1;
        }
    }
}
