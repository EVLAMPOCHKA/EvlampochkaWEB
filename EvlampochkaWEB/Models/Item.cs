using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvlampochkaWEB.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        public string ItemImage { get; set; }
        public string Tags { get; set; }   
       // public List<String> TagsList { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        //additional info, not required
        public string AddStringField0 { get; set; }
        public string AddStringField1 { get; set; }
        public string AddStringField2 { get; set; }
        public DateTime AddDateTimeField0 { get; set; }
        public DateTime AddDateTimeField1 { get; set; }
        public DateTime AddDateTimeField2 { get; set; }
        public bool AddBoolField0 { get; set; }
        public bool AddBoolField1 { get; set; }
        public bool AddBoolField2 { get; set; }
        //collection info
        public int CollectionID { get; set; }
        public Collection Collection { get; set; }
    }
}
