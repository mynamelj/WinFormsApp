using System.Windows.Forms;

namespace WinFormsApp.Views
{
    partial class AddCourseWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            CourseTeacherBox = new TextBox();
            CourseIdBox = new TextBox();
            CourseNamelabel = new Label();
            CourseIdlabel = new Label();
            CourseTeacherlabel = new Label();
            CourseNameBox = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            Confirm = new Button();
            Cancel = new Button();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayoutPanel1.Controls.Add(CourseTeacherBox, 1, 2);
            tableLayoutPanel1.Controls.Add(CourseIdBox, 1, 1);
            tableLayoutPanel1.Controls.Add(CourseNamelabel, 0, 0);
            tableLayoutPanel1.Controls.Add(CourseIdlabel, 0, 1);
            tableLayoutPanel1.Controls.Add(CourseTeacherlabel, 0, 2);
            tableLayoutPanel1.Controls.Add(CourseNameBox, 1, 0);
            tableLayoutPanel1.Location = new Point(2, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(361, 170);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // CourseTeacherBox
            // 
            CourseTeacherBox.Location = new Point(93, 115);
            CourseTeacherBox.Name = "CourseTeacherBox";
            CourseTeacherBox.Size = new Size(130, 23);
            CourseTeacherBox.TabIndex = 5;
            // 
            // CourseIdBox
            // 
            CourseIdBox.Location = new Point(93, 59);
            CourseIdBox.Name = "CourseIdBox";
            CourseIdBox.Size = new Size(130, 23);
            CourseIdBox.TabIndex = 4;
            // 
            // CourseNamelabel
            // 
            CourseNamelabel.AutoSize = true;
            CourseNamelabel.Location = new Point(3, 0);
            CourseNamelabel.Name = "CourseNamelabel";
            CourseNamelabel.Size = new Size(44, 17);
            CourseNamelabel.TabIndex = 0;
            CourseNamelabel.Text = "课程名";
            // 
            // CourseIdlabel
            // 
            CourseIdlabel.AutoSize = true;
            CourseIdlabel.Location = new Point(3, 56);
            CourseIdlabel.Name = "CourseIdlabel";
            CourseIdlabel.Size = new Size(45, 17);
            CourseIdlabel.TabIndex = 1;
            CourseIdlabel.Text = "课程ID";
            // 
            // CourseTeacherlabel
            // 
            CourseTeacherlabel.AutoSize = true;
            CourseTeacherlabel.Location = new Point(3, 112);
            CourseTeacherlabel.Name = "CourseTeacherlabel";
            CourseTeacherlabel.Size = new Size(69, 17);
            CourseTeacherlabel.TabIndex = 2;
            CourseTeacherlabel.Text = "任课老师ID";
            // 
            // CourseNameBox
            // 
            CourseNameBox.Location = new Point(93, 3);
            CourseNameBox.Name = "CourseNameBox";
            CourseNameBox.Size = new Size(130, 23);
            CourseNameBox.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(Confirm);
            flowLayoutPanel1.Controls.Add(Cancel);
            flowLayoutPanel1.Location = new Point(2, 171);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(361, 68);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // Confirm
            // 
            Confirm.Location = new Point(50, 3);
            Confirm.Margin = new Padding(50, 3, 3, 3);
            Confirm.Name = "Confirm";
            Confirm.Size = new Size(75, 23);
            Confirm.TabIndex = 0;
            Confirm.Text = "确认";
            Confirm.UseVisualStyleBackColor = true;
            Confirm.Click += Confirm_Click;
            // 
            // Cancel
            // 
            Cancel.DialogResult = DialogResult.Cancel;
            Cancel.Location = new Point(228, 3);
            Cancel.Margin = new Padding(100, 3, 3, 3);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(75, 23);
            Cancel.TabIndex = 1;
            Cancel.Text = "取消";
            Cancel.UseVisualStyleBackColor = true;
            // 
            // AddCourseWindow
            // 
            ClientSize = new Size(365, 240);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(tableLayoutPanel1);
            Name = "AddCourseWindow";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);


        }

        #endregion


        private TableLayoutPanel tableLayoutPanel1;
        private Label CourseNamelabel;
        private Label CourseIdlabel;
        private Label CourseTeacherlabel;
        private TextBox CourseTeacherBox;
        private TextBox CourseIdBox;
        private TextBox CourseNameBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button Confirm;
        private Button Cancel;
    }
}