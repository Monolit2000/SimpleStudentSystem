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

        [NotMapped]
        public ICollection<Student> Students { get; private set; } = new List<Student>();

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

    }
}
