using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace wdt_assignment.Model
{
    class MovieModel
    {
        private struct Movie
        {
            public int id;
            public string cineplex;
            public string title;
            public DateTime dateTime;
        };
        private List<Movie> movies = new List<Movie>();
        public void RemoveMovie(int id)
        {
            if (movies.Count() <= 0) return;

            foreach (Movie value in movies)
            {
                if (value.id == id)
                    movies.Remove(value);
            }
        }
        public void AddMovie(int id, string cineplex, string title, DateTime dateTime)
        {
            Movie movie = new Movie();
            movie.id = id;
            movie.cineplex = cineplex;
            movie.title = title;
            movie.dateTime = dateTime;
            movies.Add(movie);
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
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
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
    }
}
