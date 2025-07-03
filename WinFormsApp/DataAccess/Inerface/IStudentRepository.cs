using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;

namespace WinFormsApp.DataAccess.Inerface
{
    public interface IStudentRepository
    {
         Task<IEnumerable<Student>> GetAll();
         Task<Student> GetByIdAsync(string id); // 返回单个学生对象
         Task<IEnumerable<Student>> SearchAsync(Student criteria);
    }
}
