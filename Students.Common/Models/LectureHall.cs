using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Common.Models
{
    public class LectureHall
    {
        public int Id { get; set; }

        [Range(1, 100)]
        public int Capacity { get; set; }   

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        //public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

        public ICollection<LectureHallSubject> LectureHallSubjects { get; set; } = new List<LectureHallSubject>();

        [NotMapped]
        public List<Subject> AvailableSubjects { get; set; } = new List<Subject>();


        public void AddSubject(Subject subject)
        {
            var studentSubject = new LectureHallSubject
            {
                LectureHall = this,
                Subject = subject
            };

            LectureHallSubjects.Add(studentSubject);
        }
    }
}
