using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Common.Models
{
    public class LectureHallSubject
    {

        public int LectureHallId { get; set; }
        public required LectureHall LectureHall { get; set; }

        public int SubjectId { get; set; }
        public required Subject Subject { get; set; }

    }
}
