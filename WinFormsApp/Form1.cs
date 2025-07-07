using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WinFormsApp.Models;
using WinFormsApp.Services;
using WinFormsApp.Services.Interface;
using WinFormsApp.Views; // �������������ռ�

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // ����һ���������������

        private readonly IStudentService _studentService;
        private BindingList<Student> students;
        private HashSet<Student> modifiedStudents = new HashSet<Student>();

        // �ù��캯������ IStudentService ����
        public Form1(IStudentService studentService)
        {
            InitializeComponent();
            _studentService = studentService; // DI�������Զ��ṩʵ��
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
            // ����ֻ�����������Ա��
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                // ��ȡ���޸ĵ� Product ���󲢽�����ӵ� HashSet ��
                Student changedProduct = students[e.NewIndex];
                modifiedStudents.Add(changedProduct);
            }
        }

        private void StudentDeleteBtn_Click(object sender, EventArgs e)
        {

        }

        // ��ȷ�� Form1 ��ť�¼�����
        private async void UpdateStudentBtn_Click(object sender, EventArgs e)
        {
            this.StudentDataGridView.EndEdit();
            this.Validate();

            if (!modifiedStudents.Any())
            {
                MessageBox.Show("û�м�⵽�κ����ݸ��ġ�");
                return;
            }

            try
            {
                // �ؼ���ʹ�� await �ȴ��첽������ɣ�����ȡ���ؽ��
                bool success = await _studentService.UpdateStudentAsync(modifiedStudents);

                if (success)
                {
                    MessageBox.Show("ѧ����Ϣ���³ɹ���", "�ɹ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    modifiedStudents.Clear(); // ֻ���������ɹ������ռ���
                }
                else
                {
                    MessageBox.Show("����ʧ�ܣ�û�����ܵ�Ӱ�����δ֪����", "����ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"����ѧ����Ϣʱ��������: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void flowLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}