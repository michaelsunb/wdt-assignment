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
        public string time;
    };
    class MovieModel
    {
        private List<Movie> movies = new List<Movie>();
        private static MovieModel instance;

        private MovieModel() {}

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

        public List<Movie> Movies
        {
            get
            {
                return movies;
            }
        }

        public Movie AddMovie(string title, double price, string time)
        {
            int movieIndex = SearchMovieIndex(title, price, time);
            if (movieIndex != -1) return movies[movieIndex];

            Movie movie = new Movie();
            movie.title = title;
            movie.price = price;
            movie.time = time;
            movies.Add(movie);

            return movie;
        }

        public int SearchMovieIndex(string title, double price, string time)
        {
            for (int i = 0; i < movies.Count; i++)
            {
                if (movies[i].title.Equals(title) &&
                    movies[i].price.Equals(price) &&
                    movies[i].time.Equals(time))
                    return i;
            }
            return -1;
        }

        public List<Movie> SearchMovie(string title)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(title.ToLower());
            if (movies.Exists(x => regEx.IsMatch(x.title.ToLower())))
                return movies.Where(s => regEx.IsMatch(s.title.ToLower())).ToList();

            throw new CustomCouldntFindException("Could not find " + title);
        }
    }
}
