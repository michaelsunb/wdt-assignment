using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private ArrayList movies = new ArrayList();
        public void AddMovie(int id, string cineplex, string title, DateTime dateTime)
        {
            Movie movie = new Movie();
            movie.id = id;
            movie.cineplex = cineplex;
            movie.title = title;
            movie.dateTime = dateTime;
            movies.Add(movie);
        }
        public void WriteJson()
        {
            string json = JsonConvert.SerializeObject(movies);
            string fileName = @"db.json";
            if (FileExists(fileName))
                File.Delete(fileName);
            File.WriteAllText(fileName, json);
        }
        public bool FileExists(String existingFilename)
        {
            return File.Exists(existingFilename);
        }
    }
}
