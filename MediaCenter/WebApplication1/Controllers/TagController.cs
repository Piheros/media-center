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

        /*
         * INDEX
         */
        public async Task<IActionResult> Index(int? imageid)
        {
            var ListAllImages = await _context.Image.ToListAsync();
            var Image = new Image();
            foreach (Image i in ListAllImages)
            {
                if (i.Id == imageid)
                {
                    Image = i;
                }
            }
            ViewBag.imagemini = Image;
            ViewBag.imageid = imageid;
            return View(await _context.Tag.ToListAsync());
        }

        /*
         * CREATE
         */
        public IActionResult Create(int? id)
        {
            ViewBag.id = id;
            return View();
        }

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

        /*
         * DELETE
         */
        public async Task<IActionResult> Delete(int? id)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? imageid)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Tag", new { imageid = tag.ImageId });
        }

        /*
         * DETAILS
         */
        public async Task<IActionResult> Details(int? id)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            return View(tag);
        }

        /*
         * EDIT
         */
        public async Task<IActionResult> Edit(int? id)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(m => m.Id == id);
            return View(tag);
        }

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

        /*
         * SEARCH
         */
        public async Task<IActionResult> Search(String RejectText)
        {
            var ListAllImages = await _context.Image.ToListAsync();
            var ListTags = await _context.Tag.ToListAsync();
            List<Image> ListImages = new List<Image>();

            foreach (Image img in ListAllImages)
            {
                foreach (Tag tag in img.ListTag)
                {
                    if (!(RejectText == null))
                    {
                        if (!ListImages.Contains(img) && !RejectText.Equals(""))
                        {
                            if (tag.Nom.ToLower().Contains(RejectText.ToLower()))
                            {
                                ListImages.Add(img);
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
