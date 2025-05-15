using DeansOffice.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace DeansOffice.ViewModels
{
    public class TeacherEditDialogViewModel
    {
        public List<string> AcademicTitles { get; } = new List<string>
        {
            "Ассистент",
            "Старший преподаватель",
            "Доцент",
            "Профессор"
        };
        public List<string> Departments { get; } = new List<string>
        {
            "Кафедра информатики",
            "Кафедра экономики",
            "Кафедра физики",
            "Кафедра математики"
        };
        public Teacher teacher {  get; set; } = new Teacher();
    }
}

