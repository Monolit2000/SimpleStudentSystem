using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

namespace Students.Web.Controllers
{
    public class LectureHallController : Controller
    {
        //private readonly StudentsContext _context;

        private readonly IDatabaseService _databaseService;

        public LectureHallController(StudentsContext context, IDatabaseService databaseService)
        {
          //  _context = context;
            _databaseService = databaseService; 
        }

        // GET: LectureHalls
        public async Task<IActionResult> Index()
        {
            return View(await _databaseService.GetOllLectureHallAsync());
        }

        // GET: LectureHalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureHall = await _databaseService.GetLectureHallAsync(id);

            if (lectureHall == null)
            {
                return NotFound();
            }

            return View(lectureHall);
        }

        // GET: LectureHalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LectureHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Capacity,Name")] LectureHall lectureHall)
        {
            if (ModelState.IsValid)
            {
                await _databaseService.CreateLectureHallAsync(lectureHall); 
                return RedirectToAction(nameof(Index));
            }
            return View(lectureHall);
        }

        // GET: LectureHalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureHall = await _databaseService.GetLectureHallAsync(id);
            if (lectureHall == null)
            {
                return NotFound();
            }
            return View(lectureHall);
        }

        // POST: LectureHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Capacity,Name")] LectureHall lectureHall)
        {
            if (id != lectureHall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _databaseService.UpdateLectureHallAsync(lectureHall);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureHallExists(lectureHall.Id))
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
            return View(lectureHall);
        }

        // GET: LectureHalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureHall = await _databaseService.GetLectureHallAsync(id);

            if (lectureHall == null)
            {
                return NotFound();
            }

            return View(lectureHall);
        }

        // POST: LectureHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _databaseService.DeleteLectureHall(id);

            return RedirectToAction(nameof(Index));
        }

        private bool LectureHallExists(int id)
        {
            return _databaseService.LectureHallExists(id);
        }
    }
}
