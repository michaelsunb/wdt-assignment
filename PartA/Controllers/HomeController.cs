using PartA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PartA.Controllers
{
    public class HomeController : Controller
    {
        masterEntities db = new masterEntities();
        public ActionResult SiteMap()
        {
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            const string url = "/Home/{0}";
            var items = new List<string>{ "Index", "Movies", "Cinemas", "ComingSoon" };
            var sitemap = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(ns + "urlset",
                from i in items
                select
                new XElement(ns + "url",
                    new XElement(ns + "loc", string.Format(url, i)),
                    new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                    new XElement(ns + "changefreq", "always"),
                    new XElement(ns + "priority", "0.5")
            )));
            return Content("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + sitemap.ToString(), "text/xml");
        }
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
                                      where (cinMov.CineplexID == cineplexId) &&
                                      (movies.Status == 1)
                                          select movies;
                ViewBag.CineplexMovies = queryCineplexMovies.ToList();
            }
            else
            {
                ViewBag.CineplexMovies = db.Movies.ToList();
            }

            var queryCineplex = from cineplex in db.Cineplexes
                                where cineplex.Status == 1
                                select cineplex;
            IEnumerable<SelectListItem> items = queryCineplex
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
                return RedirectToAction("Movies");
            }

            if (id != -1)
            {
                ViewBag.MoviePreview = db.Movies.Find(id);
            }

            return RedirectToAction("Movies");
        }

        [HttpPost]
        public ActionResult Enquire(Enquiry model)
        {
            if (ModelState.IsValid)
            {
                db.Enquiries.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Enquire()
        {
            return View();
        }
    }
}