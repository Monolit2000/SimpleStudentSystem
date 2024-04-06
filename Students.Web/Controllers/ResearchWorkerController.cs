using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;

namespace Students.Web.Controllers
{
    public class ResearchWorkerController : Controller
    {
        private readonly StudentsContext _context;

        public ResearchWorkerController(StudentsContext context)
        {
            _context = context;
        }

        // GET: ResearchWorkers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResearchWorker.ToListAsync());
        }

        // GET: ResearchWorkers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchWorker = await _context.ResearchWorker
                .FirstOrDefaultAsync(m => m.id == id);
            if (researchWorker == null)
            {
                return NotFound();
            }

            return View(researchWorker);
        }

        // GET: ResearchWorkers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResearchWorkers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Age")] ResearchWorker researchWorker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(researchWorker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(researchWorker);
        }

        // GET: ResearchWorkers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchWorker = await _context.ResearchWorker.FindAsync(id);
            if (researchWorker == null)
            {
                return NotFound();
            }
            return View(researchWorker);
        }

        // POST: ResearchWorkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Age")] ResearchWorker researchWorker)
        {
            if (id != researchWorker.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researchWorker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchWorkerExists(researchWorker.id))
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
            return View(researchWorker);
        }

        // GET: ResearchWorkers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchWorker = await _context.ResearchWorker
                .FirstOrDefaultAsync(m => m.id == id);
            if (researchWorker == null)
            {
                return NotFound();
            }

            return View(researchWorker);
        }

        // POST: ResearchWorkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researchWorker = await _context.ResearchWorker.FindAsync(id);
            if (researchWorker != null)
            {
                _context.ResearchWorker.Remove(researchWorker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearchWorkerExists(int id)
        {
            return _context.ResearchWorker.Any(e => e.id == id);
        }
    }
}
