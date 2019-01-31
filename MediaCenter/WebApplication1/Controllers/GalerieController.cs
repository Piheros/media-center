using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediaCenter.Data;
using MediaCenter.Models;

namespace MediaCenter.Controllers
{
    public class GalerieController : Controller
    {
        private readonly DbContextDemo _context;
        private IGaleriesRepository _galeries;

        public GalerieController(DbContextDemo context, IGaleriesRepository galeries)
        {
            _context = context;
            _galeries = galeries;
        }

        /*
         * INDEX
         */
        public async Task<IActionResult> Index()
        {
            return View(await _context.Galerie.ToListAsync());
        }

        /*
         * CREATE
         */
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create";
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nom")] Galerie galerie)
        {
            if (ModelState.IsValid)
            {
                _galeries.Add(galerie);
                _context.SaveChangesAsync();

                //Create folder for images
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "public", galerie.Id.ToString()));

                return RedirectToAction(nameof(Index));
            }
            return View(galerie);
        }

        /*
         * DELETE
         */
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var galerie = await _context.Galerie.SingleOrDefaultAsync(m => m.Id == id);
            return View(galerie);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galerie = await _context.Galerie.SingleOrDefaultAsync(m => m.Id == id);
            _context.Galerie.Remove(galerie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*
         * DETAILS
         */
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Title"] = "Details";
            var galerie = await _context.Galerie.SingleOrDefaultAsync(e => e.Id == id);
            var ListAllImages = await _context.Image.ToListAsync();
            return View(galerie);
        }

        /*
         * EDIT
         */
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Title"] = "Edit";
            var galerie = await _context.Galerie.SingleOrDefaultAsync(e => e.Id == id);
            return View(galerie);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Nom")] Galerie galerie)
        {
            if (ModelState.IsValid)
            {
                _galeries.Update(galerie);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(galerie);
        }
    }
}
