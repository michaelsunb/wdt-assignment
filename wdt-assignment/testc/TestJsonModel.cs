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
        public const string FILE_NAME = @"data.json";
        private const string NEW_FILE_NAME = @"data2.json";
        private string[] dayOfWeek = new String[7] {
            "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"
        };

        public TestJsonModel()
        {
            CinemaModel cinemaModel = CinemaModel.Instance;

            Cineplex stKilda = cinemaModel.AddCinplex("St Kilda");
            Cineplex fitzroy = cinemaModel.AddCinplex("Fitzroy");
            Cineplex melbourneCBD = cinemaModel.AddCinplex("Melbourne CBD");
            Cineplex sunshine = cinemaModel.AddCinplex("Sunshine");
            Cineplex lilydale = cinemaModel.AddCinplex("Lilydale");

            MovieModel movieModel = MovieModel.Instance;
            Movie matrix = movieModel.AddMovie("The Matrix", 10, "8am");
            Movie matrixReloaded = movieModel.AddMovie("The Matrix Reloaded", 15, "10am");
            Movie matrixRevolution = movieModel.AddMovie("The Matrix Revolution", 20, "12pm");
            Movie fellowshipRing = movieModel.AddMovie("Fellowship of the Ring", 25, "2pm");
            Movie twoTowers = movieModel.AddMovie("The Two Towers", 30, "4pm");

            SessionModel sessionModel = SessionModel.Instance;
            foreach (Cineplex cineplex in cinemaModel.Cineplex)
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

            SessionModel sessionModel = SessionModel.Instance;
            int i = 0;
            foreach (Sessions session in SessionModel.Instance.Sessions)
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

            jsonMovie.WriteJson(FILE_NAME);
            Assert.IsTrue(jsonMovie.FileExists(FILE_NAME));
        }

        [TestMethod]
        public void TestReadJsonModel()
        {
            JsonModel movie = new JsonModel();

            movie.ReadJson(FILE_NAME);
            movie.WriteJson(NEW_FILE_NAME);

            Assert.IsTrue(movie.FileExists(NEW_FILE_NAME));
        }

        [TestMethod]
        public void TestRemoveMovie()
        {
            if (!File.Exists(FILE_NAME))
                TestReadJsonModel();

            JsonModel movie = new JsonModel();
            movie.ReadJson(NEW_FILE_NAME);

            ArrayList movies = movie.Movies;
            Assert.AreEqual(175, movies.Count);
            movie.RemoveMovie(3);
            movie.RemoveMovie(2);
            movie.RemoveMovie(0);
            Assert.AreEqual(172, movies.Count);

            //File.Delete(existingFilename);
            File.Delete(NEW_FILE_NAME);
        }
    }
}
