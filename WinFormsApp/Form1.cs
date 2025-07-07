using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WinFormsApp.Models;
using WinFormsApp.Services;
using WinFormsApp.Services.Interface;
using WinFormsApp.Views; // 引入服务层命名空间

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // 持有一个服务层对象的引用

        private readonly IStudentService _studentService;
        private BindingList<Student> students;
        private HashSet<Student> modifiedStudents = new HashSet<Student>();

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
                sage = StudentBirthdaytextBox.Enabled && DateTime.TryParse(StudentBirthdaytextBox.Text.Trim(), out var date) ? date : (DateTime?)null,
                ssex = StudentGendertextBox.Text.Trim()
            };
            try
            {
                // 2. 调用服务层的统一查询方法
                var stus = (await _studentService.SearchStudentsAsync(criteria)).ToList();
                students = new BindingList<Student>(stus); // 将 IEnumerable 转换为 IList
                students.ListChanged += Students_ListChanged;
                // 3. 绑定结果到DataGridView
                StudentDataGridView.DataSource = students;
                // 如果需要，可以设置DataGridView的列标题等属性
                StudentDataGridView.Columns["sid"].HeaderText = "学生ID";
                StudentDataGridView.Columns["sname"].HeaderText = "学生姓名";
                StudentDataGridView.Columns["sage"].HeaderText = "出生年月";
                StudentDataGridView.Columns["ssex"].HeaderText = "性别";

                // (可选) 提示查询结果数量
                MessageBox.Show($"查询到 {students.Count()} 条记录。", "查询完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 这是 ListChanged 事件的处理程序
        private void Students_ListChanged(object sender, ListChangedEventArgs e)
        {
            // 我们只关心项本身的属性变更
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                // 获取被修改的 Product 对象并将其添加到 HashSet 中
                Student changedProduct = students[e.NewIndex];
                modifiedStudents.Add(changedProduct);
            }
        }

        private void StudentDeleteBtn_Click(object sender, EventArgs e)
        {

        }

        // 正确的 Form1 按钮事件代码
        private async void UpdateStudentBtn_Click(object sender, EventArgs e)
        {
            this.StudentDataGridView.EndEdit();
            this.Validate();

            if (!modifiedStudents.Any())
            {
                MessageBox.Show("没有检测到任何数据更改。");
                return;
            }

            try
            {
                // 关键：使用 await 等待异步方法完成，并获取返回结果
                bool success = await _studentService.UpdateStudentAsync(modifiedStudents);

                if (success)
                {
                    MessageBox.Show("学生信息更新成功。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    modifiedStudents.Clear(); // 只有在真正成功后才清空集合
                }
                else
                {
                    MessageBox.Show("更新失败，没有行受到影响或发生未知错误。", "更新失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更新学生信息时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void StudentAddBtn_Click(object sender, EventArgs e)
        {

            using (AddStudentWindow addStudentWindow = new AddStudentWindow(_studentService))
            {
                // 显示添加学生信息的窗口
                DialogResult result = addStudentWindow.ShowDialog();
            }

        }

        private void StudentIdTextChanged(object sender, EventArgs e)
        {
            //bool hasId = !string.IsNullOrWhiteSpace(StudentIdtextBox.Text);
            //StudentNametextBox.Enabled = hasId;
            //StudnetGendertextBox.Enabled = hasId;
            //StduentBirthdaytextBox.Enabled = hasId;
            //if (hasId)
            //{
            //    StudentNametextBox.Clear();
            //    StudnetGendertextBox.Clear();
            //    StduentBirthdaytextBox.Clear(); // 清空选择
            //}

        }

        private void flowLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}