using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WinFormsApp.Models;
using WinFormsApp.Services;
using WinFormsApp.Services.Interface; // 引入服务层命名空间

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // 持有一个服务层对象的引用

        private readonly IStudentService _studentService;

        // 让构造函数接收 IStudentService 依赖
        public Form1(IStudentService studentService)
        {
            InitializeComponent();
            _studentService = studentService; // DI容器会自动提供实例
        }

        private async void StudentQueryBtn_Click(object sender, EventArgs e)
        {
            var criteria = new Student
            {
                sid = StudentIdtextBox.Text.Trim(),
                sname = StudentNametextBox.Text.Trim(),
                // 假设日期控件为DateTimePicker，性别为ComboBox
                // 如果控件未启用或未选择，则条件为null
                sage = StduentBirthdaytextBox.Enabled && DateTime.TryParse(StduentBirthdaytextBox.Text, out var date) ? date : (DateTime?)null,
                ssex = StudnetGendertextBox.Text.Trim()
            };
            try
            {
                // 2. 调用服务层的统一查询方法
                var students = await _studentService.SearchStudentsAsync(criteria);

                // 3. 绑定结果到DataGridView
                StudentDataGridView.DataSource = students.ToList();

                // (可选) 提示查询结果数量
                MessageBox.Show($"查询到 {students.Count()} 条记录。", "查询完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void StudentDeleteBtn_Click(object sender, EventArgs e)
        {

        }

        private void UpdateStudentBtn_Click(object sender, EventArgs e)
        {

        }

        private void StudentAddBtn_Click(object sender, EventArgs e)
        {

        }

        private void StudentIdTextChanged(object sender, EventArgs e)
        {
            bool hasId = string.IsNullOrWhiteSpace(StudentIdtextBox.Text);
            StudentNametextBox.Enabled = hasId;
            StudnetGendertextBox.Enabled = hasId;
            StduentBirthdaytextBox.Enabled = hasId;
            if (hasId)
            {
                StudentNametextBox.Clear();
                StudnetGendertextBox.Clear(); 
                StduentBirthdaytextBox .Clear(); // 清空选择
            }

        }
    }
}