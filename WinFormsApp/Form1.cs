using Dapper;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WinFormsApp.DataAccess;
using WinFormsApp.DataAccess.Inerface;
using WinFormsApp.Models;
using WinFormsApp.Models.DTO;
using WinFormsApp.Services;
using WinFormsApp.Services.Interface;
using WinFormsApp.Utils;
using WinFormsApp.Views; // 引入服务层命名空间

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // 持有一个服务层对象的引用

        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;


        private BindingList<Student> students = new BindingList<Student>();

        //private BindingList<Student> students = new BindingList<Student>();
        private BindingList<CourseTeacherView> courses = new BindingList<CourseTeacherView>();
        private BindingList<Teacher> teachers = new BindingList<Teacher>();
        // 用于跟踪修改的学生
        private HashSet<Student> modifiedStudents = new HashSet<Student>();
        private HashSet<Student> newStudents = new HashSet<Student>();
        private HashSet<Teacher> modifiedTeachers = new HashSet<Teacher>();
        // 用于跟踪修改的课程
        private HashSet<CourseTeacherView> modifiedCourses = new HashSet<CourseTeacherView>();
        private HashSet<CourseTeacherView> newCourses = new HashSet<CourseTeacherView>();
        private HashSet<Teacher> newTeachers = new HashSet<Teacher>();

        // 让构造函数接收 IStudentService 依赖
        public Form1(IStudentService studentService,
                     ICourseService courseService,
                     ITeacherService teacherService)
        {
            InitializeComponent();
            _studentService = studentService; // DI容器会自动提供实例
            _courseService = courseService;
            _teacherService = teacherService;
            StudentGendercomboBox.SelectedIndex = 2;// 默认选择第一个选项

            var StucheckColumn = new SelectCheckBoxColumn();
            if (!StudentDataGridView.Columns.Contains("colCheck"))
            {
                StudentDataGridView.Columns.Insert(0, StucheckColumn);
            }
            StudentDataGridView.DataSource = students;
            // 可以设置DataGridView的列标题等属性
            StudentDataGridView.Columns["sid"].HeaderText = "学生ID";
            StudentDataGridView.Columns["sname"].HeaderText = "学生姓名";
            StudentDataGridView.Columns["sage"].HeaderText = "出生年月";
            StudentDataGridView.Columns["ssex"].HeaderText = "性别";
            var CoursecheckColumn = new SelectCheckBoxColumn();
            if (!CourseDataGridView.Columns.Contains("colCheck"))
            {
                CourseDataGridView.Columns.Insert(0, CoursecheckColumn);
            }
            CourseDataGridView.DataSource = courses;
            // 可以设置DataGridView的列标题等属性
            CourseDataGridView.Columns["Cid"].HeaderText = "课程ID";
            CourseDataGridView.Columns["Cname"].HeaderText = "课程名";
            CourseDataGridView.Columns["Tid"].HeaderText = "教师ID";
            CourseDataGridView.Columns["Tname"].HeaderText = "教师名";

            var TeachercheckColumn = new SelectCheckBoxColumn();
            if (!TeacherDataGridView.Columns.Contains("colCheck"))
            {
                TeacherDataGridView.Columns.Insert(0, TeachercheckColumn);
            }
            TeacherDataGridView.DataSource = teachers;
            TeacherDataGridView.Columns["Tid"].HeaderText = "教师ID";
            TeacherDataGridView.Columns["Tname"].HeaderText = "教师姓名";
        }


        //  ListChanged 事件的处理程序,监听学生列表的变化
        #region Student控件的事件处理
        private async Task LoadStudentsAsync()
        {

            var allStudents = await _studentService.GetStudentAllAsync();
            students = new BindingList<Student>(allStudents.ToList());
            students.ListChanged += Students_ListChanged; // 订阅 ListChanged 事件
            StudentDataGridView.DataSource = students;

        }
        private async void StudentDeleteBtn_Click(object sender, EventArgs e)
        {
            var idsToDelete = new List<string>();
            foreach (DataGridViewRow row in StudentDataGridView.Rows)
            {
                // 获取第一列的复选框单元格
                DataGridViewCheckBoxCell chkCell = row.Cells["colCheck"] as DataGridViewCheckBoxCell;

                // 检查单元格是否有效且被选中 (Value可能为null)
                if (chkCell?.Value != null && (bool)chkCell.Value == true)
                {
                    // 获取该行绑定的数据对象的ID
                    string studentId = row.Cells["Sid"].Value.ToString();
                    idsToDelete.Add(studentId);
                }
            }
            if (idsToDelete.Count == 0)
            {
                MessageBox.Show("请至少选择一个要删除的行。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var confirmResult = MessageBox.Show($"您确定要删除选中的 {idsToDelete.Count} 条记录吗？\n此操作不可恢复。",
                                "确认删除",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            // 禁用按钮，防止重复点击
            StudentDeleteBtn.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 使用CancellationTokenSource设置超时
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30)))
                {
                    foreach (var id in idsToDelete)
                    {
                        // 检查是否已取消
                        cts.Token.ThrowIfCancellationRequested();
                        
                        // 调用服务层的删除方法
                        bool success = await _studentService.DeleteStudentAsync(id).ConfigureAwait(false);
                        if (!success)
                        {
                            // 切换回UI线程显示消息
                            this.Invoke(() => MessageBox.Show($"删除学生ID {id} 失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error));
                            return; // 如果有任何一条删除失败，则终止操作
                        }
                    }
                    
                    // 切换回UI线程更新界面
                    this.Invoke(() => MessageBox.Show("删除操作完成", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information));
                }
            }
            catch (OperationCanceledException)
            {
                this.Invoke(() => MessageBox.Show("操作超时，请检查网络连接。", "超时", MessageBoxButtons.OK, MessageBoxIcon.Warning));
            }
            catch (Exception ex)
            {
                // 检查是否是网络相关异常
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                    errorMessage += $"\n详细信息: {ex.InnerException.Message}";
                
                this.Invoke(() => MessageBox.Show($"删除过程中发生错误: {errorMessage}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                // 恢复UI状态
                this.Invoke(() =>
                {
                    StudentDeleteBtn.Enabled = true;
                    this.Cursor = Cursors.Default;
                });
            }
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
                ssex = StudentGendercomboBox.SelectedItem.ToString() == " " ? null : StudentGendercomboBox.SelectedItem.ToString()

            };
            try
            {
                // 2. 调用服务层的统一查询方法
                var stus = (await _studentService.SearchStudentsAsync(criteria)).ToList();
                students = new BindingList<Student>(stus); // 将 IEnumerable 转换为 IList
                students.ListChanged += Students_ListChanged;
                // 3. 绑定结果到DataGridView
                StudentDataGridView.DataSource = students;
                // (可选) 提示查询结果数量
                MessageBox.Show($"查询到 {students.Count()} 条记录。", "查询完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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
        private void StudentAddBtn_Click(object sender, EventArgs e)
        {

            using (AddStudentWindow addStudentWindow = new AddStudentWindow(_studentService))
            {
                // 显示添加学生信息的窗口
                DialogResult result = addStudentWindow.ShowDialog();
            }

        }
        #endregion

        #region Course控件的事件处理
        private async void CourseQueryBtn_Click(object sender, EventArgs e)
        {
            var criteria = new CourseTeacherView
            {
                Cid = CourseIdtextBox.Text.Trim(),
                Cname = CourseNametextBox.Text.Trim(),
                Tid = TeacherIdtextBox.Text.Trim(),
            };
            try
            {
                // 2. 调用服务层的统一查询方法
                var cos = (await _courseService.SearchCoursesAsync(criteria)).ToList();
                courses = new BindingList<CourseTeacherView>(cos); // 将 IEnumerable 转换为 IList
                courses.ListChanged += Courses_ListChanged; // 订阅 ListChanged 事件
                // 3. 绑定结果到DataGridView
                CourseDataGridView.DataSource = courses;
                // (可选) 提示查询结果数量
                MessageBox.Show($"查询到 {courses.Count()} 条记录。", "查询完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void CourseAddBtn_Click(object sender, EventArgs e)
        {
            using (AddCourseWindow addCourseWindow = new AddCourseWindow(_courseService))
            {
                using (var connection = DbConnectionFactory.GetConnection())
                {
                    try
                    {
                        const string sql = "SELECT CAST(Tid AS NVARCHAR(50)) FROM Teacher;";
                        var res = await connection.QueryAsync<string>(sql);
                        addCourseWindow.TeachercomboBox.DataSource = res.ToList();// 使用实例对象

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"获取教师列表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                // 显示添加课程信息的窗口
                DialogResult result = addCourseWindow.ShowDialog();
            }
        }
        private async void CourseUpdateBtn_Click(object sender, EventArgs e)
        {
            CourseDataGridView.EndEdit();
            this.Validate();

            // 检查是否有任何需要保存的更改（无论是新增还是修改）
            if (!newCourses.Any() && !modifiedCourses.Any())
            {
                MessageBox.Show("没有检测到任何数据更改。");
                return;
            }

            try
            {
                // 关键：调用统一的保存方法，将两个集合都传过去
                bool success = await _courseService.UpdateCourseAsync(newCourses, modifiedCourses);

                if (success)
                {
                    MessageBox.Show("课程数据保存成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 操作成功后，清空两个跟踪集合
                    newCourses.Clear();
                    modifiedCourses.Clear();

                    // （可选但推荐）重新加载数据，以获取新插入记录的自增ID
                    // await LoadCoursesAsync();
                }
                else
                {
                    MessageBox.Show("保存失败，没有行受到影响或发生未知错误。", "失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存课程数据时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CourseDeleteBtn_Click(object sender, EventArgs e)
        {
            var idsToDelete = new List<string>();
            foreach (DataGridViewRow row in CourseDataGridView.Rows)
            {
                // 获取第一列的复选框单元格
                DataGridViewCheckBoxCell chkCell = row.Cells["colCheck"] as DataGridViewCheckBoxCell;
                // 检查单元格是否有效且被选中 (Value可能为null)
                if (chkCell?.Value != null && (bool)chkCell.Value == true)
                {
                    // 获取该行绑定的数据对象的ID
                    string courseId = row.Cells["Cid"].Value.ToString();
                    idsToDelete.Add(courseId);
                }

            }
            if (idsToDelete.Count == 0)
            {
                MessageBox.Show("请至少选择一个要删除的行。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var confirmResult = MessageBox.Show($"您确定要删除选中的 {idsToDelete.Count} 条记录吗？\n此操作不可恢复。",
                                "确认删除",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.No)
            {
                return;
            }
            try
            {
                foreach (var id in idsToDelete)
                {
                    // 调用服务层的删除方法
                    bool success = await _courseService.DeleteCourseAsync(id);
                    if (!success)
                    {
                        MessageBox.Show($"删除课程ID {id} 失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // 如果有任何一条删除失败，则终止操作
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除过程中发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        #endregion

        #region Teacher控件的事件处理

        private async void TeacherUpdateBtn_Click(object sender, EventArgs e)
        {
            TeacherDataGridView.EndEdit();
            this.Validate();
            // 检查是否有任何需要保存的更改（无论是新增还是修改）
            if (!newTeachers.Any() && !modifiedTeachers.Any())
            {
                MessageBox.Show("没有检测到任何数据更改。");
                return;
            }
            try
            {
                // 关键：调用统一的保存方法，将两个集合都传过去
                bool success = await  _teacherService.SaveChangesAsync(newTeachers, modifiedTeachers);
                if (success)
                {
                    MessageBox.Show("教师数据保存成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 操作成功后，清空两个跟踪集合
                    newTeachers.Clear();
                    modifiedTeachers.Clear();
                    // （可选但推荐）重新加载数据，以获取新插入记录的自增ID
                    // await LoadTeachersAsync();
                }
                else
                {
                    MessageBox.Show("保存失败，没有行受到影响或发生未知错误。", "失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存教师数据时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void TeacherAddBtn_Click(object sender, EventArgs e)
        {
            // 显示添加教师信息的窗口
            using (AddTeacherWindow addTeacherWindow = new AddTeacherWindow(_teacherService))
            {
                DialogResult result = addTeacherWindow.ShowDialog();
            }
        }

        private async void TeacherQueryBtn_Click(object sender, EventArgs e)
        {
            var criteria = new Teacher
            {
                Tid = TidtextBox.Text.Trim(),
                Tname = TnametextBox.Text.Trim(),
            };
            try
            {
                // 2. 调用服务层的统一查询方法
                var teachersList = (await _teacherService.SearchAsync(criteria)).ToList();
                teachers = new BindingList<Teacher>(teachersList); // 将 IEnumerable 转换为 IList
                teachers.ListChanged += Teachers_ListChanged; // 订阅 ListChanged 事件
                // 3. 绑定结果到DataGridView
                TeacherDataGridView.DataSource = teachers;
                // (可选) 提示查询结果数量
                MessageBox.Show($"查询到 {teachers.Count()} 条记录。", "查询完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region 监听DataGridView的复选框列
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
        // Update the method signature to allow nullable reference types
        private void Courses_ListChanged(object? sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    {
                        CourseTeacherView changedCourse = courses[e.NewIndex];
                        if (!newCourses.Contains(changedCourse))
                        {
                            modifiedCourses.Add(changedCourse);
                        }
                        break;
                    }
                case ListChangedType.ItemAdded:
                    {
                        CourseTeacherView newCourse = courses[e.NewIndex];
                        newCourses.Add(newCourse);
                        break;
                    }
            }
        }

        private void Teachers_ListChanged(object? sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    {
                        Teacher changedTeacher = teachers[e.NewIndex];
                        if (!newTeachers.Contains(changedTeacher))
                        {
                            modifiedTeachers.Add(changedTeacher);
                        }
                        break;
                    }
                case ListChangedType.ItemAdded:
                    {
                        Teacher newTeacher = teachers[e.NewIndex];
                        newTeachers.Add(newTeacher);
                        break;
                    }
            }
        }
        #endregion




        private void TeacherDeleteBtn_Click(object sender, EventArgs e)
        {

        }

    }
}