using DeansOffice.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeansOffice.Services
{
    public class GroupService
    {
        public List<Group> GetAllGroups()
        {
            var db = new DeanDbContext();

            var groups = db.Groups.ToList();
            return groups;

        }

        public void AddGroup(Group group)
        {
            var db = new DeanDbContext();
            group.Curator = db.Teachers.FirstOrDefault(x=>x.TeacherID==group.CuratorID);
            db.Groups.Add(group);
            db.SaveChanges();
        }

        public void UpdateGroup(Group updatedGroup)
        {
            if (updatedGroup == null)
                throw new ArgumentNullException(nameof(updatedGroup));

            using (var db = new DeanDbContext())
            {
                var existingGroup = db.Groups.FirstOrDefault(x => x.GroupID == updatedGroup.GroupID);

                if (existingGroup == null)
                    throw new ArgumentException($"Group with ID {updatedGroup.GroupID} not found.");

                existingGroup.GroupName = updatedGroup.GroupName;
                existingGroup.Course = updatedGroup.Course;
                existingGroup.Specialty = updatedGroup.Specialty;
                existingGroup.CuratorID = updatedGroup.CuratorID;

                db.SaveChanges();
            }
        }

        public void DeleteGroup(Group group)
        {
            var db = new DeanDbContext();
            var group_to_delete = db.Groups.FirstOrDefault(x => x.GroupID == group.GroupID);

            if (group_to_delete != null)
            {
                db.Groups.Remove(group_to_delete);
                db.SaveChanges();
            }
        }
    }
}
