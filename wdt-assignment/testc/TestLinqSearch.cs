using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using wdt_assignment.model;
using System.Collections.Generic;

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
            Assert.IsTrue((cinemaModel.Cineplex.Count > 0));
            Assert.AreEqual(cinemaModel.Cineplex.Count, 5);

            MovieModel movieModel = MovieModel.Instance;
            Assert.IsTrue((movieModel.Movies.Count > 0));
            Assert.AreEqual(movieModel.Movies.Count, 5);
        }

        [TestMethod]
        public void TestCinemaSearch()
        {
            CinemaModel cinemaModel = CinemaModel.Instance;
            List<Cineplex> cineplexs = cinemaModel.Cineplex;

            Assert.IsTrue(cineplexs.Exists(x => x.cinemaName == "St Kilda"));

            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex("Kilda".ToLower());
            Assert.IsTrue(cineplexs.Exists(x => regEx.IsMatch(x.cinemaName.ToLower())));
        }

        [TestMethod]
        public void TestSessionSearch()
        {
            SessionModel sessionModel = SessionModel.Instance;
            IEnumerable<Sessions> cineplexs = sessionModel.SearchCinplex("St Kilda");
            IEnumerable<Sessions> movies = sessionModel.SearchMovie("Matrix");

            System.Text.RegularExpressions.Regex regEx1 = new System.Text.RegularExpressions.Regex("Kilda".ToLower());
            System.Text.RegularExpressions.Regex regEx2 = new System.Text.RegularExpressions.Regex("The Matrix".ToLower());

            // Test Cineplex if true
            foreach (Sessions c in cineplexs)
                Assert.IsTrue(regEx1.IsMatch(c.cineplexId.cinemaName.ToLower()));
            // Test Cineplex if false
            foreach (Sessions c in cineplexs)
                Assert.IsFalse(regEx2.IsMatch(c.cineplexId.cinemaName.ToLower()));

            // Test Movie if false
            foreach (Sessions m in movies)
                Assert.IsFalse(regEx1.IsMatch(m.movieId.title.ToLower()));

            // Test Movie if true
            int sessionCount = 0;
            foreach (Sessions m in movies)
            {
                Assert.IsTrue(regEx2.IsMatch(m.movieId.title.ToLower()));
                sessionCount++;
            }
            Assert.AreEqual(105,sessionCount);
        }
    }
}
