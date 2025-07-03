using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WinFormsApp.Models;
using WinFormsApp.Services;
using WinFormsApp.Services.Interface; // �������������ռ�

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // ����һ���������������

        private readonly IStudentService _studentService;

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
                sage = StduentBirthdaytextBox.Enabled && DateTime.TryParse(StduentBirthdaytextBox.Text, out var date) ? date : (DateTime?)null,
                ssex = StudnetGendertextBox.Text.Trim()
            };
            try
            {
                // 2. ���÷�����ͳһ��ѯ����
                var students = await _studentService.SearchStudentsAsync(criteria);

                // 3. �󶨽����DataGridView
                StudentDataGridView.DataSource = students.ToList();

                // (��ѡ) ��ʾ��ѯ�������
                MessageBox.Show($"��ѯ�� {students.Count()} ����¼��", "��ѯ���");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ѯʱ��������: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                StduentBirthdaytextBox .Clear(); // ���ѡ��
            }

        }
    }
}