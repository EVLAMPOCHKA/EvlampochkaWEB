using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace EvlampochkaWEB.Models
{
    public class User:IdentityUser
    {
      public  List<Collection> Collections { get; set; }
      public List<Like> Likes { get; set; }
    //public string LikedItemsId { get; set; }
    }
}
