using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wdt_assignment.model
{
    class JsonModel
    {
        private ArrayList movies = new ArrayList();

        public ArrayList GetMovies()
        {
            return movies;
        }

        public void ReadJson(string fileName = @"db.json")
        {
            var filestream = new FileStream(fileName,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                movies = JsonConvert.DeserializeObject<ArrayList>(json);
            }
            file.Close();
        }

        public void WriteJson(string fileName = @"db.json")
        {
            string json = JsonConvert.SerializeObject(movies);
            if (FileExists(fileName))
                File.Delete(fileName);
            File.WriteAllText(fileName, json);
        }

        public bool FileExists(string fileName = @"db.json")
        {
            return File.Exists(fileName);
        }

        public void AddMovie(int id, string cineplex, string dayOfWeek, string time,
            string title, double price, int seatsAvailable, int totalSeats)
        {
            JObject movie = new JObject();
            movie.Add("id", id);
            movie.Add("title", title);
            movie.Add("dayOfWeek", dayOfWeek);
            movie.Add("time", time);
            movie.Add("price", price);
            movie.Add("cineplex", cineplex);
            movie.Add("seatsAvailable", seatsAvailable);
            movie.Add("totalSeats", totalSeats);
            movies.Add(movie);

            SetToModels(cineplex, time, dayOfWeek, title, price, seatsAvailable, totalSeats);
        }

        public void RemoveMovie(int id)
        {
            if (movies.Count <= 0) return;

            foreach (var value in movies)
            {
                JToken token = JObject.Parse(JsonConvert.SerializeObject(value));
                int JsonId = (int)token.SelectToken("id");
                if (JsonId == id)
                {
                    movies.Remove(value);
                    return;
                }
            }
        }

        public void RemoveMovie(CinemaModel cinemaModel, MovieModel movieModel, MovieDetailModel movieDetail)
        {
        }

        public void LoadJsonDetails()
        {
            foreach (var value in movies)
            {
                JToken token = JObject.Parse(JsonConvert.SerializeObject(value));
                string cineplex = (string)token.SelectToken("cineplex");
                string time = (string)token.SelectToken("time");
                string dayOfWeek = (string)token.SelectToken("dayOfWeek");
                string title = (string)token.SelectToken("title");
                double price = (double)token.SelectToken("price");
                int seatsAvailable = (int)token.SelectToken("seatsAvailable");
                int totalSeats = (int)token.SelectToken("totalSeats");

                SetToModels(cineplex, time, dayOfWeek, title, price, seatsAvailable, totalSeats);
            }
        }

        private void SetToModels(string cineplex, string time, string dayOfWeek, string title, double price, int seatsAvailable, int totalSeats)
        {
            CinemaModel cinemaModel = CinemaModel.GetInstance;
            Cineplex cinema = cinemaModel.AddCinplex(cineplex, totalSeats);

            MovieModel movieModel = MovieModel.GetInstance;
            Movie movie = movieModel.AddMovie(title, price, cinema);

            MovieDetailModel movieDetail = MovieDetailModel.GetInstance;
            movieDetail.AddDetails(seatsAvailable, dayOfWeek, time, movie);
        }
    }
}
