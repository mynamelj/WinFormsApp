using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.DataAccess.Inerface;
using WinFormsApp.Models;
using WinFormsApp.Services.Interface;

namespace WinFormsApp.Services
{
    public class TeacherService : ITeacherService
    {
        // 这里可以注入数据访问层的接口，例如 ITeacherRepository
        private readonly ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public Task<bool> DeleteAsync(string teacherId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Teacher teacher)
        {
            return await _teacherRepository.InsertAsync(teacher);
            
        }

        public Task<bool> SaveChangesAsync(IEnumerable<Teacher> teachersToInsert, IEnumerable<Teacher> teachersToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Teacher>> SearchAsync(Teacher criteria)
        {
            // 调用仓储层的搜索方法，传入查询条件
            return await _teacherRepository.SearchAsync(criteria);
        }
    }
}
