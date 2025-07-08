using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.Models;
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

        private async void Confirm_Click(object sender, EventArgs e)
        {

            // 验证课程名称输入
            if (string.IsNullOrWhiteSpace(CourseNameBox.Text.Trim()))
            {
                MessageBox.Show("请输入课程名称", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }
            // 验证课程Id输入,只能为数字
            if (string.IsNullOrWhiteSpace(CourseIdBox.Text.Trim()) || !int.TryParse(CourseIdBox.Text.Trim(), out _))
            {
                MessageBox.Show("请输入有效的课程ID", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            // 验证教师ID，只能为数字
            if (string.IsNullOrWhiteSpace(CourseTeacherBox.Text.Trim()) || !int.TryParse(CourseTeacherBox.Text.Trim(), out _))
            {
                MessageBox.Show("请输入有效的教师ID", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            // 创建课程对象
            var course = new Course
            {
                Cid = CourseIdBox.Text.Trim(),
                Cname = CourseNameBox.Text.Trim(),
                Tid = CourseTeacherBox.Text.Trim()
            };
            try
            {
                // 调用服务层方法添加课程
                var res= await _courseService.InsertCourseAsync(course);
                if (!res)
                    return ;
                MessageBox.Show("课程添加成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // 关闭窗口
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加课程失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

        }
    }
}
