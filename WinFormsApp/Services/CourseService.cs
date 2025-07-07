using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.DataAccess.Inerface;
using WinFormsApp.Models.DTO;
using WinFormsApp.Services.Interface;

namespace WinFormsApp.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IEnumerable<CourseTeacherView>> SearchCoursesAsync(CourseTeacherView criteria)
        {
            // 调用仓储层的搜索方法，传入查询条件
            return await _courseRepository.SearchAsync(criteria);

        }
    }
}
