using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    #region Student

    Student? DisplayStudent(int? id);

    Task<IList<Student>> GetOllStudentsAsync();

    Task<Student?> GetStudentAsync(int? id);

    Task<Student?> GetStudentWithSubjectsAsync(int? id);

    Task<bool> CreateStudentAsync(int id, string name, int age, string major, int[] subjectIdDst);

    Task<bool> DeleteStudentAsync(int? id);

    bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst);

    bool StudentExists(int id);

    #endregion

    #region Subject

    Task<List<Subject>> GetOllSubjectsAsync();

    Task<Subject?> GetSubjectAsync(int? id);

    Task CreateSubjectAsync(Subject subject);

    Task UpdateSubjectAsync(Subject subject);

    Task DeleteSubjectAsync(int id);

    bool SubjectExists(int id);

    List<Subject> GetChosenSubjects(int? id);

    List<Subject> GetAvailableSubjects(int? id);

    List<StudentSubject> GetStudentSubjects(int? id);


    #endregion

    #region Lecture Hall 

    Task<LectureHall?> GetLectureHallAsync(int? Id);

    Task<IList<LectureHall>> GetOllLectureHallAsync();

    Task<LectureHall?> GetLectureHallWithSubjectsAsync(int? id);
    Task<LectureHall> CreateLectureHallAsync(int id, string name, int capacity, int[] subjectIdDst);

    Task UpdateLectureHallAsync(LectureHall lectureHall, int[] subjectIdDst);

    Task DeleteLectureHall(int id);

    bool LectureHallExists(int id);

    #endregion

    #region Book
    Task<Book?> GetBookAsync(int? Id);

    Task<IList<Book>> GetOllBookAsync();

    Task CreateBookAsync(Book lectureHall);

    Task UpdateBookAsync(Book lectureHall);

    Task DeleteBookAsync(int id);

    bool BookExists(int id);

    #endregion

    #region ResearchWorker

    Task<ResearchWorker?> GetResearchWorkerAsync(int? Id);

    Task<IList<ResearchWorker>> GetOllResearchWorkerAsync();

    Task CreateResearchWorkerAsync(ResearchWorker researchWorker);

    Task UpdateResearchWorkerAsync(ResearchWorker researchWorker);

    Task DeleteResearchWorkerAsync(int? id);

    bool ResearchWorkerExists(int id);


    #endregion

}
