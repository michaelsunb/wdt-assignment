using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using wdt_assignment_testc;

namespace wdt_assignment.model
{
    class JsonModel
    {
        private List<JObject> movies = new List<JObject>();

        /// <summary>Getter to get an arraylist of movies with all the info.</summary>
        /// <returns>Returns arraylist of movies.</returns>
        public List<JObject> Movies
        {
            get
            {
                return movies;
            }
        }

        /// <summary>Read from a json file and set the values into an arraylist.
        /// Default file name is called data.json</summary>
        /// <param name="fileName"> parameter takes a file name.
        /// Default file name is data.json.</param>
        public void ReadJson(string fileName = TestJsonModel.FILE_NAME)
        {
            var filestream = new FileStream(fileName,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<JObject>>(json);
            }
            file.Close();
        }

        /// <summary>Read from a json file and set the values into an arraylist.
        /// Default file name is called data.json</summary>
        /// <param name="fileName"> parameter takes a file name.
        /// Default file name is data.json.</param>
        public void WriteJson(string fileName)
        {
            string json = JsonConvert.SerializeObject(movies);
            if (File.Exists(fileName))
                File.Delete(fileName);
            File.WriteAllText(fileName, json);
        }

        /// <summary>Adds parameters listed below into JObject to be added
        /// into the arraylist.</summary>
        /// <param name="id"> parameter takes id of int.</param>
        /// <param name="cineplex"> parameter takes a name of cineplex.</param>
        /// <param name="dayOfWeek"> parameter takes a string from day of week.</param>
        /// <param name="time"> parameter takes a string for time.</param>
        /// <param name="title"> parameter takes a string for title.</param>
        /// <param name="price"> parameter takes a double for price.</param>
        /// <param name="seatsOccupied"> parameter takes an integer of seats occupied.</param>
        /// <param name="totalSeats"> parameter takes an integer of total number of seats allowed.</param>
        public void AddEntireSessionInfo(int id, string cineplex, string dayOfWeek, string time,
            string title, double price, int seatsOccupied, int totalSeats)
        {
            JObject movie = new JObject();
            movie.Add("id", id);
            movie.Add("title", title);
            movie.Add("dayOfWeek", dayOfWeek);
            movie.Add("time", time);
            movie.Add("price", price);
            movie.Add("cineplex", cineplex);
            movie.Add("seatsAvailable", seatsOccupied);
            movie.Add("totalSeats", totalSeats);

            movies.Add(movie);
        }

        /// <summary>Uses the id input for find and remove an element from movies.</summary>
        /// <param name="id"> parameter takes id of int.</param>
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

        /// <summary>Method to read from json file and set them into the models.</summary>
        public void LoadJsonDetails()
        {
            if (movies.Count <= 0)
                ReadJson();

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

        /// <summary>Sets parameters to their corresponding models</summary>
        /// <param name="cineplex"> parameter takes a name of cineplex.</param>
        /// <param name="dayOfWeek"> parameter takes a string from day of week.</param>
        /// <param name="time"> parameter takes a string for time.</param>
        /// <param name="title"> parameter takes a string for title.</param>
        /// <param name="price"> parameter takes a double for price.</param>
        /// <param name="seatsOccupied"> parameter takes an integer of seats occupied.</param>
        /// <param name="totalSeats"> parameter takes an integer of total number of seats allowed.</param>
        private void SetToModels(string cineplex, string time, string dayOfWeek, string title, double price, int seatsAvailable, int totalSeats)
        {
            CineplexModel cinemaModel = CineplexModel.Instance;
            Cineplex cinema = cinemaModel.AddCinplex(cineplex, totalSeats);

            MovieModel movieModel = MovieModel.Instance;
            Movie movie = movieModel.AddMovie(title, price, time);

            SessionModel sessionModel = SessionModel.Instance;
            sessionModel.AddSession(cinema, movie, dayOfWeek, seatsAvailable);
        }
    }
}
