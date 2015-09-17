using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using wdt_assignment.model;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace wdt_assignment_testc
{
    [TestClass]
    public class TestJsonModel
    {
        public const string FILE_NAME = @"data.json";
        private const string NEW_FILE_NAME = @"data2.json";
        public string[] days = new String[7] {
            "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"
        };

        public TestJsonModel()
        {
            CineplexModel cinemaModel = CineplexModel.Instance;

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
                foreach(String day in days)
                {
                    if(sessionModel.Sessions.Count == 0)
                        sessionModel.AddSession(cineplex, matrix, day, 17);
                    else
                        sessionModel.AddSession(cineplex, matrix, day, 10);

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
            foreach (Session session in SessionModel.Instance.Sessions)
            {
                jsonMovie.AddEntireSessionInfo(i,
                    session.cineplexId.cineplexName,
                    session.dayOfWeek,
                    session.movieId.time,
                    session.movieId.title,
                    session.movieId.price,
                    session.seatsOccupied,
                    session.cineplexId.totalSeats);
                i++;
            }

            jsonMovie.WriteJson(FILE_NAME);
            Assert.IsTrue(File.Exists(FILE_NAME), "'data.json' should exits in /wdt-assignment/bin/Debug/");
        }

        [TestMethod]
        public void TestReadJsonModel()
        {
            JsonModel movie = new JsonModel();

            movie.ReadJson(FILE_NAME);
            movie.WriteJson(NEW_FILE_NAME);

            Assert.IsTrue(File.Exists(NEW_FILE_NAME), "'data2.json' should exits in /wdt-assignment/bin/Debug/");
        }

        [TestMethod]
        public void TestRemoveMovie()
        {
            if (!File.Exists(FILE_NAME))
                TestReadJsonModel();

            JsonModel movie = new JsonModel();
            movie.ReadJson(NEW_FILE_NAME);

            List<JObject> movies = movie.Movies;
            Assert.AreEqual(175, movies.Count, "Should be 175 results");
            movie.RemoveMovie(3);
            movie.RemoveMovie(2);
            movie.RemoveMovie(0);
            Assert.AreEqual(172, movies.Count, "Results now should be 172");

            //File.Delete(existingFilename);
            File.Delete(NEW_FILE_NAME);
        }
    }
}
