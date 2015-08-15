using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace wdt_assignment_testc
{
    [TestClass]
    public class TestMovieModel
    {
        [TestMethod]
        public void TestAddMovieModel()
        {
            wdt_assignment.Model.MovieModel movie = new wdt_assignment.Model.MovieModel();
            movie.AddMovie(0, "St Kilda", "title 1", new DateTime());
            movie.AddMovie(1, "St1", "title 2", new DateTime());
            movie.AddMovie(2, "St 2", "title 3", new DateTime());
            movie.AddMovie(3, "St 3", "title 4", new DateTime());
            movie.AddMovie(4, "St 4", "title 5", new DateTime());
            movie.AddMovie(5, "St 5", "title 6", new DateTime());
            movie.AddMovie(6, "St 6", "title 7", new DateTime());

            string existingFilename = @"db.json";
            movie.WriteJson(existingFilename);

            Assert.IsTrue(movie.FileExists(existingFilename));
        }
        [TestMethod]
        public void TestReadModelModel()
        {
            string existingFilename = @"db.json";
            if (!File.Exists(existingFilename))
                TestAddMovieModel();

            wdt_assignment.Model.MovieModel movie = new wdt_assignment.Model.MovieModel();

            movie.ReadJson(existingFilename);
            
            movie.AddMovie(7, "St 7", "title 8", new DateTime());
            movie.AddMovie(8, "St 8", "title 9", new DateTime());
            movie.AddMovie(9, "St 9", "title 10", new DateTime());
            
            string newFilename = @"db2.json";
            movie.WriteJson(newFilename);

            Assert.IsTrue(movie.FileExists(newFilename));

            if (movie.FileExists(existingFilename))
                File.Delete(existingFilename);
            if (movie.FileExists(newFilename))
                File.Delete(newFilename);
        }
    }
}
