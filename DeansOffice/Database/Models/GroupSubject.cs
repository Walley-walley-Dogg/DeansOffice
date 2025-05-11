using DeansOffice.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Database
{
    public class GroupSubject
    {
        public int GroupID { get; set; }
        public Group Group { get; set; }

        public int SubjectID { get; set; }
        public Subject Subject { get; set; }

        public int Semester { get; set; }
    }
}
