using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvlampochkaWEB.Data;
using EvlampochkaWEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EvlampochkaWEB.Controllers
{
    [Authorize]
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CollectionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Collections
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewBag.IsRealUser = false;
            ViewBag.UserId = _userManager.GetUserId(User);
            return View(await _context.Collection.ToListAsync());
        }

        
        public async Task<IActionResult> IndexForAuth()
        {
            ViewBag.IsRealUser = true;
            ViewBag.UserId = _userManager.GetUserId(User);
            var collections = User.IsInRole("admin") ? await _context.Collection.ToListAsync() : await _context.Collection.Where(x => x.UserId == _userManager.GetUserId(User)).ToListAsync();
            return View("Index",collections);
        }

        // GET: Collections/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .FirstOrDefaultAsync(m => m.CollectionId == id);
            if (collection == null)
            {
                return NotFound();
            }
            ViewBag.UserName = _userManager.Users.First(x => x.Id == collection.UserId).Email;
            ViewBag.IsRealUser = collection.UserId == _userManager.GetUserId(User) || User.IsInRole("admin") ? true : false;
            return View(collection);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollectionId,CollectionName,Theme,ShortDiscription,LongDiscrioption,CollectionImage,AddStringField0,AddStringField1,AddStringField2,AddDateTimeField0,AddDateTimeField1,AddDateTimeField2,AddBoolField0,AddBoolField1,AddBoolField2")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                collection.UserId = _userManager.GetUserId(User);
                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CollectionId,CollectionName,Theme,ShortDiscription,LongDiscrioption,CollectionImage,AddStringField0,AddStringField1,AddStringField2,AddDateTimeField0,AddDateTimeField1,AddDateTimeField2,AddBoolField0,AddBoolField1,AddBoolField2")] Collection collection)
        {
            if (id != collection.CollectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.CollectionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .FirstOrDefaultAsync(m => m.CollectionId == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collection = await _context.Collection.FindAsync(id);
            _context.Collection.Remove(collection);
            var items = _context.Item.Where(x=>x.CollectionID==id).ToList();
            foreach(var item in items)
            {
                _context.Item.Remove(item);
                string[] tags = item.Tags.Split(',');
                foreach (string tag in tags)
                {
                    Tag myTag = _context.Tag.FirstOrDefault(x => x.TagName == tag);

                    if (myTag != null) //its exist
                    {
                        myTag.TagNumber--;
                        _context.Update(myTag);
                        _context.Update(collection);
                    }

                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id)
        {
            return _context.Collection.Any(e => e.CollectionId == id);
        }
    }
}
