using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    #region Student

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


    Student? DisplayStudent(int? id);
}
