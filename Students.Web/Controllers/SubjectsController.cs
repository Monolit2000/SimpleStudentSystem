using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

namespace Students.Web.Controllers;

public class SubjectsController : Controller
{
    //private readonly StudentsContext _context;

    private readonly IDatabaseService _databaseService;

    public SubjectsController(
        //StudentsContext context,
        IDatabaseService databaseService)
    {
        //_context = context;
        _databaseService = databaseService;
    }

    // GET: Subjects
    public async Task<IActionResult> Index()
    {
        return View(await _databaseService.GetOllSubjectsAsync());
    }

    // GET: Subjects/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _databaseService.GetSubjectAsync(id);

        if (subject == null)
        {
            return NotFound();
        }

        return View(subject);
    }

    // GET: Subjects/Create
    public async Task<IActionResult> Create()
    {
        var listOfStudents = await _databaseService.GetOllStudentsAsync();

        var newSubject = new Subject();
        newSubject.AvailableStudents = listOfStudents.ToList();

        return View(newSubject);
    }

    // POST: Subjects/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create( Subject subject, int[] subjectIdDst)
    {
        if (ModelState.IsValid)
        {
            //_context.Add(subject);
            //await _context.SaveChangesAsync();

            await _databaseService.CreateSubjectAsync(subject,subjectIdDst);

            return RedirectToAction(nameof(Index));
        }
        return View(subject);
    }

    // GET: Subjects/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        //var subject = await _context.Subject.FindAsync(id);

        var subject = await _databaseService.GetSubjectWithStudentsAsync(id);

        if (subject == null)
        {
            return NotFound();
        }
        return View(subject);
    }

    // POST: Subjects/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Subject subject, int[] subjectIdDst)
    {
        if (id != subject.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                //_context.Update(subject);
                //await _context.SaveChangesAsync();

                await _databaseService.UpdateSubjectAsync(subject, subjectIdDst);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(subject.Id))
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
        return View(subject);
    }

    // GET: Subjects/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        //var subject = await _context.Subject
        //    .FirstOrDefaultAsync(m => m.Id == id);

        var subject = await _databaseService.GetSubjectAsync(id);

        if (subject == null)
        {
            return NotFound();
        }

        return View(subject);
    }

    // POST: Subjects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _databaseService.DeleteSubjectAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private bool SubjectExists(int id)
    {
        var exist = _databaseService.SubjectExists(id);
        return exist;
    }
}
