using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;
using WinFormsApp.Models.DTO;

namespace WinFormsApp.DataAccess.Inerface
{
    public interface ITeacherRepository
    {

        Task<IEnumerable<Teacher>> SearchAsync(Teacher criteria);
        Task<bool> SaveChangesAsync(IEnumerable<Teacher> studentsToInsert, IEnumerable<Teacher> studentsToUpdate);
        Task<bool> InsertAsync(Teacher course); // 插入新课程
    }
}
