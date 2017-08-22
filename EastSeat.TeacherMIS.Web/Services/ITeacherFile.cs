using System;
using System.Threading.Tasks;

namespace EastSeat.TeacherMIS.Web.Services
{
    public interface ITeacherFile
    {
        Task LogAccess(string username, Guid teacherId);
        Task LogUpdate(string username, Guid teacherId);
        Task LogSubjectsTaughtAccess(string username, Guid teacherId);
        Task LogAddingSubjectTaught(string username, Guid teacherId, string subject);
        Task LogTimelineAccess(string username, Guid teacherId);
        Task LogRegistration(string username, Guid teacherId, string recordDetails);
    }
}