using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeansOffice.Database.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int EnrollmentYear { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int GroupID { get; set; }
        public Group Group { get; set; }
    }
}
