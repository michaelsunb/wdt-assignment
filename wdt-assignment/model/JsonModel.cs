using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wdt_assignment.Model
{
    class JsonModel
    {
        private struct JsonMovie
        {
            public int id;
            public string title;
            public DateTime dateTime;
            public double price;
            public string cineplex;
            public int seatsAvailable;
            public int totalSeats;
        };
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
        public void AddMovie(int id, double price, string title, DateTime dateTime,
            string cineplex, int seatsAvailable, int totalSeats)
        {
            JsonMovie movie = new JsonMovie();
            movie.id = id;
            movie.title = title;
            movie.dateTime = dateTime;
            movie.price = price;
            movie.cineplex = cineplex;
            movie.seatsAvailable = seatsAvailable;
            movie.totalSeats = totalSeats;

            movies.Add(movie);
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
    }
}
