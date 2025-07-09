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
    public partial class AddTeacherWindow : Form
    {
        //依赖注入
        private readonly ITeacherService _teacherService;

        public AddTeacherWindow(ITeacherService teacherService)
        {
            _teacherService = teacherService;
            InitializeComponent();
        }


        private async void Confirm_Click(object sender, EventArgs e)
        {
            //验证输入
            if (string.IsNullOrWhiteSpace(TeacherIdtextBox.Text.Trim()) || !int.TryParse(TeacherIdtextBox.Text.Trim(), out _))
            {
                MessageBox.Show("请输入有效的ID", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(TeacherNametextBox.Text.Trim()))
            {
                MessageBox.Show("请输入教师姓名", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //创建教师对象
            var teacher = new Teacher
            {
                Tid = TeacherIdtextBox.Text.Trim(),
                Tname = TeacherNametextBox.Text.Trim()
            };
            try
            {
                //调用服务层插入教师
                bool result = await _teacherService.InsertAsync(teacher);
                if (result)
                {
                    MessageBox.Show("教师添加成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // 关闭窗口
                }
                else
                {
                    MessageBox.Show("教师添加失败，请稍后重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
