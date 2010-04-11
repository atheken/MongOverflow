using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Norm;
using MongOverflow.Models;
using Norm.Protocol.SystemMessages;

namespace MongOverflow.Controllers
{
    [HandleError]
    public class QuestionsController : Controller
    {
        private readonly String _connStringName = "sourceData";

        public ActionResult Index()
        {
            var posts = Enumerable.Empty<OverflowQuestion>();
            using (var db = Mongo.Create(this._connStringName))
            {
                //db.Database.SetProfileLevel(ProfileLevel.AllOperations);
                var query = new { AcceptedAnswerId = Q.NotEqual(new int?()) };
                posts = db.GetCollection<OverflowQuestion>().Find(query, new {CreationDate = -1}, 10, 0).ToList();
            }
            return View(posts);
        }

        public ActionResult View(int id)
        {
            OverflowQuestion currentPost = null;
            using (var db = Mongo.Create(this._connStringName))
            {
                currentPost = db.GetCollection<OverflowQuestion>().FindOne(new { _id = id });
            }
            return this.View(currentPost);
        }
    }
}
