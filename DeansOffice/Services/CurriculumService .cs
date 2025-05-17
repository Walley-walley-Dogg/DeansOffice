using DeansOffice.Database;
using DeansOffice.Database.Models;
using DeansOffice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeansOffice.Services
{
    public class CurriculumService
    {
        private readonly DeanDbContext _context = new DeanDbContext();
        public async Task AddSubjectAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubjectAsync(int subjectId)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups
                .Include(g => g.Curator)
                .ToListAsync();
        }

        public  List<Subject> GetAllSubjects()
        {
            return  _context.Subjects.ToList();
        }

        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<List<GroupSubject>> GetGroupSubjectsAsync(int groupId)
        {
            return await _context.GroupSubjects
                .Where(gs => gs.GroupID == groupId)
                .Include(gs => gs.Subject)
                .Include(gs => gs.Group)
                .ToListAsync();
        }

        public async Task<List<TeacherSubject>> GetSubjectTeachersAsync(int subjectId)
        {
            return await _context.TeacherSubjects
                .Where(ts => ts.SubjectID == subjectId)
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .ToListAsync();
        }

        public async Task AddGroupSubjectAsync(GroupSubject groupSubject)
        {
            _context.GroupSubjects.Add(groupSubject);
            await _context.SaveChangesAsync();
        }

        public async Task AddTeacherSubjectAsync(TeacherSubject teacherSubject)
        {
            _context.TeacherSubjects.Add(teacherSubject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroupSubjectsAsync(List<GroupSubject> items)
        {
            foreach (var item in items)
            {
                var existing = await _context.GroupSubjects.FindAsync(item.GroupID, item.SubjectID);
                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(item);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeacherSubjectsAsync(List<TeacherSubject> items)
        {
            foreach (var item in items)
            {
                var existing = await _context.TeacherSubjects.FindAsync(item.TeacherID, item.SubjectID);
                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(item);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
