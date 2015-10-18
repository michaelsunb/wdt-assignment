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

            IEnumerable<SelectListItem> items = db.Cineplexes
                .Select(c => new SelectListItem
                {
                    Value = c.CineplexID.ToString(),
                    Text = c.Location

                });
            ViewBag.Cineplexs = items;

            return View(db);
        }

        public ActionResult Cinemas()
        {
            return View(db);
        }
        public ActionResult ComingSoon()
        {
            return View(db);
        }
        public ActionResult MoviePreview()
        {
            int id = -1;
            try
            {
                id = int.Parse(this.Request.QueryString["MovieID"]);
            }
            catch (Exception e)
            {
                Redirect("/Home/Movies");
            }

            if (id != -1)
            {
                ViewBag.MoviePreview = db.Movies.Find(id);
            }

            Redirect("/Home/Movies");
            return View(db);
        }
        public ActionResult Enquire()
        {
            return View();
        }
    }
}