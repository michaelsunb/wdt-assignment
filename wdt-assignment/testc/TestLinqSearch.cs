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

            CinemaModel cinemaModel = CinemaModel.Instance;
            Assert.IsTrue((cinemaModel.Cineplex.Count > 0), "Should be more than zero results");
            Assert.AreEqual(cinemaModel.Cineplex.Count, 5, "From json there should 5 results");

            MovieModel movieModel = MovieModel.Instance;
            Assert.IsTrue((movieModel.Movies.Count > 0),"Should be more than zero results");
            Assert.AreEqual(movieModel.Movies.Count, 5,"From json there should 5 results");
        }

        [TestMethod]
        public void TestSearches()
        {
            CinemaModel cinemaModel = CinemaModel.Instance;
            IEnumerable<Cineplex> cineplexs = cinemaModel.SearchCinplex("St Kilda");

            MovieModel movieModel = MovieModel.Instance;
            IEnumerable<Movie> movies = movieModel.SearchMovie("The Matrix");

            System.Text.RegularExpressions.Regex regEx1 = new System.Text.RegularExpressions.Regex("Kilda".ToLower());
            System.Text.RegularExpressions.Regex regEx2 = new System.Text.RegularExpressions.Regex("The Matrix".ToLower());

            // Test Cineplex if true
            foreach(Cineplex c in cineplexs)
                Assert.IsTrue(regEx1.IsMatch(c.cinemaName.ToLower()), "Results should match any cinema with 'Kilda'");
            // Test Cineplex if false
            foreach (Cineplex c in cineplexs)
                Assert.IsFalse(regEx2.IsMatch(c.cinemaName.ToLower()), "Should not find any matches from 'Matrix'");

            // Test Movie if true
            foreach (Movie m in movies)
                Assert.IsTrue(regEx2.IsMatch(m.title.ToLower()), "Results should match any movie with 'Matrix'");
            // Test Movie if false
            foreach (Movie m in movies)
                Assert.IsFalse(regEx1.IsMatch(m.title.ToLower()), "Should not find any matches from 'Kilda'");
        }

        [TestMethod]
        public void TestSessionSearch()
        {
            SessionModel sessionModel = SessionModel.Instance;
            IEnumerable<Session> cineplexs = sessionModel.SearchCinplex("St Kilda");
            IEnumerable<Session> movies = sessionModel.SearchMovie("Matrix");

            System.Text.RegularExpressions.Regex regEx1 = new System.Text.RegularExpressions.Regex("Kilda".ToLower());
            System.Text.RegularExpressions.Regex regEx2 = new System.Text.RegularExpressions.Regex("Matrix".ToLower());

            // Test Cineplex if true
            foreach (Session c in cineplexs)
                Assert.IsTrue(regEx1.IsMatch(c.cineplexId.cinemaName.ToLower()), "Results should match any cinema with 'Kilda'");
            // Test Cineplex if false
            foreach (Session c in cineplexs)
                Assert.IsFalse(regEx2.IsMatch(c.cineplexId.cinemaName.ToLower()), "Should not find any matches from 'Matrix'");

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
            Assert.AreEqual(105, sessionCount, "Should have 105 results for movies with 'Matrix'");
        }


        [TestMethod]
        [ExpectedException(typeof(CustomCouldntFindException), "Could not find any results")]
        public void TestFailSearch()
        {
            SessionModel sessionModel = SessionModel.Instance;
            IEnumerable<Session> cineplexs = sessionModel.SearchCinplex("qqq");
            IEnumerable<Session> movies = sessionModel.SearchMovie("qqq");

            Assert.AreEqual(null, cineplexs, "Should throw a CustomCouldntFindException");
            Assert.AreEqual(null, movies, "Should throw a CustomCouldntFindException");
        }
    }
}
