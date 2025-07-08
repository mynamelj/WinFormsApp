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

        public  async Task<bool> DeleteStudentAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                // 或者抛出异常
                return false;
            }
            return await _studentRepository.DeleteAsync(id);

        }


        /// <summary>
        /// 获取所有学生的业务方法
        /// </summary>
        /// <returns>学生列表</returns>

        public async Task<IEnumerable<Student>> GetStudentAllAsync()
        {

            return await _studentRepository.GetAllAsync();
        }

        public async Task<bool> InsertStudentAsync(Student student)
        {
            return await _studentRepository.InsertAsync(student);
        }


        public async Task<bool> SaveChangesAsync(IEnumerable<Student> studentsToInsert, IEnumerable<Student> studentsToUpdate)
        {
            // 直接调用仓储层的统一保存方法
            return await _studentRepository.SaveChangesAsync(studentsToInsert, studentsToUpdate);
        }

        public  async  Task<IEnumerable<Student>> SearchStudentsAsync(Student criteria)
        {
            // 在这里可以对criteria进行更复杂的业务验证
            return await _studentRepository.SearchAsync(criteria);
        }

        public async  Task<bool> UpdateStudentAsync(HashSet<Student> students)
        {
            if (students == null || students.Count == 0)
            {
                // 或者抛出异常
                return false;
            }
            return await _studentRepository.UpdateAsync(students);
        }




    }
}
