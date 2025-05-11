using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Database.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int Course { get; set; }
        public string Specialty { get; set; }

        public int? CuratorID { get; set; }
        public Teacher Curator { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<GroupSubject> GroupSubjects { get; set; }
    }
}
