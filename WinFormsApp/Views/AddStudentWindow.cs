using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.Models;
using WinFormsApp.Services;
using WinFormsApp.Services.Interface;

namespace WinFormsApp.Views
{
    public partial class AddStudentWindow : Form
    {
        private readonly IStudentService _studentService;

        // Constructor updated to fix the errors
        public AddStudentWindow(IStudentService studentService)
        {
            InitializeComponent();
            _studentService = studentService; // DI容器会自动提供实例
        }

        private async void Confirm_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(StudentIdtextBox.Text.Trim()) || !int.TryParse(StudentIdtextBox.Text.Trim(), out _))
            //{
            //    MessageBox.Show("请输入有效的ID", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //验证日期格式
            if (string.IsNullOrWhiteSpace(this.StudentBirthdaytextBox.Text.Trim()) || !DateTime.TryParse(StudentBirthdaytextBox.Text.Trim(), out _))
            {
                MessageBox.Show("请输入有效的出生日期", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 验证性别输入
            if (StudentGendertextBox.Text.Trim() != "男" && StudentGendertextBox.Text.Trim() != "女")
            {
                MessageBox.Show("请输入有效性别", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 验证姓名输入
            if (string.IsNullOrWhiteSpace(StudentNametextBox.Text.Trim()))
            {
                MessageBox.Show("请输入学生姓名", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             Student student = new Student
            {

                sname = StudentNametextBox.Text.Trim(),
                sage = DateTime.Parse(StudentBirthdaytextBox.Text.Trim()),
                ssex = StudentGendertextBox.Text.Trim()
            };
            try
            {
                bool success = await _studentService.InsertStudentAsync(student);
                if (success)
                {
                    MessageBox.Show("学生信息添加成功。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 清空输入框
                    StudentNametextBox.Clear();
                    StudentGendertextBox.Clear();
                    StudentBirthdaytextBox.Clear();
                }
                else
                {
                    MessageBox.Show("添加学生信息失败，请检查输入。", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加学生信息时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

    }
}
