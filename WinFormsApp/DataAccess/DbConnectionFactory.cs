using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

// Add the NuGet package reference for System.Data.SqlClient if not already added.
// You can do this by running the following command in the NuGet Package Manager Console:
// Install-Package System.Data.SqlClient
using Microsoft.Data.SqlClient;

namespace WinFormsApp.DataAccess
{
    /// <summary>
    /// 数据库连接工厂
    /// 集中管理连接字符串，方便未来修改（例如从配置文件读取）
    /// </summary>
    public static class DbConnectionFactory
    {
        // *******************************************************************
        // !! 请在这里修改为你的数据库名 !!
        // *******************************************************************
        private const string DatabaseName = "MyData"; // <-- 请确认此数据库名是否正确

        private static readonly string connectionString = $"Server=43.251.101.161;Database={DatabaseName};User ID=sa;Password=Sa123456!;TrustServerCertificate=True;";

        /// <summary>
        /// 获取一个新的、打开的数据库连接
        /// </summary>
        /// <returns>IDbConnection 接口的实例</returns>
        public static IDbConnection GetConnection()
        {
            // 使用接口（IDbConnection）而不是具体实现（SqlConnection）是良好实践
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}