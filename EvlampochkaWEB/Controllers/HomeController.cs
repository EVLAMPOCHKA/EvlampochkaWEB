using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EvlampochkaWEB.Models;
using EvlampochkaWEB.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace EvlampochkaWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<string> stringTags = new List<string>();
            var tags = _context.Tag.ToList().OrderByDescending(x=>x.TagNumber);
            foreach( Tag tag in tags)
            {
                if(tag.TagNumber!=0)
                {
                    stringTags.Add(tag.TagName);
                }
                
            }
            ViewBag.Tags = stringTags.Count<=30?stringTags:stringTags.Take(30);

            var allItems = _context.Item.ToList();
            allItems.OrderByDescending(x => x.CreationTime);

            var allCollections = _context.Collection.ToList();
            allCollections.OrderByDescending(x => x.CollectionSize);
            var collection = allCollections.Count <= 5 ? allCollections : allCollections.Take(5);
            collection.First().Items = allItems.Count <= 5 ? allItems : allItems.Take(5).ToList();
            ViewBag.IsRealUser = false;
            return View(collection);

        }
        [HttpPost]
        public IActionResult CultureManagment(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return  Redirect(returnUrl);

        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
