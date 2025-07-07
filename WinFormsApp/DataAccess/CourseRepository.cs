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
