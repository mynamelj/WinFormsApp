using Dapper; // 引入Dapper命名空间
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.DataAccess.Inerface;
using WinFormsApp.Models;

namespace WinFormsApp.DataAccess
{
    /// <summary>
    /// 学生数据仓库类，负责所有与学生表相关的数据库操作
    /// </summary>
    public class StudentRepository: IStudentRepository
    {
        /// <summary>
        /// 从数据库中获取所有学生的信息
        /// </summary>
        /// <returns>学生列表</returns>
        /// 

        public async Task<IEnumerable<Student>> GetAll()
        {
            // 'using' 语句确保数据库连接在使用完毕后会被自动关闭和释放，即使发生错误
            using (var connection = DbConnectionFactory.GetConnection())
            {
                // 定义要执行的SQL查询语句
                string sql = "SELECT sid, sname, sage, ssex FROM student";

                // 使用Dapper的Query<T>方法执行查询
                // Dapper会自动将查询结果的每一行映射到一个Student对象中
                // 并返回一个Student对象的集合
                var students =  await connection.QueryAsync<Student>(sql);
                return students;
            }
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            using (var connection = DbConnectionFactory.GetConnection())
            {
                // SQL查询语句，使用 @Id 作为参数占位符
                string sql = "SELECT sid, sname, sage, ssex FROM student WHERE sid = @Id";

                // 使用Dapper的 QueryFirstOrDefaultAsync 方法
                // 它会返回查询到的第一条记录，如果没有找到则返回 null
                // 第二个参数是一个匿名对象，其属性名(@后面的部分)应与SQL中的参数名匹配
                var student = await connection.QueryFirstOrDefaultAsync<Student>(sql, new { Id = id });
                return student;
            }
        }

        public async Task<IEnumerable<Student>> SearchAsync(Student criteria)
        {
            var sqlBuilder = new StringBuilder("SELECT sid, sname, sage, ssex FROM student");
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            // 规则：如果ID存在，则只按ID查询
            if (!string.IsNullOrWhiteSpace(criteria.sid))
            {
                conditions.Add("sid = @Id");
                parameters.Add("Id", criteria.sid);
            }
            else // 否则，组合其他条件
            {
                if (!string.IsNullOrWhiteSpace(criteria.sname))
                {
                    // 使用LIKE实现模糊查询
                    conditions.Add("sname LIKE @Name");
                    parameters.Add("Name", $"%{criteria.sname}%");
                }
                if (criteria.sage.HasValue)
                {
                    // 查询出生日期（只比较年月日部分）
                    conditions.Add("CAST(sage AS DATE) = @BirthDate");
                    parameters.Add("BirthDate", criteria.sage.Value.Date);
                }
                if (!string.IsNullOrWhiteSpace(criteria.ssex))
                {
                    conditions.Add("ssex = @Gender");
                    parameters.Add("Gender", criteria.ssex);
                }
            }

            // 如果有任何查询条件，则拼接WHERE子句
            if (conditions.Any())
            {
                sqlBuilder.Append(" WHERE ");
                sqlBuilder.Append(string.Join(" AND ", conditions));
            }

            using (var connection = DbConnectionFactory.GetConnection())
            {
                return await connection.QueryAsync<Student>(sqlBuilder.ToString(), parameters);
            }
        }

        // 未来可在这里扩展新功能，例如：
        // public Student GetById(string sid) { ... }
        // public void Add(Student student) { ... }
        // public void Update(Student student) { ... }
        // public void Delete(string sid) { ... }
    }
}
