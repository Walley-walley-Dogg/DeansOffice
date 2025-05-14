using DeansOffice.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.ViewModels
{
    public class StudentEditViewModel
    {
        public List<string> Genders { get; } = new List<string>
        {
            "Мужской",
            "Женский"
        };

        public Student Student { get; set; } = new Student();
    }
}
