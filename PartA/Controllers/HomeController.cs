using PartA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartA.Controllers
{
    public class HomeController : Controller
    {
        masterEntities db = new masterEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";
            return View(db);
        }

        public ActionResult Movies()
        {
            int cineplexId = -1;
            try
            {
                cineplexId = int.Parse(this.Request.QueryString["CineplexID"]);
            }
            catch (Exception e)
            {
                Redirect("/Home/Movies");
            }

            if (cineplexId != -1)
            {
                var queryCineplexMovies = from movies in db.Movies
                                      join cinMov in db.CineplexMovies
                                      on movies.MovieID equals cinMov.MovieID
                                      where cinMov.CineplexID == cineplexId
                                          select movies;
                ViewBag.CineplexMovies = queryCineplexMovies.ToList();
            }
            else
            {
                ViewBag.CineplexMovies = db.Movies.ToList();
            }


            ViewBag.Message = "Your application description page.";

            IEnumerable<SelectListItem> items = db.Cineplexes
                .Select(c => new SelectListItem
                {
                    Value = c.CineplexID.ToString(),
                    Text = c.Location

                });
            ViewBag.Cineplexs = items;

            var query = db.Cineplexes.Select(c => new { c.CineplexID, c.Location });
            ViewBag.CategoryId = new SelectList(query.AsEnumerable(), "CategoryID", "CategoryName", 3);

            return View(db);
        }

        public ActionResult Cinemas()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}