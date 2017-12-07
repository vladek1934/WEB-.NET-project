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
            return View();
        }
        [Authorize]
        public ActionResult Draw()
        {
            Images image = new Images();
            return View(image);
        }
        [Authorize]
        public ActionResult Showcase()
        {
            Entities2 db = new Entities2();
            var imagesList = (from img in db.Images select img).ToList();
            return View(imagesList);
        }
        [Authorize]
        public ActionResult Drawings()
        {
            Entities2 db = new Entities2();
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var imagesList = user.Images.ToList();
            return View(imagesList);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Draw(HttpPostedFileBase image)
        {
            Entities2 db = new Entities2();
            Images model = new Images();
            model.UserID = User.Identity.GetUserId();
            if (image != null)
            {
                model.Name = image.FileName+" " + User.Identity.GetUserName();
                model.Image = new byte[image.ContentLength];
                image.InputStream.Read(model.Image, 0, image.ContentLength);
                db.Images.Add(model);
                db.SaveChanges();
                return RedirectToAction("Drawings");
            }
            else return Index();
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            Entities2 db = new Entities2();
            Images img = db.Images.Find(id);
            db.Images.Remove(img);
            db.SaveChanges();
            return RedirectToAction("Drawings");
        }
    }
}
