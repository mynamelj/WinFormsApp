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
using WinFormsApp.Views; // �������������ռ�

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // ����һ���������������

        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        [Required]
        private BindingList<Student> students;
        private BindingList<CourseTeacherView> courses;
        // ���ڸ����޸ĵ�ѧ��
        private HashSet<Student> modifiedStudents = new HashSet<Student>();
        //����Ƿ�����ѧ�����
        private HashSet<Student> newStudents = new HashSet<Student>();
        private HashSet<CourseTeacherView> modifiedCourses = new HashSet<CourseTeacherView>();

        // �ù��캯������ IStudentService ����
        public Form1(IStudentService studentService, ICourseService courseService)
        {
            InitializeComponent();
            _studentService = studentService; // DI�������Զ��ṩʵ��
            _courseService = courseService;
        }

        private  async Task LoadStudentsAsync()
        {

            var allStudents = await _studentService.GetStudentAllAsync();
            students = new BindingList<Student>(allStudents.ToList());
            students.ListChanged += Students_ListChanged; // ���� ListChanged �¼�
            StudentDataGridView.DataSource = students;
            // �����Ҫ����������DataGridView���б��������
            StudentDataGridView.Columns["sid"].HeaderText = "ѧ��ID";
            StudentDataGridView.Columns["sname"].HeaderText = "ѧ������";
            StudentDataGridView.Columns["sage"].HeaderText = "��������";
            StudentDataGridView.Columns["ssex"].HeaderText = "�Ա�";

        }

        private async void StudentQueryBtn_Click(object sender, EventArgs e)
        {
            var criteria = new Student
            {
                sid = StudentIdtextBox.Text.Trim(),
                sname = StudentNametextBox.Text.Trim(),
                // �������ڿؼ�ΪDateTimePicker���Ա�ΪComboBox
                // ����ؼ�δ���û�δѡ��������Ϊnull
                sage = StudentBirthdaytextBox.Enabled && DateTime.TryParse(StudentBirthdaytextBox.Text.Trim(), out var date) ? date : (DateTime?)null,
                ssex = StudentGendertextBox.Text.Trim()
            };
            try
            {
                // 2. ���÷�����ͳһ��ѯ����
                var stus = (await _studentService.SearchStudentsAsync(criteria)).ToList();
                students = new BindingList<Student>(stus); // �� IEnumerable ת��Ϊ IList
                students.ListChanged += Students_ListChanged;
                // 3. �󶨽����DataGridView
                StudentDataGridView.DataSource = students;
                // �����Ҫ����������DataGridView���б��������
                StudentDataGridView.Columns["sid"].HeaderText = "ѧ��ID";
                StudentDataGridView.Columns["sname"].HeaderText = "ѧ������";
                StudentDataGridView.Columns["sage"].HeaderText = "��������";
                StudentDataGridView.Columns["ssex"].HeaderText = "�Ա�";

                // (��ѡ) ��ʾ��ѯ�������
                MessageBox.Show($"��ѯ�� {students.Count()} ����¼��", "��ѯ���");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ѯʱ��������: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // ���� ListChanged �¼��Ĵ������
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

        private void StudentDeleteBtn_Click(object sender, EventArgs e)
        {
            // �ֱ��ǣ���ʾ��Ϣ�����⡢Ĭ��ֵ
            string inputId = Interaction.InputBox("������Ҫɾ����ѧ��ID��", "ɾ��ȷ��", "");
            if (!string.IsNullOrEmpty(inputId))
            {
                // ������ִ������ɾ���߼�
                // Ϊ�˰�ȫ�������֤һ��������Ƿ�Ϊ��Ч����
                if (int.TryParse(inputId, out int idToDelete))
                {
                    MessageBox.Show($"׼��ɾ��IDΪ {idToDelete} �ļ�¼��");
                    // �ڴ˵�������ɾ������򷽷�
                    _studentService.DeleteStudentAsync(inputId);
                }
                else
                {
                    MessageBox.Show("��������Ч������ID��", "������Ч", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("������ȡ����");
            }
        }

        // ��ȷ�� Form1 ��ť�¼�����
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

        private async void StudentAddBtn_Click(object sender, EventArgs e)
        {

            using (AddStudentWindow addStudentWindow = new AddStudentWindow(_studentService))
            {
                // ��ʾ���ѧ����Ϣ�Ĵ���
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
            //    StduentBirthdaytextBox.Clear(); // ���ѡ��
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
                // 2. ���÷�����ͳһ��ѯ����
                 var cos = (await _courseService.SearchCoursesAsync(criteria)).ToList();
                courses = new BindingList<CourseTeacherView>(cos); // �� IEnumerable ת��Ϊ IList
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
    }
}