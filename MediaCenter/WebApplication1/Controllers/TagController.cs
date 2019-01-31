using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCenter.Data;
using MediaCenter.Models;

namespace MediaCenter.Controllers
{
    public class TagController : Controller
    {
        private readonly DbContextDemo _context;

        public TagController(DbContextDemo context)
        {
            _context = context;
        }

        // GET: Tags
        public async Task<IActionResult> Index(int? imageid)
        {
            var ListAllImages = await _context.Image.ToListAsync();
            var ImageMini = new Image();
            foreach (Image i in ListAllImages)
            {
                if (i.Id == imageid)
                {
                    ImageMini = i;
                }
            }
            ViewBag.imagemini = ImageMini;
            ViewBag.imageid = imageid;
            return View(await _context.Tag.ToListAsync());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Tags/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId, Nom, Size")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Tag", new { imageid = tag.ImageId });
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            return View(tag);
        }

        // POST: Tags/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ImageId, Nom, Size")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Update(tag);
                await _context.SaveChangesAsync();       
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? imageid)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Tag", new { imageid = tag.ImageId });
        }

        public async Task<IActionResult> Search(String RejectText)
        {
            var ListAllImages = await _context.Image.ToListAsync();
            var ListTags = await _context.Tag.ToListAsync();
            List<Image> ListImages = new List<Image>();

            foreach (Image i in ListAllImages)
            {
                foreach (Tag t in i.ListTag)
                {
                    if (!(RejectText == null))
                    {
                        if (!ListImages.Contains(i) && !RejectText.Equals(""))
                        {
                            if (t.Nom.ToLower().Contains(RejectText.ToLower()))
                            {
                                ListImages.Add(i);
                            }
                        }
                    }
                }
            }

            ViewBag.searchText = RejectText;
            ViewBag.lsImages = ListImages;

            return View();
        }
    }
}
