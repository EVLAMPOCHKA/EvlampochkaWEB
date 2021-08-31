using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvlampochkaWEB.Data;
using EvlampochkaWEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace EvlampochkaWEB.Controllers
{
   [Authorize]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostEnv;

        public static string url = "https://res.cloudinary.com/dlrmdokvi/image/upload/v1628960926/istockphoto-1252916139-170667a_no1ew2.jpg";

        public ItemsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostEnv = hostEnv;
        }

        // GET: Items
        [AllowAnonymous]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.IsRealUser = false;
            ViewBag.CollectionId = id;
            var items = await _context.Item.Where(x => x.CollectionID == id).ToListAsync();
            foreach(var item in items)
            {
                item.Collection = _context.Collection.First(x => x.CollectionId == item.CollectionID);
            }
            return View(items);
        }
       
        public async Task<IActionResult> IndexForAuth(int id)
        {            
            ViewBag.IsRealUser = true;
            ViewBag.CollectionId = id;

            var items =  await _context.Item.Where(x => x.CollectionID == id).ToListAsync();
            foreach (var item in items)
            {
                item.Collection = _context.Collection.First(x => x.CollectionId == item.CollectionID);
            }
            return View("Index",items);
        }
        [AllowAnonymous]
        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {           
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            var collection = _context.Collection.Find(_context.Item.Find(id).CollectionID);
            ViewBag.IsRealUser = collection.UserId == _userManager.GetUserId(User)|| User.IsInRole("admin") ? true : false;
            ViewBag.ItemId = id;
            item.Comments = _context.Comment.Where(x => x.ItemID == id).ToList();
            item.Likes = _context.Like.Where(x => x.ItemID == id).ToList();
            item.Collection = collection;
            ViewBag.Likes = item.Likes.Count();
            ViewBag.TagsList = item.Tags.Split(',').ToList();
            string[] fields = { collection.AddStringField0, collection.AddStringField1, collection.AddStringField2, collection.AddDateTimeField0, collection.AddDateTimeField1,
            collection.AddDateTimeField2, collection.AddBoolField0, collection.AddBoolField1,collection.AddBoolField2};
            ViewBag.Fields = fields;
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Like([Bind("LikeId,ItemID")] Like like)
        {
            like.UserID= _userManager.GetUserId(User);
            var currentLike = _context.Like.FirstOrDefault(x => x.ItemID == like.ItemID && x.UserID == like.UserID);
            if (currentLike==null)
            {
                _context.Add(like);
            }
            else
            {
                _context.Like.Remove(currentLike);
            }

            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = like.ItemID });

        }

        [HttpPost]
        public async Task<IActionResult> AddComment([Bind("CommentId,CommentText,DateCreation, ItemID, UserName")] Comment comment)
        {
            comment.UserName = _userManager.GetUserName(User);
           // comment.User.Id = _userManager.GetUserId(User);
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id=comment.ItemID});
        } 

        [HttpPost]
       public void Upload (IFormFile file)
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("dlrmdokvi", "687753493891478", "2QB3giOBidYpol1__fj1J4G3Xrc");

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            var fileDic = "Files";
            var fileName = file.FileName;
            string filePath = Path.Combine(fileDic, fileName);
            var publicID = file.FileName;

            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new CloudinaryDotNet.FileDescription(file.FileName,file.OpenReadStream()),//@"C:\Users\David\Downloads\etgarPlusWebsite-master\etgarPlusWebsite\etgarPlus\images\1.png"),
                PublicId = publicID
            };
           
            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            url = cloudinary.Api.UrlImgUp.BuildUrl( publicID + filePath.Substring(filePath.LastIndexOf(".")));

          
            
        }

        // GET: Items/Create
        public IActionResult Create(int id)
        {
            var collection = _context.Collection.Find(id);
            ViewBag.CollectionId = id;
            string[] fields = { collection.AddStringField0, collection.AddStringField1, collection.AddStringField2, collection.AddDateTimeField0, collection.AddDateTimeField1,
            collection.AddDateTimeField2, collection.AddBoolField0, collection.AddBoolField1,collection.AddBoolField2};
            ViewBag.Fields = fields;
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,LikesNumber,ItemImage,Tags,AddStringField0,AddStringField1,AddStringField2,AddDateTimeField0,AddDateTimeField1,AddDateTimeField2,AddBoolField0,AddBoolField1,AddBoolField2,CollectionID")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.ItemImage = url;
                _context.Add(item);
                string[] tags = item.Tags.Split(',');
                foreach(string tag in tags)
                {
                    Tag myTag = _context.Tag.FirstOrDefault(x => x.TagName == tag);
                    if (myTag!=null) //its exist
                    {
                        myTag.TagNumber++;                       
                         _context.Update(myTag);                   
                    }
                    else
                    {
                        Tag newTag = new Tag();
                        newTag.TagName = tag;
                        newTag.TagNumber = 1;
                        _context.Add(newTag);
                    }
                }
                var collection = await _context.Collection.FindAsync(item.CollectionID);
                collection.CollectionSize++;
                _context.Update(collection);                    
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = item.CollectionID });
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            url = item.ItemImage;
            var collection = _context.Collection.Find(item.CollectionID);
            ViewBag.TagsList = item.Tags.Split(',').ToList();
            string[] fields = { collection.AddStringField0, collection.AddStringField1, collection.AddStringField2, collection.AddDateTimeField0, collection.AddDateTimeField1,
            collection.AddDateTimeField2, collection.AddBoolField0, collection.AddBoolField1,collection.AddBoolField2};
            ViewBag.Fields = fields;

            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,LikesNumber,ItemImage,Tags,AddStringField0,AddStringField1,AddStringField2,AddDateTimeField0,AddDateTimeField1,AddDateTimeField2,AddBoolField0,AddBoolField1,AddBoolField2, CollectionID")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    item.ItemImage = url;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = item.CollectionID });
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }
            var collection = _context.Collection.Find(_context.Item.Find(id).CollectionID);
            ViewBag.TagsList = item.Tags.Split(',').ToList();
            string[] fields = { collection.AddStringField0, collection.AddStringField1, collection.AddStringField2, collection.AddDateTimeField0, collection.AddDateTimeField1,
            collection.AddDateTimeField2, collection.AddBoolField0, collection.AddBoolField1,collection.AddBoolField2};
            ViewBag.Fields = fields;

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            string[] tags = item.Tags.Split(',');
            foreach (string tag in tags)
            {
                Tag myTag = _context.Tag.FirstOrDefault(x => x.TagName == tag);
                var collection = await _context.Collection.FindAsync(item.CollectionID);
               
                if (myTag != null) //its exist
                {
                    myTag.TagNumber--;
                    _context.Update(myTag);
                    collection.CollectionSize--;
                    _context.Update(collection);
                }
                
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { id = item.CollectionID });
        }

        [AllowAnonymous]
        public IActionResult Search(string info)
        {
            if(info!=null)
            {

                info = info.ToLower();
                List<Item> items = new List<Item>();
                var collections = _context.Collection.ToList();
                var dbItems = _context.Item.ToList();
                var comments = _context.Comment.ToList();

                foreach (var item in dbItems)
                {
                    item.Collection = collections.First(x => x.CollectionId == item.CollectionID);
                    item.Comments = comments.Where(x => x.ItemID == item.ItemId).ToList();
                    item.AddStringField0 = item.AddStringField0 == null ? "#" : item.AddStringField0;
                    item.AddStringField1 = item.AddStringField1 == null ? "#" : item.AddStringField1;
                    item.AddStringField2 = item.AddStringField2 == null ? "#" : item.AddStringField2;

                    if (item.ItemName.ToLower().Contains(info) || item.Tags.Contains(info) || item.Collection.CollectionName.ToLower().Contains(info) ||
                        item.AddStringField0.ToLower().Contains(info) || item.AddStringField1.ToLower().Contains(info) ||
                        item.AddStringField2.ToLower().Contains(info) || item.Collection.ShortDiscription.ToLower().Contains(info) ||
                        item.Collection.LongDiscrioption.ToLower().Contains(info) || item.Collection.Theme.ToString().ToLower().Contains(info))
                    {
                        items.Add(item);
                    }

                    foreach (var comment in item.Comments)
                    {
                        if (comment.CommentText.ToLower().Contains(info) && !items.Contains(item))
                        {
                            items.Add(item);
                            break;
                        }
                    }

                }
                ViewBag.isRealUser = false;
                if (items.Any())
                    return View("Index", items);

            }
            return View("NotFound");
        }
        [AllowAnonymous]
        public IActionResult SearchBytag(string tag)
        {
            ViewBag.isRealUser = false;

            var items = _context.Item.Where(x => x.Tags.Contains(tag)).ToList();
            foreach (var item in items)
            {
                item.Collection = _context.Collection.First(x => x.CollectionId == item.CollectionID);
            }
            return View("~/Views/Items/Index.cshtml", items);
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
