namespace WinFormsApp
{
    // partial 关键字表示这个类还有一部分在其他文件中定义（即 Form1.cs）
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            MainLayoutPanel = new TableLayoutPanel();
            StudentflowLayoutPanel = new FlowLayoutPanel();
            Tablelabel = new Label();
            Infobox = new TableLayoutPanel();
            Idlabel = new Label();
            Namelabel = new Label();
            Datelable = new Label();
            Genderlabel = new Label();
            StudentIdtextBox = new TextBox();
            StudentNametextBox = new TextBox();
            StduentBirthdaytextBox = new TextBox();
            StudnetGendertextBox = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            StudentAddBtn = new Button();
            UpdateStudentBtn = new Button();
            StudentDeleteBtn = new Button();
            StudentQueryBtn = new Button();
            StudentDataGridView = new DataGridView();
            MainLayoutPanel.SuspendLayout();
            StudentflowLayoutPanel.SuspendLayout();
            Infobox.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)StudentDataGridView).BeginInit();
            SuspendLayout();
            // 
            // MainLayoutPanel
            // 
            MainLayoutPanel.ColumnCount = 3;
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 496F));
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MainLayoutPanel.Controls.Add(StudentflowLayoutPanel, 0, 0);
            MainLayoutPanel.Location = new Point(-1, 1);
            MainLayoutPanel.Name = "MainLayoutPanel";
            MainLayoutPanel.RowCount = 1;
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            MainLayoutPanel.Size = new Size(1419, 467);
            MainLayoutPanel.TabIndex = 0;
            // 
            // StudentflowLayoutPanel
            // 
            StudentflowLayoutPanel.Controls.Add(Tablelabel);
            StudentflowLayoutPanel.Controls.Add(Infobox);
            StudentflowLayoutPanel.Controls.Add(flowLayoutPanel1);
            StudentflowLayoutPanel.Controls.Add(StudentDataGridView);
            StudentflowLayoutPanel.Location = new Point(3, 3);
            StudentflowLayoutPanel.Name = "StudentflowLayoutPanel";
            StudentflowLayoutPanel.Size = new Size(455, 461);
            StudentflowLayoutPanel.TabIndex = 0;
            // 
            // Tablelabel
            // 
            Tablelabel.AutoSize = true;
            Tablelabel.Location = new Point(20, 20);
            Tablelabel.Margin = new Padding(20, 20, 0, 0);
            Tablelabel.Name = "Tablelabel";
            Tablelabel.Size = new Size(44, 17);
            Tablelabel.TabIndex = 0;
            Tablelabel.Text = "学生表";
            // 
            // Infobox
            // 
            Infobox.ColumnCount = 4;
            Infobox.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            Infobox.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            Infobox.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            Infobox.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            Infobox.Controls.Add(Idlabel, 0, 0);
            Infobox.Controls.Add(Namelabel, 0, 1);
            Infobox.Controls.Add(Datelable, 0, 2);
            Infobox.Controls.Add(Genderlabel, 0, 3);
            Infobox.Controls.Add(StudentIdtextBox, 1, 0);
            Infobox.Controls.Add(StudentNametextBox, 1, 1);
            Infobox.Controls.Add(StduentBirthdaytextBox, 1, 2);
            Infobox.Controls.Add(StudnetGendertextBox, 1, 3);
            Infobox.Location = new Point(3, 40);
            Infobox.Name = "Infobox";
            Infobox.RowCount = 4;
            Infobox.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            Infobox.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            Infobox.RowStyles.Add(new RowStyle(SizeType.Percent, 22F));
            Infobox.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            Infobox.Size = new Size(452, 112);
            Infobox.TabIndex = 1;
            // 
            // Idlabel
            // 
            Idlabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Idlabel.AutoSize = true;
            Idlabel.Location = new Point(3, 0);
            Idlabel.Name = "Idlabel";
            Idlabel.Size = new Size(107, 28);
            Idlabel.TabIndex = 0;
            Idlabel.Text = "ID";
            // 
            // Namelabel
            // 
            Namelabel.AutoSize = true;
            Namelabel.Location = new Point(3, 28);
            Namelabel.Name = "Namelabel";
            Namelabel.Size = new Size(32, 17);
            Namelabel.TabIndex = 1;
            Namelabel.Text = "姓名";
            // 
            // Datelable
            // 
            Datelable.AutoSize = true;
            Datelable.Location = new Point(3, 56);
            Datelable.Name = "Datelable";
            Datelable.Size = new Size(56, 17);
            Datelable.TabIndex = 2;
            Datelable.Text = "出生年月";
            // 
            // Genderlabel
            // 
            Genderlabel.AutoSize = true;
            Genderlabel.Location = new Point(3, 81);
            Genderlabel.Name = "Genderlabel";
            Genderlabel.Size = new Size(32, 17);
            Genderlabel.TabIndex = 3;
            Genderlabel.Text = "性别";
            // 
            // StudentIdtextBox
            // 
            StudentIdtextBox.Location = new Point(116, 3);
            StudentIdtextBox.Name = "StudentIdtextBox";
            StudentIdtextBox.Size = new Size(100, 23);
            StudentIdtextBox.TabIndex = 4;
            StudentIdtextBox.TextChanged += StudentIdTextChanged;
            // 
            // StudentNametextBox
            // 
            StudentNametextBox.Location = new Point(116, 31);
            StudentNametextBox.Name = "StudentNametextBox";
            StudentNametextBox.Size = new Size(100, 23);
            StudentNametextBox.TabIndex = 5;
            // 
            // StduentBirthdaytextBox
            // 
            StduentBirthdaytextBox.Location = new Point(116, 59);
            StduentBirthdaytextBox.Name = "StduentBirthdaytextBox";
            StduentBirthdaytextBox.Size = new Size(100, 23);
            StduentBirthdaytextBox.TabIndex = 6;
            // 
            // StudnetGendertextBox
            // 
            StudnetGendertextBox.Location = new Point(116, 84);
            StudnetGendertextBox.Name = "StudnetGendertextBox";
            StudnetGendertextBox.Size = new Size(100, 23);
            StudnetGendertextBox.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(StudentAddBtn);
            flowLayoutPanel1.Controls.Add(UpdateStudentBtn);
            flowLayoutPanel1.Controls.Add(StudentDeleteBtn);
            flowLayoutPanel1.Controls.Add(StudentQueryBtn);
            flowLayoutPanel1.Location = new Point(3, 158);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(452, 37);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // StudentAddBtn
            // 
            StudentAddBtn.Location = new Point(3, 3);
            StudentAddBtn.Name = "StudentAddBtn";
            StudentAddBtn.Size = new Size(75, 30);
            StudentAddBtn.TabIndex = 0;
            StudentAddBtn.Text = "新增";
            StudentAddBtn.UseVisualStyleBackColor = true;
            StudentAddBtn.Click += StudentAddBtn_Click;
            // 
            // UpdateStudentBtn
            // 
            UpdateStudentBtn.Location = new Point(84, 3);
            UpdateStudentBtn.Name = "UpdateStudentBtn";
            UpdateStudentBtn.Size = new Size(75, 30);
            UpdateStudentBtn.TabIndex = 1;
            UpdateStudentBtn.Text = "修改";
            UpdateStudentBtn.UseVisualStyleBackColor = true;
            UpdateStudentBtn.Click += UpdateStudentBtn_Click;
            // 
            // StudentDeleteBtn
            // 
            StudentDeleteBtn.Location = new Point(165, 3);
            StudentDeleteBtn.Name = "StudentDeleteBtn";
            StudentDeleteBtn.Size = new Size(75, 30);
            StudentDeleteBtn.TabIndex = 2;
            StudentDeleteBtn.Text = "删除";
            StudentDeleteBtn.UseVisualStyleBackColor = true;
            StudentDeleteBtn.Click += StudentDeleteBtn_Click;
            // 
            // StudentQueryBtn
            // 
            StudentQueryBtn.Location = new Point(343, 3);
            StudentQueryBtn.Margin = new Padding(100, 3, 3, 3);
            StudentQueryBtn.Name = "StudentQueryBtn";
            StudentQueryBtn.Size = new Size(75, 30);
            StudentQueryBtn.TabIndex = 3;
            StudentQueryBtn.Text = "查询";
            StudentQueryBtn.UseVisualStyleBackColor = true;
            StudentQueryBtn.Click += StudentQueryBtn_Click;
            // 
            // StudentDataGridView
            // 
            StudentDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            StudentDataGridView.Location = new Point(3, 201);
            StudentDataGridView.Name = "StudentDataGridView";
            StudentDataGridView.Size = new Size(452, 260);
            StudentDataGridView.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1420, 674);
            Controls.Add(MainLayoutPanel);
            Name = "Form1";
            Text = "学生信息查询";
            MainLayoutPanel.ResumeLayout(false);
            StudentflowLayoutPanel.ResumeLayout(false);
            StudentflowLayoutPanel.PerformLayout();
            Infobox.ResumeLayout(false);
            Infobox.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)StudentDataGridView).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel MainLayoutPanel;
        private FlowLayoutPanel StudentflowLayoutPanel;
        private Label Tablelabel;
        private TableLayoutPanel Infobox;
        private Label Idlabel;
        private Label Namelabel;
        private Label Datelable;
        private Label Genderlabel;
        private TextBox StudentIdtextBox;
        private TextBox StudentNametextBox;
        private TextBox StduentBirthdaytextBox;
        private TextBox StudnetGendertextBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button StudentAddBtn;
        private Button UpdateStudentBtn;
        private Button StudentDeleteBtn;
        private Button StudentQueryBtn;
        private DataGridView StudentDataGridView;
    }
}
