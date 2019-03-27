using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Controllers
{
    public class SouvenirsController : Controller
    {
        private readonly ApplicationContext _context;

        public SouvenirsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Souvenirs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Souvenirs.ToListAsync());
        }

        // GET: Souvenirs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }

        // GET: Souvenirs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Souvenirs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Price,PathOfImage")] Souvenir souvenir)
        {
            if (ModelState.IsValid)
            {
                _context.Add(souvenir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(souvenir);
        }

        // GET: Souvenirs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs.FindAsync(id);
            if (souvenir == null)
            {
                return NotFound();
            }
            return View(souvenir);
        }

        // POST: Souvenirs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price,PathOfImage")] Souvenir souvenir)
        {
            if (id != souvenir.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(souvenir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SouvenirExists(souvenir.ID))
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
            return View(souvenir);
        }

        // GET: Souvenirs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }

        // POST: Souvenirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var souvenir = await _context.Souvenirs.FindAsync(id);
            _context.Souvenirs.Remove(souvenir);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SouvenirExists(int id)
        {
            return _context.Souvenirs.Any(e => e.ID == id);
        }
    }
}
