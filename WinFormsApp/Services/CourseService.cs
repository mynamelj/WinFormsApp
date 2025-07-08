using Dapper;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.DataAccess;
using WinFormsApp.DataAccess.Inerface;
using WinFormsApp.Models;
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

        public  async Task<bool> InsertCourseAsync(Course course)
        {
            // 验证课程对象的三个字段都不为空
            if (course == null || string.IsNullOrWhiteSpace(course.Cid) || string.IsNullOrWhiteSpace(course.Cname) || string.IsNullOrWhiteSpace(course.Tid))
            {
                MessageBox.Show("课程编号、课程名称和教师编号不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //检查是否能在Teacher表中找到对应的教师编号，否则提示错误，新增失败
            using (var connection = DbConnectionFactory.GetConnection())
            {
                var teacherExists = await connection.ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM Teacher WHERE Tid = @Tid", new { Tid = course.Tid });
                if (!teacherExists)
                {
                    MessageBox.Show("教师不存在，请检查！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return await _courseRepository.InsertAsync(course);
        }

        public async Task<IEnumerable<CourseTeacherView>> SearchCoursesAsync(CourseTeacherView criteria)
        {
            // 调用仓储层的搜索方法，传入查询条件
            return await _courseRepository.SearchAsync(criteria);

        }
    }
}
