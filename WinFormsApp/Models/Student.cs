using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace WinFormsApp.Models
{
    /// <summary>
    /// 代表学生信息的实体类，对应数据库中的 student 表
    /// </summary>
    public class Student
    {
        // 属性名应与数据库表的列名完全一致，Dapper会自动映射
        // 如果不一致，可以使用 [Column("列名")] 特性来指定

        /// <summary>
        /// 学生编号
        /// </summary>
        public string? sid { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string? sname { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? sage { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string? ssex { get; set; }
    }
}