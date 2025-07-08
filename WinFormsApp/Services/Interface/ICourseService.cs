using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;
using WinFormsApp.Models.DTO;

namespace WinFormsApp.Services.Interface
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseTeacherView>> SearchCoursesAsync(CourseTeacherView criteria);

        // 新增课程
        Task<bool> InsertCourseAsync(Course course);
    }
}
