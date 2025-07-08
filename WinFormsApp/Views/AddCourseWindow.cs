using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.Services.Interface;

namespace WinFormsApp.Views
{
    public partial class AddCourseWindow : Form
    {
        private readonly ICourseService _courseService;
        public AddCourseWindow(ICourseService courseService)
        {
            InitializeComponent();
            _courseService = courseService; // DI容器会自动提供实例
        }
    }
}
