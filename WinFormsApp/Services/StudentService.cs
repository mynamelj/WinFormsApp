using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.DataAccess;
using WinFormsApp.DataAccess.Inerface;
using WinFormsApp.Models;
using WinFormsApp.Services.Interface;

namespace WinFormsApp.Services
{
    /// <summary>
    /// 学生服务类，处理与学生相关的业务逻辑
    /// </summary>
    public class StudentService: IStudentService
    {
        // 依赖于数据仓库，但不直接创建它，这为未来的依赖注入和单元测试做好了准备
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// 获取所有学生的业务方法
        /// </summary>
        /// <returns>学生列表</returns>
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            // 目前，这个方法只是简单地调用仓库方法
            // 未来可以在这里添加业务逻辑，比如数据验证、过滤、组合等
            return await _studentRepository.GetAll();
        }

        public async Task<Student> GetStudentByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                // 或者抛出异常
                return null;
            }
            return await _studentRepository.GetByIdAsync(id);
        }

        public  async  Task<IEnumerable<Student>> SearchStudentsAsync(Student criteria)
        {
            // 在这里可以对criteria进行更复杂的业务验证
            return await _studentRepository.SearchAsync(criteria);
        }
    }
}
