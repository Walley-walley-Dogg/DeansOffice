using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Database.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }

        public int GroupID { get; set; }
        public Group Group { get; set; }

        public int SubjectID { get; set; }
        public Subject Subject { get; set; }

        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public int DayOfWeek { get; set; }
        public DateTime TimeSlot { get; set; }
        public string Room { get; set; }
    }

}
