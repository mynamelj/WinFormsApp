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
        // 批量保存课程（包括插入和更新）
        Task<bool> UpdateCourseAsync(IEnumerable<CourseTeacherView> coursesToInsert, IEnumerable<CourseTeacherView> coursesToUpdate);
        // 删除课程
        Task<bool> DeleteCourseAsync(string courseId);
    }
}
