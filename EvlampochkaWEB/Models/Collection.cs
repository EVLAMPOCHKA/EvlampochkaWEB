using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvlampochkaWEB.Models
{
    public class Collection
    {
        public int CollectionId { get; set; }        
        public string CollectionName { get; set; }
        public Themes Theme { get; set; }
        public string ShortDiscription { get; set; }
        public string LongDiscrioption { get; set; }
        public string CollectionImage { get; set; }
        public List<Item> Items { get; set; }
        public int CollectionSize { get; set; } = 0;
        //add info
        public string AddStringField0 { get; set; }
        public string AddStringField1 { get; set; }
        public string AddStringField2 { get; set; }
        public string AddDateTimeField0 { get; set; }
        public string AddDateTimeField1 { get; set; }
        public string AddDateTimeField2 { get; set; }
        public string AddBoolField0 { get; set; }
        public string AddBoolField1 { get; set; }
        public string AddBoolField2 { get; set; }
        //user info
        public string UserId { get; set; }
        public User User { get; set; }

    }

    public enum Themes
    {
        Science,
        Art,
        Food,
        Drinks
    }
}
