using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wdt_assignment.Model
{
    class MovieDetailModel
    {
        struct MovieDetails
        {
            public int seatsAvailable;
            public DateTime dateTime;
            public MovieModel.Movie movieId;
        };
        private List<MovieDetails> movieDetails = new List<MovieDetails>();
        public void AddDetails(int seatsAvailable, DateTime dateTime, MovieModel.Movie movieId)
        {
            MovieDetails movieDetail = new MovieDetails();
            movieDetail.seatsAvailable = seatsAvailable;
            movieDetail.dateTime = dateTime;
            movieDetail.movieId = movieId;

            movieDetails.Add(movieDetail);
        }
    }
}
