using DeansOffice.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Services
{
    public class StudentService
    {
        
        public List<Student> GetAllStudents()
        {
            var db = new DeanDbContext();

            var students = db.Students.ToList();
            return students;

        }

        public void AddStudent(Student student)
        {
           var db = new DeanDbContext();
            student.Group = db.Groups.FirstOrDefault(x=>x.GroupID==student.GroupID);
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            var db = new DeanDbContext();
            var student_to_update = db.Students.FirstOrDefault(x => x.StudentID == student.StudentID);

            if (student_to_update != null)
            {
                db.Students.Update(student_to_update);
                db.SaveChanges();
            }
        }

        public void DeleteStudent(Student student)
        {
            var db = new DeanDbContext();
            var student_to_delete = db.Students.FirstOrDefault(x => x.StudentID == student.StudentID);

            if (student_to_delete != null)
            {
                db.Students.Remove(student_to_delete);
                db.SaveChanges(); 
            }
        }
    }
}
