using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;

namespace WinFormsApp.Services.Interface
{
    public interface ITeacherService
    {
        /// <summary>
        /// 根据条件搜索教师
        /// </summary>
        /// <param name="criteria">搜索条件</param>
        /// <returns>符合条件的教师列表</returns>
        Task<IEnumerable<Teacher>> SearchAsync(Teacher criteria);
        /// <summary>
        /// 保存教师信息
        /// </summary>
        /// <param name="teachersToInsert">需要插入的教师列表</param>
        /// <param name="teachersToUpdate">需要更新的教师列表</param>
        /// <returns>是否保存成功</returns>
        Task<bool> SaveChangesAsync(IEnumerable<Teacher> teachersToInsert, IEnumerable<Teacher> teachersToUpdate);
        /// <summary>
        /// 插入新教师
        /// </summary>
        /// <param name="teacher">新教师信息</param>
        /// <returns>是否插入成功</returns>
        Task<bool> InsertAsync(Teacher teacher);
        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>是否删除成功</returns>
        Task<bool> DeleteAsync(string teacherId);
    }
}
