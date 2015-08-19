using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using wdt_assignment.model;
using System.Collections;

namespace wdt_assignment_testc
{
    [TestClass]
    public class TestJsonModel
    {
        private const string existingFilename = @"db.json";
        private const string newFilename = @"db2.json";
        private string[] dayOfWeek = new String[7] {
            "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"
        };

        public TestJsonModel()
        {
            CinemaModel cinemaModel = CinemaModel.GetInstance;

            Cineplex stKilda = cinemaModel.AddCinplex("St Kilda");
            Cineplex fitzroy = cinemaModel.AddCinplex("Fitzroy");
            Cineplex melbourneCBD = cinemaModel.AddCinplex("Melbourne CBD");
            Cineplex sunshine = cinemaModel.AddCinplex("Sunshine");
            Cineplex lilydale = cinemaModel.AddCinplex("Lilydale");

            MovieModel movieModel = MovieModel.GetInstance;
            Movie matrix = movieModel.AddMovie("The Matrix", 10, "8am");
            Movie matrixReloaded = movieModel.AddMovie("The Matrix Reloaded", 15, "10am");
            Movie matrixRevolution = movieModel.AddMovie("The Matrix Revolution", 20, "12pm");
            Movie fellowshipRing = movieModel.AddMovie("Fellowship of the Ring", 25, "2pm");
            Movie twoTowers = movieModel.AddMovie("The Two Towers", 30, "4pm");

            SessionModel sessionModel = SessionModel.GetInstance;
            foreach (Cineplex cineplex in cinemaModel.GetCineplex())
            {
                foreach(String day in dayOfWeek)
                {
                    sessionModel.AddSession(cineplex, matrix, day, 17);
                    sessionModel.AddSession(cineplex, matrixReloaded, day, 0);
                    sessionModel.AddSession(cineplex, matrixRevolution, day, 0);
                    sessionModel.AddSession(cineplex, fellowshipRing, day, 0);
                    sessionModel.AddSession(cineplex, twoTowers, day, 0);
                }
            }
        }

        [TestMethod]
        public void TestAddJsonMovieModel()
        {
            JsonModel jsonMovie = new JsonModel();

            SessionModel sessionModel = SessionModel.GetInstance;
            int i = 0;
            foreach (Sessions session in SessionModel.GetInstance.GetSessions())
            {
                jsonMovie.AddMovie(i,
                    session.cineplexId.cinemaName,
                    session.dayOfWeek,
                    session.movieId.time,
                    session.movieId.title,
                    session.movieId.price,
                    session.seatsAvailable,
                    session.cineplexId.totalSeats);
                i++;
            }

            jsonMovie.WriteJson(existingFilename);
            Assert.IsTrue(jsonMovie.FileExists(existingFilename));
        }

        [TestMethod]
        public void TestReadJsonModel()
        {
            JsonModel movie = new JsonModel();

            movie.ReadJson(existingFilename);
            movie.WriteJson(newFilename);

            Assert.IsTrue(movie.FileExists(newFilename));
        }

        [TestMethod]
        public void TestRemoveMovie()
        {
            if (!File.Exists(newFilename))
                TestReadJsonModel();

            JsonModel movie = new JsonModel();
            movie.ReadJson(@"db2.json");

            ArrayList movies = movie.GetMovies();
            Assert.AreEqual(175, movies.Count);
            movie.RemoveMovie(3);
            movie.RemoveMovie(2);
            movie.RemoveMovie(0);
            Assert.AreEqual(172, movies.Count);

            //File.Delete(existingFilename);
            File.Delete(newFilename);
        }
    }
}
