using Students.Interfaces;
using Students.Common.Data;
using Students.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Students.Web.Controllers
{
    public class ResearchWorkerController : Controller
    {
        //private readonly StudentsContext _context;

        private readonly IDatabaseService _databaseService;

        public ResearchWorkerController(StudentsContext context, IDatabaseService databaseService)
        {
            //_context = context;
            _databaseService = databaseService;
        }

        // GET: ResearchWorkers
        public async Task<IActionResult> Index()
        {
            return View(await _databaseService.GetOllResearchWorkerAsync());
        }

        // GET: ResearchWorkers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchWorker = await _databaseService.GetResearchWorkerAsync(id);

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
        public async Task<IActionResult> Create([Bind("Id,Name,Age")] ResearchWorker researchWorker)
        {
            if (ModelState.IsValid)
            {
                await _databaseService.CreateResearchWorkerAsync(researchWorker);

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

            var researchWorker = await _databaseService.GetResearchWorkerAsync(id);

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age")] ResearchWorker researchWorker)
        {
            if (id != researchWorker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _databaseService.UpdateResearchWorkerAsync(researchWorker);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchWorkerExists(researchWorker.Id))
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

            var researchWorker = await _databaseService.GetResearchWorkerAsync(id);

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
            await _databaseService.DeleteResearchWorkerAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool ResearchWorkerExists(int id)
        {
            return _databaseService.ResearchWorkerExists(id);
        }
    }
}
