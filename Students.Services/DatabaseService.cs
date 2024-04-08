using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

using System.Diagnostics.Metrics;

namespace Students.Services;

public class DatabaseService : IDatabaseService
{
    #region Ctor and Properties

    private readonly StudentsContext _context;

    private readonly ILogger<DatabaseService> _logger;

    public DatabaseService(
        ILogger<DatabaseService> logger,
        StudentsContext context)
    {
        _logger = logger;
        _context = context;
    }

    #endregion // Ctor and Properties


    #region Public Methods

    #region Student

    public async Task<IList<Student>> GetOllStudentsAsync()
    {
        var students = await _context.Student.ToListAsync();
        return students;
    }

    public async Task<Student?> GetStudentAsync(int? id)
    {
        var student = await _context.Student
                   .FirstOrDefaultAsync(m => m.Id == id);

        return student;
    }

    public async Task<Student?> GetStudentWithSubjectsAsync(int? id)
    {
        var student = await GetStudentAsync(id);

        if (student == null)
            return student;

        var chosenSubjects = _context.StudentSubject
            .Where(ss => ss.StudentId == id)
            .Select(ss => ss.Subject)
            .ToList();

        var availableSubjects = _context.Subject
            .Where(s => !chosenSubjects.Contains(s))
            .ToList();

        student.StudentSubjects = _context.StudentSubject
            .Where(x => x.StudentId == id)
            .ToList();

        student.AvailableSubjects = availableSubjects;

        return student;

    }

    public async Task<bool> CreateStudentAsync(int id, string name, int age, string major, int[] subjectIdDst)
    {
        var chosenSubjects = _context.Subject
            .Where(s => subjectIdDst.Contains(s.Id))
            .ToList();

        var availableSubjects = _context.Subject
            .Where(s => !subjectIdDst.Contains(s.Id))
            .ToList();

        var student = new Student()
        {
            Id = id,
            Name = name,
            Age = age,
            Major = major,
            AvailableSubjects = availableSubjects
        };
        foreach (var chosenSubject in chosenSubjects)
        {
            student.AddSubject(chosenSubject);
        }

        _context.Add(student);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteStudentAsync(int? id)
    {
        var student = await _context.Student.FindAsync(id);

        if (student == null)
        {
            return false;
        }
       
         _context.Student.Remove(student);

        await _context.SaveChangesAsync();

        return true;
    }


    public bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst)
    {
        var result = false;

        // Find the student
        var student = _context.Student.Find(id);
        if (student != null)
        {
            // Update the student's properties
            student.Name = name;
            student.Age = age;
            student.Major = major;

            // Get the chosen subjects
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();

            // Remove the existing StudentSubject entities for the student
            var studentSubjects = _context.StudentSubject
                .Where(ss => ss.StudentId == id)
                .ToList();
            _context.StudentSubject.RemoveRange(studentSubjects);

            // Add new StudentSubject entities for the chosen subjects
            foreach (var subject in chosenSubjects)
            {
                var studentSubject = new StudentSubject
                {
                    Student = student,
                    Subject = subject
                };
                _context.StudentSubject.Add(studentSubject);
            }

            // Save changes to the database
            var resultInt = _context.SaveChanges();
            result = resultInt > 0;
        }

        return result;
    }

    public Student? DisplayStudent(int? id)
    {
        Student? student = null;
        try
        {
            student = _context.Student
                .FirstOrDefault(m => m.Id == id);
            if (student is not null)
            {
                var studentSubjects = _context.StudentSubject
                    .Where(ss => ss.StudentId == id)
                    .Include(ss => ss.Subject)
                    .ToList();
                student.StudentSubjects = studentSubjects;
            }
        }
        catch (Exception ex)
        {
           _logger.LogError("Exception caught in DisplayStudent: " + ex);
        }

        return student;
    }


    public bool StudentExists(int id)
    {
        var result = _context.Student.Any(e => e.Id == id);
        return result;
    }

    #endregion

    #region Subjects 

    public async Task<List<Subject>> GetOllSubjectsAsync()
    {
        var subjects = await _context.Subject.ToListAsync();
        return subjects;
    }

    public async Task<Subject?> GetSubjectAsync(int? id)
    {
        var subject = await _context.Subject
            .FirstOrDefaultAsync(m => m.Id == id);

        return subject;
    }

    public async Task CreateSubjectAsync(Subject subject)
    {
        _context.Add(subject);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSubjectAsync(Subject subject)
    {
        _context.Update(subject);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubjectAsync(int id)
    {
        var subject = await _context.Subject.FindAsync(id);

        if (subject == null)
            return;
        
        _context.Subject.Remove(subject);

        await _context.SaveChangesAsync();
    }

    public bool SubjectExists(int id)
    {
        return _context.Subject.Any(e => e.Id == id);
    }

    public List<Subject> GetChosenSubjects(int? id)
    {
        var chosenSubjects = _context.StudentSubject
            .Where(ss => ss.StudentId == id)
            .Select(ss => ss.Subject)
            .ToList();

        return chosenSubjects;
    }

    public List<Subject> GetAvailableSubjects(int? id)
    {
        var chosenSubjects = GetChosenSubjects(id);

        var availableSubjects = _context.Subject
            .Where(s => !chosenSubjects.Contains(s))
            .ToList();

        return availableSubjects;
    }

    public List<StudentSubject> GetStudentSubjects(int? id)
    {
        var studentSubjects = _context.StudentSubject
            .Where(x => x.StudentId == id)
            .ToList();

        return studentSubjects;
    }



    #endregion

    #region Lecture Hall


    public async Task<IList<LectureHall>> GetOllLectureHallAsync()
    {
        var lectureHalls = await _context.LectureHall.ToListAsync();

        return lectureHalls;
    }

    public async Task<LectureHall?> GetLectureHallAsync(int? Id)
    {

        var lectureHall = await _context.LectureHall.Include(x => x.Subjects)
            .FirstOrDefaultAsync(m => m.Id == Id);

        return lectureHall;

    }

    public async Task<LectureHall?> GetLectureHallWithSubjectsAsync(int? id)
    {

        //var student = await GetStudentAsync(id);

        //if (student == null)
        //    return student;

        //var chosenSubjects = _context.StudentSubject
        //    .Where(ss => ss.StudentId == id)
        //    .Select(ss => ss.Subject)
        //    .ToList();

        //var availableSubjects = _context.Subject
        //    .Where(s => !chosenSubjects.Contains(s))
        //    .ToList();

        //student.StudentSubjects = _context.StudentSubject
        //    .Where(x => x.StudentId == id)
        //    .ToList();

        //student.AvailableSubjects = availableSubjects;

        //return student;




        var lectureHall = await GetLectureHallAsync(id);

        if (lectureHall == null)
            return lectureHall;



        var chosenSubjects = lectureHall.Subjects;

       

            //_context.Subject
            //.Where(ss => ss.LectureHallID == id)
            ////.Select(ss => ss.Subject)
            //.ToList();

        var availableSubjects = _context.Subject
            .Where(s => !chosenSubjects.Contains(s))
            .ToList();



        lectureHall.AvailableSubjects = availableSubjects;

        return lectureHall;

    }

    public async Task<LectureHall> CreateLectureHallAsync(int id, string name, int capacity, int[] subjectIdDst)
    {

        var chosenSubjects = _context.Subject
            .Where(s => subjectIdDst.Contains(s.Id))
            .ToList();

        var availableSubjects = _context.Subject
            .Where(s => !subjectIdDst.Contains(s.Id))
            .ToList();

        var lectureHall = new LectureHall()
        {
            Id = id,
            Name = name,
            Capacity = capacity,
            AvailableSubjects = availableSubjects
        };


        lectureHall.AvailableSubjects = availableSubjects;
        
        foreach (var chosenSubject in chosenSubjects)
        {
            lectureHall.AddSubject(chosenSubject);
        }


        _context.Add(lectureHall);
        await _context.SaveChangesAsync();

        return lectureHall;
    }

    public async Task UpdateLectureHallAsync(LectureHall lectureHallModel, int[] subjectIdDst)
    {

        var result = false;

        // Find the student
        var lectureHall = await GetLectureHallAsync(lectureHallModel.Id);
  
        if (lectureHall == null)
            return;

        lectureHall.Name = lectureHallModel.Name;
        lectureHall.Capacity = lectureHallModel.Capacity;
        

        
        // Get the chosen subjects
        var chosenSubjects = _context.Subject
            .Where(s => subjectIdDst.Contains(s.Id))
            .ToList();

        // Add new StudentSubject entities for the chosen subjects

        var availableSubjects = _context.Subject
            .Where(s => !subjectIdDst.Contains(s.Id))
            .ToList();

        lectureHall.AvailableSubjects = availableSubjects;

        lectureHall.Subjects = chosenSubjects;


        _context.Update(lectureHall);

        await _context.SaveChangesAsync();


    
        //_context.Update(lectureHall);
    }

    public async Task DeleteLectureHall(int id)
    {
        var lectureHall = await GetLectureHallAsync(id);

        if (lectureHall == null)
        {
            return;
        }

        _context.LectureHall.Remove(lectureHall);

        await _context.SaveChangesAsync();
    }

    public bool LectureHallExists(int id)
    {
        return _context.LectureHall.Any(e => e.Id == id);
    }

    #endregion

    #region Book

    public async Task<Book?> GetBookAsync(int? Id)
    {
        var book = await _context.Book
          .FirstOrDefaultAsync(m => m.Id == Id);

        return book;
    }

    public async Task<IList<Book>> GetOllBookAsync()
    {
        var lectureHalls = await _context.Book.ToListAsync();

        return lectureHalls;
    }

    public async Task CreateBookAsync(Book book)
    {
        _context.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        _context.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await GetBookAsync(id);

        if (book == null)
        {
            return;
        }

        _context.Book.Remove(book);

        await _context.SaveChangesAsync();
    }

    public bool BookExists(int id)
    {
        return _context.Book.Any(e => e.Id == id);
    }

    #endregion // Book

    #region Research Worker

    public async Task<ResearchWorker?> GetResearchWorkerAsync(int? Id)
    {
        var lectureHall = await _context.ResearchWorker
           .FirstOrDefaultAsync(m => m.Id == Id);

        return lectureHall;
    }

    public async Task<IList<ResearchWorker>> GetOllResearchWorkerAsync()
    {
        var lectureHalls = await _context.ResearchWorker.ToListAsync();

        return lectureHalls;
    }

    public async Task CreateResearchWorkerAsync(ResearchWorker researchWorker)
    {
        _context.Add(researchWorker);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateResearchWorkerAsync(ResearchWorker researchWorker)
    {
        _context.Update(researchWorker);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteResearchWorkerAsync(int? id)
    {
        var researchWorker = await GetResearchWorkerAsync(id);

        if (researchWorker == null)
        {
            return;
        }

        _context.ResearchWorker.Remove(researchWorker);

        await _context.SaveChangesAsync();
    }

    public bool ResearchWorkerExists(int id)
    {
        return _context.ResearchWorker.Any(e => e.Id == id);
    }

    #endregion // Research Worker

    #endregion // Public Methods
}
