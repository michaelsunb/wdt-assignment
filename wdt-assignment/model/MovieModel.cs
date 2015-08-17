using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace wdt_assignment.Model
{
    class MovieModel
    {
        public struct Movie
        {
            public int id;
            public string title;
            public double price;
            public CinemaModel.Cineplex cineplex;
        };
        private List<Movie> movies = new List<Movie>();
        public List<Movie> GetMovies(int id)
        {
            return movies;
        }
        public void AddMovie(int id, double price, string title, DateTime dateTime, CinemaModel.Cineplex cineplex)
        {
            Movie movie = new Movie();
            movie.id = id;
            movie.title = title;
            movie.price = price;
            movie.cineplex = cineplex;
            movies.Add(movie);
        }
    }
}
