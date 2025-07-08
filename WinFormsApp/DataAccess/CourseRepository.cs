using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.DataAccess.Inerface;
using WinFormsApp.Models;
using WinFormsApp.Models.DTO;

namespace WinFormsApp.DataAccess
{
    using Dapper;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CourseRepository : ICourseRepository
    {
        public async Task<bool> InsertAsync(Course course)
        {
            using( var connection = DbConnectionFactory.GetConnection())
            {
                // 定义插入的SQL语句
                string sql = "INSERT INTO Course(Cid, Cname, TId) VALUES(@Cid, @Cname, @TId)";
                
                // 执行插入操作
                int rowsAffected = await connection.ExecuteAsync(sql, course);
                
                // 如果影响的行数大于0，表示插入成功
                return rowsAffected > 0;
            }


        }

        public  async Task<bool> SaveChangesAsync(IEnumerable<CourseTeacherView> coursesToInsert, IEnumerable<CourseTeacherView> coursesToUpdate)
        {
            using (var connection = DbConnectionFactory.GetConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 定义插入和更新的SQL语句
                        string insertSql = "INSERT INTO Course(Cid, Cname, TId) VALUES(@Cid, @Cname, @TId)";
                        string updateSql = "UPDATE Courset SET Cname = @Cname, TId = @TId WHERE Cid = @Cid,";

                        // 1. 批量执行插入操作
                        if (coursesToInsert != null && coursesToInsert.Any())
                        {
                            await connection.ExecuteAsync(insertSql, coursesToInsert, transaction);
                        }

                        // 2. 批量执行更新操作
                        if (coursesToUpdate != null && coursesToUpdate.Any())
                        {
                            await connection.ExecuteAsync(updateSql, coursesToUpdate, transaction);
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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CourseTeacherView>> SearchAsync(CourseTeacherView criteria)
        {
            // 修正1：为表设置别名(c 和 t)，并用别名限定所有列，避免歧义
            var sqlBuilder = new StringBuilder(@"
            SELECT 
                c.Cid, 
                c.Cname, 
                t.Tid, 
                t.Tname 
            FROM 
                Course AS c 
            JOIN 
                Teacher AS t ON c.Tid = t.Tid"); // ON子句中的列也使用别名

            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            // 规则：如果课程ID存在，则只按ID精确查询
            if (!string.IsNullOrWhiteSpace(criteria.Cid))
            {
                // 修正2：字段名从 sid 改为 c.Cid，参数名也对应修改
                conditions.Add("c.Cid = @Cid");
                parameters.Add("Cid", criteria.Cid);
            }
            else // 否则，组合其他条件
            {
                if (!string.IsNullOrWhiteSpace(criteria.Cname))
                {
                    // 修正3：字段名从 sname 改为 c.Cname，参数名也对应修改
                    conditions.Add("c.Cname LIKE @Cname");
                    parameters.Add("Cname", $"%{criteria.Cname}%");
                }
                if (!string.IsNullOrWhiteSpace(criteria.Tid))
                {
                    // 修正4：整个条件都已修正，按教师ID精确查询
                    conditions.Add("t.Tid = @Tid");
                    parameters.Add("Tid", criteria.Tid);
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
                return await connection.QueryAsync<CourseTeacherView>(sqlBuilder.ToString(), parameters);
            }
        }
    }
}
