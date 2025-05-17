using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeansOffice.Database.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string AcademicTitle { get; set; }
        public string Password_hashed { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

        public ICollection<Group> CuratedGroups { get; set; }
        public ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}
