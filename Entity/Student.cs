using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Entity
{
    public class Student
    {
        public Student()
        {
            PhotoUrl = new byte[6];
        }
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GradeId { get; set; }

        //New Fields
        public DateTime DateOfBirth { get; set; }
        public byte[] PhotoUrl { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }

        //Foreign Keys
        //One-to-one relationship
        public Grade Grade { get; set; }
    }
}
