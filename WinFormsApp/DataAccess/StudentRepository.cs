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
        public async Task<bool> DeleteAsync(string id)
        {
            using (var connection = DbConnectionFactory.GetConnection())
            {
                string sql = "DELETE FROM Student WHERE sid = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id }); 
                return affectedRows > 0;
            }
        }

        /// <summary>
        /// 从数据库中获取所有学生的信息
        /// </summary>
        /// <returns>学生列表</returns>
        /// 


        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            using (var connection = DbConnectionFactory.GetConnection())
            {
                // SQL查询语句，使用 @Id 作为参数占位符
                string sql = "SELECT sid, sname, sage, ssex FROM student";

                // 使用Dapper的 QueryFirstOrDefaultAsync 方法
                // 它会返回查询到的第一条记录，如果没有找到则返回 null
                // 第二个参数是一个匿名对象，其属性名(@后面的部分)应与SQL中的参数名匹配
                var student = await connection.QueryAsync<Student>(sql);
                return student;
            }
        }

        public  async Task<bool> InsertAsync(Student student)
        {
            using (var connection = DbConnectionFactory.GetConnection())
            {
                string sql = "INSERT INTO Student( sname, sage, ssex) VALUES( @sname, @sage, @ssex)";
                var affectedRows = await connection.ExecuteAsync(sql, student);
                return affectedRows > 0;

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

        // 修改 UpdateAsync 方法中的事务提交和回滚代码
        public async Task<bool> UpdateAsync(HashSet<Student> students)
        {
            using (var connection = DbConnectionFactory.GetConnection())
            {
                string sql = @"UPDATE Student 
                               SET sname = @sname, 
                                   sage = @sage, 
                                   ssex = @ssex 
                               WHERE sid = @sid";

                // 使用同步事务方法
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 使用异步执行
                        int affectedRows = await connection.ExecuteAsync(sql, students, transaction: transaction);

                        transaction.Commit(); // 使用同步 Commit 方法

                        return affectedRows > 0;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // 使用同步 Rollback 方法
                        throw; // 重新抛出异常
                    }
                }
            }
        }
        public async Task<bool> SaveChangesAsync(IEnumerable<Student> studentsToInsert, IEnumerable<Student> studentsToUpdate)
        {
            using (var connection = DbConnectionFactory.GetConnection())
            {
                using (var transaction =  connection.BeginTransaction())
                {
                    try
                    {
                        // 定义插入和更新的SQL语句
                        string insertSql = "INSERT INTO Student(sname, sage, ssex) VALUES(@sname, @sage, @ssex)";
                        string updateSql = "UPDATE Student SET sname = @sname, sage = @sage, ssex = @ssex WHERE sid = @sid";

                        // 1. 批量执行插入操作
                        if (studentsToInsert != null && studentsToInsert.Any())
                        {
                            await connection.ExecuteAsync(insertSql, studentsToInsert, transaction);
                        }

                        // 2. 批量执行更新操作
                        if (studentsToUpdate != null && studentsToUpdate.Any())
                        {
                            await connection.ExecuteAsync(updateSql, studentsToUpdate, transaction);
                        }

                        // 3. 如果所有操作都成功，提交事务
                         transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        // 4. 如果任何一个操作失败，回滚整个事务
                        transaction.Rollback();
                        throw; // 向上抛出异常，让上层知道操作失败了
                    }
                }
            }
        }

        // 未来可在这里扩展新功能，例如：
        // public Student GetById(string sid) { ... }
        // public void Add(Student student) { ... }
        // public void Update(Student student) { ... }
        // public void Delete(string sid) { ... }
    }
}
