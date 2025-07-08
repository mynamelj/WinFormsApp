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
using WinFormsApp.Views; // �������������ռ�

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // ����һ���������������

        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        [Required]
        private BindingList<Student> students=new BindingList<Student>();
        private BindingList<CourseTeacherView> courses;
        // ���ڸ����޸ĵ�ѧ��
        private HashSet<Student> modifiedStudents = new HashSet<Student>();
        //����Ƿ�����ѧ�����
        private HashSet<Student> newStudents = new HashSet<Student>();
        private HashSet<CourseTeacherView> modifiedCourses = new HashSet<CourseTeacherView>();
        private HashSet<CourseTeacherView> newCourses = new HashSet<CourseTeacherView>();

        // �ù��캯������ IStudentService ����
        public Form1(IStudentService studentService, ICourseService courseService)
        {
            InitializeComponent();
            _studentService = studentService; // DI�������Զ��ṩʵ��
            _courseService = courseService;
            StudentGendercomboBox.SelectedIndex = 2;
            // Ĭ��ѡ���һ��ѡ��
            var checkColumn = new SelectCheckBoxColumn();
            if (!StudentDataGridView.Columns.Contains("colCheck"))
            {
                StudentDataGridView.Columns.Insert(0, checkColumn);
            }
            StudentDataGridView.DataSource = students;
            // �����Ҫ����������DataGridView���б��������
            StudentDataGridView.Columns["sid"].HeaderText = "ѧ��ID";
            StudentDataGridView.Columns["sname"].HeaderText = "ѧ������";
            StudentDataGridView.Columns["sage"].HeaderText = "��������";
            StudentDataGridView.Columns["ssex"].HeaderText = "�Ա�";
        }


        //  ListChanged �¼��Ĵ������,����ѧ���б�ı仯
        #region Student�ؼ����¼�����
        private async Task LoadStudentsAsync()
        {

            var allStudents = await _studentService.GetStudentAllAsync();
            students = new BindingList<Student>(allStudents.ToList());
            students.ListChanged += Students_ListChanged; // ���� ListChanged �¼�
            StudentDataGridView.DataSource = students;

        }
        private async void StudentDeleteBtn_Click(object sender, EventArgs e)
        {
            var idsToDelete = new List<string>();
            foreach (DataGridViewRow row in StudentDataGridView.Rows)
            {
                // ��ȡ��һ�еĸ�ѡ��Ԫ��
                DataGridViewCheckBoxCell chkCell = row.Cells["colCheck"] as DataGridViewCheckBoxCell;

                // ��鵥Ԫ���Ƿ���Ч�ұ�ѡ�� (Value����Ϊnull)
                if (chkCell?.Value != null && (bool)chkCell.Value == true)
                {
                    // ��ȡ���а󶨵����ݶ����ID
                    string studentId = row.Cells["Sid"].Value.ToString();
                    idsToDelete.Add(studentId);
                }
            }
            if (idsToDelete.Count == 0)
            {
                MessageBox.Show("������ѡ��һ��Ҫɾ�����С�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var confirmResult = MessageBox.Show($"��ȷ��Ҫɾ��ѡ�е� {idsToDelete.Count} ����¼��\n�˲������ɻָ���",
                                "ȷ��ɾ��",
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
                    // ���÷�����ɾ������
                    bool success = await _studentService.DeleteStudentAsync(id);
                    if (!success)
                    {
                        MessageBox.Show($"ɾ��ѧ��ID {id} ʧ�ܡ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // ������κ�һ��ɾ��ʧ�ܣ�����ֹ����
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ɾ�������з�������: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private async void StudentQueryBtn_Click(object sender, EventArgs e, ComboBox studentGendercomboBox)
        {
            var criteria = new Student
            {
                sid = StudentIdtextBox.Text.Trim(),
                sname = StudentNametextBox.Text.Trim(),
                // �������ڿؼ�ΪDateTimePicker���Ա�ΪComboBox
                // ����ؼ�δ���û�δѡ��������Ϊnull
                sage = StudentBirthdaytextBox.Enabled && DateTime.TryParse(StudentBirthdaytextBox.Text.Trim(), out var date) ? date : (DateTime?)null,
                ssex = StudentGendercomboBox.SelectedItem.ToString() == " " ? null : StudentGendercomboBox.SelectedItem.ToString()

            };
            try
            {
                // 2. ���÷�����ͳһ��ѯ����
                var stus = (await _studentService.SearchStudentsAsync(criteria)).ToList();
                students = new BindingList<Student>(stus); // �� IEnumerable ת��Ϊ IList
                students.ListChanged += Students_ListChanged;
                // 3. �󶨽����DataGridView
                StudentDataGridView.DataSource = students;
                // (��ѡ) ��ʾ��ѯ�������
                MessageBox.Show($"��ѯ�� {students.Count()} ����¼��", "��ѯ���");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ѯʱ��������: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void UpdateStudentBtn_Click(object sender, EventArgs e)
        {
            this.StudentDataGridView.EndEdit();
            this.Validate();

            // ����Ƿ����κ���Ҫ����ĸ��ģ����������������޸ģ�
            if (!newStudents.Any() && !modifiedStudents.Any())
            {
                MessageBox.Show("û�м�⵽�κ����ݸ��ġ�");
                return;
            }

            try
            {
                // �ؼ�������ͳһ�ı��淽�������������϶�����ȥ
                bool success = await _studentService.SaveChangesAsync(newStudents, modifiedStudents);

                if (success)
                {
                    MessageBox.Show("���ݱ���ɹ���", "�ɹ�", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // �����ɹ�������������ټ���
                    newStudents.Clear();
                    modifiedStudents.Clear();
                    await LoadStudentsAsync();
                    // ����ѡ���Ƽ������¼������ݣ��Ի�ȡ�²����¼������ID
                    // await LoadStudentsAsync(); 
                }
                else
                {
                    MessageBox.Show("����ʧ�ܣ�û�����ܵ�Ӱ�����δ֪����", "ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������ʱ��������: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StudentAddBtn_Click(object sender, EventArgs e)
        {

            using (AddStudentWindow addStudentWindow = new AddStudentWindow(_studentService))
            {
                // ��ʾ���ѧ����Ϣ�Ĵ���
                DialogResult result = addStudentWindow.ShowDialog();
            }

        }
        #endregion

        #region Course�ؼ����¼�����
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
                // 2. ���÷�����ͳһ��ѯ����
                var cos = (await _courseService.SearchCoursesAsync(criteria)).ToList();
                courses = new BindingList<CourseTeacherView>(cos); // �� IEnumerable ת��Ϊ IList
                courses.ListChanged += Courses_ListChanged; // ���� ListChanged �¼�
                // 3. �󶨽����DataGridView
                CoursedataGridView.DataSource = courses;
                // �����Ҫ����������DataGridView���б��������
                CoursedataGridView.Columns["Cid"].HeaderText = "�γ�ID";
                CoursedataGridView.Columns["Cname"].HeaderText = "�γ���";
                CoursedataGridView.Columns["Tid"].HeaderText = "��ʦID";
                CoursedataGridView.Columns["Tname"].HeaderText = "��ʦ��";

                // (��ѡ) ��ʾ��ѯ�������
                MessageBox.Show($"��ѯ�� {courses.Count()} ����¼��", "��ѯ���");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ѯʱ��������: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        addCourseWindow.TeachercomboBox.DataSource = res.ToList(); // ʹ��ʵ������
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"��ȡ��ʦ�б�ʧ��: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                // ��ʾ��ӿγ���Ϣ�Ĵ���
                DialogResult result = addCourseWindow.ShowDialog();
            }
        }
        private async void CourseUpdateBtn_Click(object sender, EventArgs e)
        {
            CoursedataGridView.EndEdit();
            this.Validate();

            // ����Ƿ����κ���Ҫ����ĸ��ģ����������������޸ģ�
            if (!newCourses.Any() && !modifiedCourses.Any())
            {
                MessageBox.Show("û�м�⵽�κ����ݸ��ġ�");
                return;
            }

            try
            {
                // �ؼ�������ͳһ�ı��淽�������������϶�����ȥ
                bool success = await _courseService.UpdateCourseAsync(newCourses, modifiedCourses);

                if (success)
                {
                    MessageBox.Show("�γ����ݱ���ɹ���", "�ɹ�", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // �����ɹ�������������ټ���
                    newCourses.Clear();
                    modifiedCourses.Clear();

                    // ����ѡ���Ƽ������¼������ݣ��Ի�ȡ�²����¼������ID
                    // await LoadCoursesAsync();
                }
                else
                {
                    MessageBox.Show("����ʧ�ܣ�û�����ܵ�Ӱ�����δ֪����", "ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"����γ�����ʱ��������: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ����DataGridView�ĸ�ѡ����
        private void Students_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                // ��һ������������Ա��޸�ʱ����
                case ListChangedType.ItemChanged:
                    {
                        Student changedStudent = students[e.NewIndex];
                        // ֻ�е����ѧ������һ������ӵ�ѧ��ʱ���Ž�����Ϊ�����޸ġ�
                        // ����Է�ֹһ�������ڱ��༭ʱͬʱ������ newStudents �� modifiedStudents ��
                        if (!newStudents.Contains(changedStudent))
                        {
                            modifiedStudents.Add(changedStudent);
                        }
                        break;
                    }

                // ��һ�������ӵ��б�ʱ���� (���磬��DataGridView�����һ����������)
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
        #endregion



 




    }
}