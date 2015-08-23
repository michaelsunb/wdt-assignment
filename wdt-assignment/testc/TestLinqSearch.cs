using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using wdt_assignment.model;
using System.Collections.Generic;
using wdt_assignment;

namespace wdt_assignment_testc
{
    [TestClass]
    public class TestLinqSearch
    {
        [TestMethod]
        public void TestLoadJson()
        {
            JsonModel jsonMovie = new JsonModel();
            jsonMovie.LoadJsonDetails();

            CineplexModel cinemaModel = CineplexModel.Instance;
            Assert.IsTrue((cinemaModel.Cineplex.Count > 0), "Should be more than zero results");
            Assert.AreEqual(cinemaModel.Cineplex.Count, 5, "From json there should 5 results");

            MovieModel movieModel = MovieModel.Instance;
            Assert.IsTrue((movieModel.Movies.Count > 0),"Should be more than zero results");
            Assert.AreEqual(movieModel.Movies.Count, 5,"From json there should 5 results");
        }

        [TestMethod]
        public void TestCineplexSearch()
        {
            CineplexModel cinemaModel = CineplexModel.Instance;
            IEnumerable<Cineplex> cineplexs = cinemaModel.SearchCinplex("St Kilda");

            System.Text.RegularExpressions.Regex regEx1 = new System.Text.RegularExpressions.Regex("Kilda".ToLower());
            System.Text.RegularExpressions.Regex regEx2 = new System.Text.RegularExpressions.Regex("The Matrix".ToLower());

            // Test Cineplex if true
            foreach(Cineplex c in cineplexs)
                Assert.IsTrue(regEx1.IsMatch(c.cineplexName.ToLower()), "Results should match any cinema with 'Kilda'");
            // Test Cineplex if false
            foreach (Cineplex c in cineplexs)
                Assert.IsFalse(regEx2.IsMatch(c.cineplexName.ToLower()), "Should not find any matches from 'Matrix'");
        }

        [TestMethod]
        public void TestSessionSearch()
        {
            SessionModel sessionModel = SessionModel.Instance;
            IEnumerable<Session> movies = sessionModel.SearchMovie("Matrix");

            System.Text.RegularExpressions.Regex regEx1 = new System.Text.RegularExpressions.Regex("Kilda".ToLower());
            System.Text.RegularExpressions.Regex regEx2 = new System.Text.RegularExpressions.Regex("Matrix".ToLower());

            // Test Movie if false
            foreach (Session m in movies)
                Assert.IsFalse(regEx1.IsMatch(m.movieId.title.ToLower()), "Should not find any matches from 'Kilda'");

            // Test Movie if true
            int sessionCount = 0;
            foreach (Session m in movies)
            {
                Assert.IsTrue(regEx2.IsMatch(m.movieId.title.ToLower()), "Results should match any movie with 'Matrix'");
                sessionCount++;
            }
            Assert.AreEqual(106, sessionCount, "Should have 106 results for movies with 'Matrix'");
        }


        [TestMethod]
        [ExpectedException(typeof(CustomCouldntFindException), "Could not find any results")]
        public void TestFailSearch()
        {
            SessionModel sessionModel = SessionModel.Instance;
            IEnumerable<Session> movies = sessionModel.SearchMovie("qqq");

            Assert.AreEqual(null, movies, "Should throw a CustomCouldntFindException");
        }
    }
}
