using System;
using System.Threading.Tasks;
using EastSeat.TeacherMIS.Web.Data;
using EastSeat.TeacherMIS.Web.Models;

namespace EastSeat.TeacherMIS.Web.Services
{
    public class TeacherFileService : ITeacherFile
    {
        private readonly ApplicationDbContext _db;

        public TeacherFileService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public async Task LogAccess(string username, Guid teacherId)
        {
            _db.TeacherFiles.Add(new TeacherFile{
                Details = $"File accessed by {username}",
                RecordDate = DateTime.Now,
                TeacherId = teacherId
            });

            await _db.SaveChangesAsync();
        }

        
        public async Task LogUpdate(string username, Guid teacherId)
        {
            _db.TeacherFiles.Add(new TeacherFile{
                Details = $"File updated by {username}",
                RecordDate = DateTime.Now,
                TeacherId = teacherId
            });

            await _db.SaveChangesAsync();
        }

        public async Task LogSubjectsTaughtAccess(string username, Guid teacherId)
        {
            _db.TeacherFiles.Add(new TeacherFile{
                Details = $"Accessed subjects taught by {username}",
                RecordDate = DateTime.Now,
                TeacherId = teacherId
            });

            await _db.SaveChangesAsync();
        }

        public async Task LogAddingSubjectTaught(string username, Guid teacherId, string subject)
        {
            _db.TeacherFiles.Add(new TeacherFile{
                Details = $"Added subject {subject} on the subjects taught by {username}",
                RecordDate = DateTime.Now,
                TeacherId = teacherId
            });

            await _db.SaveChangesAsync();
        }

        public async Task LogTimelineAccess(string username, Guid teacherId)
        {
            _db.TeacherFiles.Add(new TeacherFile{
                Details = $"Accessed timeline by {username}",
                RecordDate = DateTime.Now,
                TeacherId = teacherId
            });

            await _db.SaveChangesAsync();
        }
    }
}