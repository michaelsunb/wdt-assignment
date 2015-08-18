using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using wdt_assignment.model;
using System.Collections;

namespace wdt_assignment_testc
{
    [TestClass]
    public class TestJsonModel : IDisposable
    {
        const string existingFilename = @"db.json";
        const string newFilename = @"db2.json";
        [TestMethod]
        public void TestAddJsonMovieModel()
        {
            JsonModel movie = new JsonModel();

            movie.AddMovie(0, "St Kilda", "Monday", "8am", "The Matrix", 4.20, 10, 20);
            movie.AddMovie(1, "St Kilda", "Monday", "10am", "The Matrix Reloaded", 6.66, 5, 20);
            movie.AddMovie(2, "St Kilda", "Monday", "12pm", "The Matrix Revolution", 7.77, 0, 20);
            movie.AddMovie(3, "St Kilda", "Monday", "2pm", "Fellowship of the Ring", 4.25, 1, 20);
            movie.AddMovie(4, "St Kilda", "Monday", "4pm", "The Two Towers", 1.23, 15, 20);
            movie.AddMovie(5, "St Kilda", "Tuesday", "8am", "The Matrix", 2.43, 10, 20);
            movie.AddMovie(6, "St Kilda", "Tuesday", "10am", "The Matrix Reloaded", 0.22, 10, 20);

            movie.WriteJson(existingFilename);

            Assert.IsTrue(movie.FileExists(existingFilename));
        }
        [TestMethod]
        public void TestReadJsonModel()
        {
            if (!File.Exists(existingFilename))
                TestAddJsonMovieModel();

            JsonModel movie = new JsonModel();

            movie.ReadJson(existingFilename);

            movie.AddMovie(7, "St 7", "Tuesday", "12pm", "The Matrix Revolution", 7.77, 10, 20);
            movie.AddMovie(8, "St 8", "Tuesday", "2pm", "Fellowship of the Ring", 8.88, 8, 20);
            movie.AddMovie(9, "St 9", "Tuesday", "4pm", "The Two Towers", 9.99, 9, 20);

            movie.WriteJson(newFilename);

            Assert.IsTrue(movie.FileExists(newFilename));
        }
        private static void DeleteJsonFiles()
        {
            if (File.Exists(existingFilename))
            {
                File.Delete(existingFilename);
            }
            if (File.Exists(newFilename))
            {
                File.Delete(newFilename);
            }
        }
        [TestMethod]
        public void TestRemoveMovie()
        {
            if (!File.Exists(newFilename))
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
            //DeleteJsonFiles();
        }
    }
}
