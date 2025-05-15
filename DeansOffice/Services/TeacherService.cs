using DeansOffice.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Services
{
    public class TeacherService
    {
        public List<Teacher> GetAllTeachers()
        {
            var db = new DeanDbContext();
            var teachers = db.Teachers.ToList();
            return teachers;
        }

        public void AddTeacher(Teacher teacher)
        {
            var db = new DeanDbContext();
            db.Teachers.Add(teacher);
            db.SaveChanges();
        }

        public void UpdateTeacher(Teacher teacher)
        {
            var db = new DeanDbContext();
            var teacher_to_update = db.Teachers.FirstOrDefault(x => x.TeacherID == teacher.TeacherID);
            if(teacher_to_update != null)
            {
                db.Teachers.Update(teacher_to_update);
                db.SaveChanges();
            }
        }

        public void DeleteTeacher(Teacher teacher)
        {
            var db = new DeanDbContext();
            var teacher_to_delete = db.Teachers.FirstOrDefault(x => x.TeacherID == teacher.TeacherID);
            if(teacher_to_delete != null)
            {
                db.Teachers.Remove(teacher_to_delete);
                db.SaveChanges();
            }
        }

    }
}
