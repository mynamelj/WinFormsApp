using Microsoft.Extensions.DependencyInjection;
using WinFormsApp.DataAccess;
using WinFormsApp.DataAccess.Inerface;
using WinFormsApp.Services;
using WinFormsApp.Services.Interface;

namespace WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var services = new ServiceCollection();

            // 在这里注册你的服务和依赖
            // 我们使用 AddTransient，表示每次请求时都创建一个新的实例
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentService, StudentService>();

            // 把窗体本身也注册进去，这样DI容器才能创建它
            services.AddTransient<Form1>();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var mainForm = serviceProvider.GetService<Form1>();
            // 3. 运行由容器创建的窗体实例
            //ApplicationConfiguration.Initialize();
            Application.Run(mainForm);
        }
    }
}