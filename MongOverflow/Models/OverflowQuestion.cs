using System;
using MongOverflow.Models;
using System.Collections.Generic;
namespace MongOverflow.Models
{
    public class OverflowQuestion : OverflowPost
    {
        public OverflowQuestion()
        {
            this.Answers = new List<OverflowAnswer>(0);
        }

        /// <summary>
        /// The number of times this post was viewed.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// An the question with an answer.
        /// </summary>
        public int? AcceptedAnswerId { get; set; }

        /// <summary>
        /// The answers associated with this question.
        /// </summary>
        public List<OverflowAnswer> Answers { get; set; }

        /// <summary>
        /// The title of this post.
        /// </summary>
        public String Title { get; set; }
    }
}