using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;

namespace WinFormsApp.Services.Interface
{
    public interface IStudentService
    {
        Task<Student> GetStudentByIdAsync(string id);
        Task<IEnumerable<Student>> SearchStudentsAsync(Student criteria);
        Task<bool> InsertStudentAsync(Student student);
    }
}
