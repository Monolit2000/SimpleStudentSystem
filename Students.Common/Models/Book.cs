using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Common.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public Book()
        {

        }

        public Book(string name)
        {
            Name = name;    
        }

    }
}
