using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Database.Models
{
    public class AdminAuthentication
    {
        public int AdminAuthenticationID { get; set; }

        public int TeacherID { get; set; }

        public Teacher Teacher { get; set; }

        public string Password_hashed { get; set; }

        public DateTime auth_time { get; set; } 



    }
}
