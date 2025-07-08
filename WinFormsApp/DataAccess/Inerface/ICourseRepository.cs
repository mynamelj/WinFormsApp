using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;
using WinFormsApp.Models.DTO;

namespace WinFormsApp.DataAccess.Inerface
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseTeacherView>> SearchAsync(CourseTeacherView criteria);
        Task<bool> SaveChangesAsync(IEnumerable<CourseTeacherView> studentsToInsert, IEnumerable<CourseTeacherView> studentsToUpdate);
        Task<bool> InsertAsync(Course course); // 插入新课程
    }
}
