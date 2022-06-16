using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParrotsProject.Data;
using ParrotsProject.Data.Models;

namespace ParrotsProject.Controllers
{
    public class ParrotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParrotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parrots
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Parrots.Include(p => p.Breed);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Parrots/Details/
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parrot = await _context.Parrots
                .Include(p => p.Breed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parrot == null)
            {
                return NotFound();
            }

            return View(parrot);
        }

        // GET: Parrots/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Name");
            return View();
        }

        // POST: Parrots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,ImageUrl,BreedId,Id")] Parrot parrot)
        {
            if (ModelState.IsValid)
            {
                parrot.Id = Guid.NewGuid();
                _context.Add(parrot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Name", parrot.BreedId);
            return View(parrot);
        }

        // GET: Parrots/Edit/
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parrot = await _context.Parrots.FindAsync(id);
            if (parrot == null)
            {
                return NotFound();
            }
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Name", parrot.BreedId);
            return View(parrot);
        }

        // POST: Parrots/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Age,ImageUrl,BreedId,Id")] Parrot parrot)
        {
            if (id != parrot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parrot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParrotExists(parrot.Id))
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
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Name", parrot.BreedId);
            return View(parrot);
        }

        // GET: Parrots/Delete/
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parrot = await _context.Parrots
                .Include(p => p.Breed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parrot == null)
            {
                return NotFound();
            }

            return View(parrot);
        }

        // POST: Parrots/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var parrot = await _context.Parrots.FindAsync(id);
            _context.Parrots.Remove(parrot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParrotExists(Guid id)
        {
            return _context.Parrots.Any(e => e.Id == id);
        }
    }
}
