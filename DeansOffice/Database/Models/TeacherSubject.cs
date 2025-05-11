using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Database.Models
{
    public class TeacherSubject
    {
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
    }

}
