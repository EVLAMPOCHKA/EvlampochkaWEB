using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvlampochkaWEB.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set;}
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;
        public int ItemID { get; set; }
        public Item Item { get; set; }
        public string UserName { get; set; }
        public User User { get; set; }
    }
}
