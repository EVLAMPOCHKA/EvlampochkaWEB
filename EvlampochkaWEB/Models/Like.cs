using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvlampochkaWEB.Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public int ItemID { get; set; }
        public Item Item { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
