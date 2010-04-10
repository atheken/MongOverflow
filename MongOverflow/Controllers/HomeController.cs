using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using MongOverflow.Models;

namespace MongOverflow.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly String _connStringName = "sourceData";

        public ActionResult Index()
        {
            var posts = new List<OverflowQuestion>();
            using (var db = Mongo.Create(this._connStringName))
            {
                posts = db.GetCollection<OverflowQuestion>().Find(new { _id = Q.GreaterThan(4000) }, 100).ToList();
            }

            return View(posts);
        }
    }
}
