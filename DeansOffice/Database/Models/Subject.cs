using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Database.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int Hours { get; set; }
        public int Semester { get; set; }

        public ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public ICollection<GroupSubject> GroupSubjects { get; set; }
    }

}
