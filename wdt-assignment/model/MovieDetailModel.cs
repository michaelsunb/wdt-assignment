using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wdt_assignment.model
{
    struct MovieDetails
    {
        public int seatsAvailable;
        public string dayOfWeek;
        public string time;
        public Movie movieId;
    };
    class MovieDetailModel
    {
        private static MovieDetailModel instance;
        private List<MovieDetails> movieDetails = new List<MovieDetails>();

        private MovieDetailModel() {}

        public static MovieDetailModel GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MovieDetailModel();
                }
                return instance;
            }
        }

        public MovieDetails AddDetails(int seatsAvailable,
            string dayOfWeek,
            string time,
            Movie movieId)
        {
            int movieDetailIndex = SearchMovieDetailIndex(movieId);
            if (movieDetailIndex != -1) return movieDetails[movieDetailIndex];

            MovieDetails movieDetail = new MovieDetails();
            movieDetail.seatsAvailable = seatsAvailable;
            movieDetail.dayOfWeek = dayOfWeek;
            movieDetail.time = time;
            movieDetail.movieId = movieId;
            movieDetails.Add(movieDetail);

            return movieDetail;
        }

        public int SearchMovieDetailIndex(Movie movieId)
        {
            for (int i = 0; i < movieDetails.Count; i++)
            {
                if (movieDetails[i].movieId.Equals(movieId))
                    return i;
            }
            return -1;
        }
    }
}
