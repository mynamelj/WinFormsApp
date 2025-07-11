﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;

namespace WinFormsApp.DataAccess.Inerface
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync(); // 返回单个学生对象
        Task<IEnumerable<Student>> SearchAsync(Student criteria);
        Task<bool> InsertAsync(Student student); // 插入新学生
        Task<bool> UpdateAsync(HashSet<Student> students);
        Task<bool> DeleteAsync(string id);
        Task<bool> SaveChangesAsync(IEnumerable<Student> studentsToInsert, IEnumerable<Student> studentsToUpdate);
    }
}