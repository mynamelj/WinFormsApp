using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.DataAccess.Inerface;
using WinFormsApp.Models;

namespace WinFormsApp.DataAccess
{
    public class TeacherRepository : ITeacherRepository
    {
        public Task<bool> DeleteAsync(string teacherId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Teacher teacher)
        {
            using (var connection = DbConnectionFactory.GetConnection())
            {
                // 检查教师编号是否已存在
                var existingTeacher = await connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM Teacher WHERE Tid = @Tid", new { Tid = teacher.Tid });
                if (existingTeacher > 0)
                {
                    throw new InvalidOperationException("教师编号已存在。");
                }
                // 插入新教师记录
                var result = await connection.ExecuteAsync(
                    "INSERT INTO Teacher (Tid, Tname) VALUES (@Tid, @Tname)",
                    new { teacher.Tid, teacher.Tname });
                return result > 0;
            }
        }

        public async Task<bool> SaveChangesAsync(IEnumerable<Teacher> teachersToInsert, IEnumerable<Teacher> teachersToUpdate)
        {
            using (var connection = DbConnectionFactory.GetConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 定义插入和更新的SQL语句
                        string insertSql = "INSERT INTO Teacher(Tid,Tname) VALUES(@Tid,@Tname)";
                        string updateSql = "UPDATE Teacher SET Tname = @Tname WHERE Tid = @Tid";

                        // 1. 批量执行插入操作
                        if (teachersToInsert != null && teachersToInsert.Any())
                        {
                            await connection.ExecuteAsync(insertSql, teachersToInsert, transaction);
                        }

                        // 2. 批量执行更新操作
                        if (teachersToInsert != null && teachersToInsert.Any())
                        {
                            await connection.ExecuteAsync(updateSql, teachersToUpdate, transaction);
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

        public async Task<IEnumerable<Teacher>> SearchAsync(Teacher criteria)
        {
            var sqlBuilder = new StringBuilder(@"SELECT Tid, Tname FROM Teacher");
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(criteria.Tid))
            {
                conditions.Add("Tid = @Tid");
                parameters.Add("Tid", criteria.Tid);
            }
            if (!string.IsNullOrWhiteSpace(criteria.Tname))
            {
                conditions.Add("Tname LIKE @Tname");
                parameters.Add("Tname", $"%{criteria.Tname}%");
            }

            if (conditions.Any())
            {
                sqlBuilder.Append(" WHERE " + string.Join(" AND ", conditions));
            }

            using (var connection = DbConnectionFactory.GetConnection())
            {
                // 使用Dapper的QueryAsync方法执行查询
                return await connection.QueryAsync<Teacher>(sqlBuilder.ToString(), parameters);
            }
        }
    }
}
