using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongOverflow.Models
{
    public class OverflowPost
    {
        /// <summary>
        /// The id of this post.
        /// </summary>
        public int _id { get; set; }

        /// <summary>
        /// Creation Moment.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Last time somebody touched this post.
        /// </summary>
        public DateTime? LastEditDate { get; set; }

        /// <summary>
        /// The score of this post.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The ID of the user that owns this post.
        /// </summary>
        public int OwnerID { get; set; }

        /// <summary>
        /// The ID of the user that owns this post.
        /// </summary>
        public int LastEditorID { get; set; }

        /// <summary>
        /// The HTML encoded body of this post.
        /// </summary>
        public String PostBody { get; set; }
    }
}
