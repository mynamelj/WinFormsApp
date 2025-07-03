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

            // ������ע����ķ��������
            // ����ʹ�� AddTransient����ʾÿ������ʱ������һ���µ�ʵ��
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentService, StudentService>();

            // �Ѵ��屾��Ҳע���ȥ������DI�������ܴ�����
            services.AddTransient<Form1>();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var mainForm = serviceProvider.GetService<Form1>();
            // 3. ���������������Ĵ���ʵ��
            //ApplicationConfiguration.Initialize();
            Application.Run(mainForm);
        }
    }
}