using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using wdt_assignment.Model;
using System.Collections;

namespace wdt_assignment_testc
{
    [TestClass]
    public class TestJsonModel : IDisposable
    {
        [TestMethod]
        public void TestAddJsonMovieModel()
        {
            JsonModel movie = new JsonModel();

            movie.AddMovie(0, 4.20, "title 1", new DateTime(), "St Kilda",10,20);
            movie.AddMovie(1, 6.66, "title 2", new DateTime(), "St Kilda",5,20);
            movie.AddMovie(2, 7.77, "title 3", new DateTime(), "St Kilda",0,20);
            movie.AddMovie(3, 4.25, "title 4", new DateTime(), "St Kilda",1,20);
            movie.AddMovie(4, 1.23, "title 5", new DateTime(), "St Kilda",15,20);
            movie.AddMovie(5, 2.43, "title 6", new DateTime(), "St Kilda",10,20);
            movie.AddMovie(6, 0.22, "title 7", new DateTime(), "St Kilda",10,20);
            string existingFilename = @"db.json";
            movie.WriteJson(existingFilename);

            Assert.IsTrue(movie.FileExists(existingFilename));
        }
        [TestMethod]
        public void TestReadJsonModel()
        {
            string existingFilename = @"db.json";
            if (!File.Exists(existingFilename))
                TestAddJsonMovieModel();

            JsonModel movie = new JsonModel();

            movie.ReadJson(existingFilename);

            movie.AddMovie(7, 7.77, "title 8", new DateTime(), "St 7",10,20);
            movie.AddMovie(8, 8.88, "title 9", new DateTime(), "St 8",8,20);
            movie.AddMovie(9, 9.99, "title 10", new DateTime(), "St 9",9,20);

            string newFilename = @"db2.json";
            movie.WriteJson(newFilename);

            Assert.IsTrue(movie.FileExists(newFilename));
        }
        private static void DeleteJsonFiles(string existingFilename, string newFilename)
        {
            if (File.Exists(existingFilename))
                File.Delete(existingFilename);
            if (File.Exists(newFilename))
                File.Delete(newFilename);
        }
        [TestMethod]
        public void TestRemoveMovie()
        {
            if (!File.Exists(@"db2.json"))
                TestReadJsonModel();

            JsonModel movie = new JsonModel();
            movie.ReadJson(@"db2.json");

            ArrayList movies = movie.GetMovies();
            Assert.AreEqual(10, movies.Count);
            movie.RemoveMovie(3);
            movie.RemoveMovie(2);
            movie.RemoveMovie(0);
            Assert.AreEqual(7, movies.Count);
        }
        public void Dispose()
        {
            DeleteJsonFiles(@"db.json", @"db2.json");
        }
    }
}
