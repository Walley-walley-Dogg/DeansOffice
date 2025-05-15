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

        public void UpdateStudent(Student student, int _id)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            using (var db = new DeanDbContext())
            {
                var existingStudent = db.Students.FirstOrDefault(x => x.StudentID == _id);

                if (existingStudent == null)
                    throw new ArgumentException($"Group with ID {existingStudent.StudentID} not found.");

                existingStudent.LastName = student.LastName;
                existingStudent.FirstName = student.FirstName;
                existingStudent.MiddleName = student.MiddleName;
                existingStudent.BirthDate = student.BirthDate;
                existingStudent.Gender = student.Gender;
                existingStudent.EnrollmentYear = student.EnrollmentYear;
                existingStudent.Email = student.Email;
                existingStudent.PhoneNumber = student.PhoneNumber;
                existingStudent.GroupID = student.GroupID;


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
