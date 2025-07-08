using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WinFormsApp.Models;
using WinFormsApp.Models.DTO;
using WinFormsApp.Services;
using WinFormsApp.Services.Interface;
using WinFormsApp.Views; // 引入服务层命名空间

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // 持有一个服务层对象的引用

        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        [Required]
        private BindingList<Student> students;
        private BindingList<CourseTeacherView> courses;
        // 用于跟踪修改的学生
        private HashSet<Student> modifiedStudents = new HashSet<Student>();
        //检查是否有新学生添加
        private HashSet<Student> newStudents = new HashSet<Student>();
        private HashSet<CourseTeacherView> modifiedCourses = new HashSet<CourseTeacherView>();

        // 让构造函数接收 IStudentService 依赖
        public Form1(IStudentService studentService, ICourseService courseService)
        {
            InitializeComponent();
            _studentService = studentService; // DI容器会自动提供实例
            _courseService = courseService;
        }

        private  async Task LoadStudentsAsync()
        {

            var allStudents = await _studentService.GetStudentAllAsync();
            students = new BindingList<Student>(allStudents.ToList());
            students.ListChanged += Students_ListChanged; // 订阅 ListChanged 事件
            StudentDataGridView.DataSource = students;
            // 如果需要，可以设置DataGridView的列标题等属性
            StudentDataGridView.Columns["sid"].HeaderText = "学生ID";
            StudentDataGridView.Columns["sname"].HeaderText = "学生姓名";
            StudentDataGridView.Columns["sage"].HeaderText = "出生年月";
            StudentDataGridView.Columns["ssex"].HeaderText = "性别";

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
            switch (e.ListChangedType)
            {
                // 当一个现有项的属性被修改时触发
                case ListChangedType.ItemChanged:
                    {
                        Student changedStudent = students[e.NewIndex];
                        // 只有当这个学生不是一个新添加的学生时，才将其视为“被修改”
                        // 这可以防止一个新行在被编辑时同时出现在 newStudents 和 modifiedStudents 中
                        if (!newStudents.Contains(changedStudent))
                        {
                            modifiedStudents.Add(changedStudent);
                        }
                        break;
                    }

                // 当一个新项被添加到列表时触发 (例如，在DataGridView的最后一行输入数据)
                case ListChangedType.ItemAdded:
                    {
                        Student newStudent = students[e.NewIndex];
                        newStudents.Add(newStudent);
                        break;
                    }
            }
        }

        private void StudentDeleteBtn_Click(object sender, EventArgs e)
        {
            // 分别是：提示信息、标题、默认值
            string inputId = Interaction.InputBox("请输入要删除的学生ID：", "删除确认", "");
            if (!string.IsNullOrEmpty(inputId))
            {
                // 在这里执行您的删除逻辑
                // 为了安全，最好验证一下输入的是否为有效数字
                if (int.TryParse(inputId, out int idToDelete))
                {
                    MessageBox.Show($"准备删除ID为 {idToDelete} 的记录！");
                    // 在此调用您的删除服务或方法
                    _studentService.DeleteStudentAsync(inputId);
                }
                else
                {
                    MessageBox.Show("请输入有效的数字ID。", "输入无效", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("操作已取消。");
            }
        }

        // 正确的 Form1 按钮事件代码
        private async void UpdateStudentBtn_Click(object sender, EventArgs e)
        {
            this.StudentDataGridView.EndEdit();
            this.Validate();

            // 检查是否有任何需要保存的更改（无论是新增还是修改）
            if (!newStudents.Any() && !modifiedStudents.Any())
            {
                MessageBox.Show("没有检测到任何数据更改。");
                return;
            }

            try
            {
                // 关键：调用统一的保存方法，将两个集合都传过去
                bool success = await _studentService.SaveChangesAsync(newStudents, modifiedStudents);

                if (success)
                {
                    MessageBox.Show("数据保存成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 操作成功后，清空两个跟踪集合
                    newStudents.Clear();
                    modifiedStudents.Clear();
                    await LoadStudentsAsync();
                    // （可选但推荐）重新加载数据，以获取新插入记录的自增ID
                    // await LoadStudentsAsync(); 
                }
                else
                {
                    MessageBox.Show("保存失败，没有行受到影响或发生未知错误。", "失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存数据时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private async void CourseQueryBtn_Click(object sender, EventArgs e)
        {
            var criteria = new CourseTeacherView
            {
                Cid= CourseIdtextBox.Text.Trim(),
                Cname= CourseNametextBox.Text.Trim(),
                Tid= TeacherIdtextBox.Text.Trim(),
            };
            try
            {
                // 2. 调用服务层的统一查询方法
                 var cos = (await _courseService.SearchCoursesAsync(criteria)).ToList();
                courses = new BindingList<CourseTeacherView>(cos); // 将 IEnumerable 转换为 IList
                // 3. 绑定结果到DataGridView
                CoursedataGridView.DataSource = courses;
                // 如果需要，可以设置DataGridView的列标题等属性
                CoursedataGridView.Columns["Cid"].HeaderText = "课程ID";
                CoursedataGridView.Columns["Cname"].HeaderText = "课程名";
                CoursedataGridView.Columns["Tid"].HeaderText = "教师ID";
                CoursedataGridView.Columns["Tname"].HeaderText = "教师名";

                // (可选) 提示查询结果数量
                MessageBox.Show($"查询到 {courses.Count()} 条记录。", "查询完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}