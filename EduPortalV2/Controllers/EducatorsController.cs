using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduPortalV2.Models;
using EduPortalV2.Models.AppDBContext;

namespace EduPortalV2.Controllers
{
    public class EducatorsController : Controller
    {
        private readonly AppDBContext _context;

        public EducatorsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Educators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Educator.ToListAsync());
        }

        // GET: Educators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educator = await _context.Educator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educator == null)
            {
                return NotFound();
            }

            return View(educator);
        }

        // GET: Educators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Educators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameSurname,EducationType")] Educator educator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educator);
        }

        // GET: Educators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educator = await _context.Educator.FindAsync(id);
            if (educator == null)
            {
                return NotFound();
            }
            return View(educator);
        }

        // POST: Educators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameSurname,EducationType")] Educator educator)
        {
            if (id != educator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducatorExists(educator.Id))
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
            return View(educator);
        }

        // GET: Educators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educator = await _context.Educator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educator == null)
            {
                return NotFound();
            }

            return View(educator);
        }

        // POST: Educators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educator = await _context.Educator.FindAsync(id);
            _context.Educator.Remove(educator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducatorExists(int id)
        {
            return _context.Educator.Any(e => e.Id == id);
        }
    }
}
