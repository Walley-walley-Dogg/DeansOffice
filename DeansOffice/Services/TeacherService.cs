using DeansOffice.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public void UpdateTeacher(Teacher teacher, int _id)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));

            using (var db = new DeanDbContext())
            {
                var existingTeacher = db.Teachers.FirstOrDefault(x => x.TeacherID == _id);

                if (existingTeacher == null)
                    throw new ArgumentException($"Group with ID {existingTeacher.TeacherID} not found.");

                existingTeacher.LastName = teacher.LastName;
                existingTeacher.FirstName = teacher.FirstName;
                existingTeacher.MiddleName = teacher.MiddleName;
                existingTeacher.Email = teacher.Email;
                existingTeacher.PhoneNumber = teacher.PhoneNumber;
                existingTeacher.Department = teacher.Department;
                existingTeacher.AcademicTitle = teacher.AcademicTitle;



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
