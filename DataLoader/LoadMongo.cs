using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using MongOverflow.Models;
using Norm;
using System.Configuration;

namespace DataLoader
{
    public static class LoaderProgram
    {
        public static void Main(String[] args)
        {
            if (args.Length == 1)
            {
                var lm = new LoadMongo();
                lm.LoadMongoQuestions(args[0]);
            }
            else
            {
                Console.WriteLine("USAGE: LoaderProgram.exe <path to export xml files>");
            }
        }
    }

    public class LoadMongo
    {

        private List<OverflowQuestion> _questionQueue = new List<OverflowQuestion>(1000);
        private Dictionary<int, HashSet<OverflowAnswer>> _answerQueue = new Dictionary<int, HashSet<OverflowAnswer>>(1000);

        private String _connStringName = "loadDestination";

        public void LoadMongoQuestions(String path)
        {
            Console.WriteLine("Do you want to REIMPORT the MongOverflow collections? [YES|NO]");
            var response = Console.ReadLine();
            if (response.Trim().ToUpper() == "YES")
            {
                using (var db = Mongo.Create(this._connStringName))
                {
                    var colls = db.Database.GetAllCollections().ToArray();
                    if (colls.Any(y => y.Name == "MongOverflow." + typeof(OverflowQuestion).Name))
                    {
                        db.Database.DropCollection(typeof(OverflowQuestion).Name);
                    }
                }
                this.ProcessFile(path + "/posts.xml", 1);//questions
                this.ProcessFile(path + "/posts.xml", 2);//answers
            }
        }

        private void ProcessFile(String fileUri, int postType)
        {
            var reader = XmlTextReader.Create(fileUri);
            int i = 0;
            long readBytes = 0;
            while (reader.Read())
            {
                if (reader.Name == "row")
                {
                    var outerXml = reader.ReadOuterXml();
                    readBytes += outerXml.Length;
                    var post = XElement.Parse(outerXml);
                    var type = Int32.Parse(post.Attribute("PostTypeId").Value);
                    if (type == postType)//this is a post
                    {
                        if (type == 1)
                        {
                            this.ProcessQuestion(post);
                        }
                        else
                        {
                            this.ProcessAnswer(post);
                        }
                    }
                    if (i++ % 1000 == 0)
                    {
                        Console.WriteLine("rows: {0}, GB Read: {1}", i, Math.Round((double)readBytes / (1024 * 1024 * 1024), 3));
                    }
                }
            }
            this.FlushQuestions(true);
            this.FlushAnswers(true);
        }

        private void ProcessQuestion(XElement question)
        {
            try
            {
                var q = AssignValues<OverflowQuestion>(question);
                if (question.Attribute("AcceptedAnswerId") != null)
                {
                    q.AcceptedAnswerId = Int32.Parse(question.Attribute("AcceptedAnswerId").Value);
                }
                q.Title = question.Attribute("Title").Value;
                q.Views = Int32.Parse(question.Attribute("ViewCount").Value);
                this._questionQueue.Add(q);
                this.FlushQuestions(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private T AssignValues<T>(XElement question) where T : OverflowPost, new()
        {
            T retval = new T();

            retval._id = Int32.Parse(question.Attribute("Id").Value);
            retval.CreationDate = DateTime.Parse(question.Attribute("CreationDate").Value);
            if (question.Attribute("LastEditDate") != null)
            {
                retval.LastEditDate = DateTime.Parse(question.Attribute("LastEditDate").Value);
                retval.LastEditorID = Int32.Parse(question.Attribute("LastEditorUserId").Value);
            }
            retval.PostBody = question.Attribute("Body").Value;
            if (question.Attribute("OwnerUserID") != null)
            {
                retval.OwnerID = Int32.Parse(question.Attribute("OwnerUserId").Value);
            }

            return retval;
        }

        private void FlushQuestions(bool finalize)
        {
            if (this._questionQueue.Count >= 1000 || (finalize && this._questionQueue.Any()))
            {
                using (var db = Mongo.Create(this._connStringName))
                {
                    var collection = db.GetCollection<OverflowQuestion>();
                    collection.Insert(this._questionQueue);
                    Console.WriteLine("Wrote {0} questions to the DB", 1000);
                    this._questionQueue.Clear();
                }
            }
        }

        private void ProcessAnswer(XElement answer)
        {
            var a = AssignValues<OverflowAnswer>(answer);
            var parentID = Int32.Parse(answer.Attribute("ParentId").Value);
            HashSet<OverflowAnswer> currAnswers = null;
            if (!this._answerQueue.TryGetValue(parentID, out currAnswers))
            {
                currAnswers = new HashSet<OverflowAnswer>(new AnswerComparer());
                this._answerQueue[parentID] = currAnswers;
            }
            currAnswers.Add(a);
            this.FlushAnswers(false);
        }

        private void FlushAnswers(bool finalize)
        {
            if (this._answerQueue.Count >= 1000 || (finalize && this._answerQueue.Any()))
            {
                using (var db = Mongo.Create(this._connStringName))
                {
                    var coll = db.GetCollection<OverflowQuestion>();

                    foreach (var a in this._answerQueue)
                    {
                        coll.Update(new { _id = a.Key }, new { Answers = M.PushAll(a.Value.ToArray()) }, false, false);
                    }
                    this._answerQueue.Clear();
                }
            }
        }


        protected class AnswerComparer : IEqualityComparer<OverflowAnswer>
        {
            public bool Equals(OverflowAnswer x, OverflowAnswer y)
            {
                return x._id == y._id;
            }

            public int GetHashCode(OverflowAnswer obj)
            {
                return obj._id;
            }
        }
    }
}
