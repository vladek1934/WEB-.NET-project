using Microsoft.AspNet.Identity;
using WEB_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_PROJECT.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Show");
        }
        [Authorize]
        public ActionResult Create()
        {
            Articles article = new Articles();
            return View(article);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Articles article)
        {
            Entities3 db = new Entities3();
            if (article != null)
            {
                article.UserID = User.Identity.GetUserId();
                article.UserName = User.Identity.GetUserName();
                article.Date = System.DateTime.Now.ToString();
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Show");
            }
            else return View();
        }

        [Authorize]
        public ActionResult Show()
        {
            Entities3 db = new Entities3();
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var artList = user.Articles.ToList();
            return View(artList);
        }
        [Authorize]
        public ActionResult Display()
        {
            Entities3 db = new Entities3();
            var artList = (from art in db.Articles select art).ToList();
            return View(artList);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            Entities3 db = new Entities3();
            Articles art = db.Articles.Find(id);
            return View(art);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Entities3 db = new Entities3();
            Articles art = db.Articles.Find(id);
            return View(art);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Articles article)
        {
            Entities3 db = new Entities3();
            var id = article.ArticleID;
            Articles art = db.Articles.Find(id);
            art.Body = article.Body;
            art.Header = article.Header;
            db.SaveChanges();
            return RedirectToAction("Show");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Entities3 db = new Entities3();
            Articles art = db.Articles.Find(id);
            db.Articles.Remove(art);
            db.SaveChanges();
            return RedirectToAction("Show");
        }
    }
}
