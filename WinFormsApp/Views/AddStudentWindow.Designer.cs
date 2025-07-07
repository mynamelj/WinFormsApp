namespace WinFormsApp.Views
{
    partial class AddStudentWindow
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
            tableLayoutPanel2 = new TableLayoutPanel();
            StudentIdtextBox = new TextBox();
            StudentIdlable = new Label();
            StudentGendertextBox = new TextBox();
            StudentBirthdaytextBox = new TextBox();
            StudentNamelabel = new Label();
            StudentBirthdaylabel = new Label();
            StudentGenderlable = new Label();
            StudentNametextBox = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            Confirm = new Button();
            取消 = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel1.Location = new Point(1, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 73.3333359F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 26.666666F));
            tableLayoutPanel1.Size = new Size(363, 240);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayoutPanel2.Controls.Add(StudentIdtextBox, 1, 0);
            tableLayoutPanel2.Controls.Add(StudentIdlable, 0, 0);
            tableLayoutPanel2.Controls.Add(StudentGendertextBox, 1, 3);
            tableLayoutPanel2.Controls.Add(StudentBirthdaytextBox, 1, 2);
            tableLayoutPanel2.Controls.Add(StudentNamelabel, 0, 1);
            tableLayoutPanel2.Controls.Add(StudentBirthdaylabel, 0, 2);
            tableLayoutPanel2.Controls.Add(StudentGenderlable, 0, 3);
            tableLayoutPanel2.Controls.Add(StudentNametextBox, 1, 1);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(357, 170);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // StudentIdtextBox
            // 
            StudentIdtextBox.Location = new Point(92, 3);
            StudentIdtextBox.Name = "StudentIdtextBox";
            StudentIdtextBox.Size = new Size(121, 23);
            StudentIdtextBox.TabIndex = 7;
            // 
            // StudentIdlable
            // 
            StudentIdlable.AutoSize = true;
            StudentIdlable.BackColor = Color.Lavender;
            StudentIdlable.Location = new Point(3, 0);
            StudentIdlable.Name = "StudentIdlable";
            StudentIdlable.Size = new Size(21, 17);
            StudentIdlable.TabIndex = 6;
            StudentIdlable.Text = "ID";
            // 
            // StudentGendertextBox
            // 
            StudentGendertextBox.Location = new Point(92, 129);
            StudentGendertextBox.Name = "StudentGendertextBox";
            StudentGendertextBox.Size = new Size(121, 23);
            StudentGendertextBox.TabIndex = 5;
            // 
            // StudentBirthdaytextBox
            // 
            StudentBirthdaytextBox.Location = new Point(92, 87);
            StudentBirthdaytextBox.Name = "StudentBirthdaytextBox";
            StudentBirthdaytextBox.Size = new Size(121, 23);
            StudentBirthdaytextBox.TabIndex = 4;
            // 
            // StudentNamelabel
            // 
            StudentNamelabel.AutoSize = true;
            StudentNamelabel.Location = new Point(3, 42);
            StudentNamelabel.Name = "StudentNamelabel";
            StudentNamelabel.Size = new Size(32, 17);
            StudentNamelabel.TabIndex = 0;
            StudentNamelabel.Text = "姓名";
            // 
            // StudentBirthdaylabel
            // 
            StudentBirthdaylabel.AutoSize = true;
            StudentBirthdaylabel.Location = new Point(3, 84);
            StudentBirthdaylabel.Name = "StudentBirthdaylabel";
            StudentBirthdaylabel.Size = new Size(56, 17);
            StudentBirthdaylabel.TabIndex = 1;
            StudentBirthdaylabel.Text = "出生年月";
            // 
            // StudentGenderlable
            // 
            StudentGenderlable.AutoSize = true;
            StudentGenderlable.Location = new Point(3, 126);
            StudentGenderlable.Name = "StudentGenderlable";
            StudentGenderlable.Size = new Size(32, 17);
            StudentGenderlable.TabIndex = 2;
            StudentGenderlable.Text = "性别";
            // 
            // StudentNametextBox
            // 
            StudentNametextBox.Location = new Point(92, 45);
            StudentNametextBox.Name = "StudentNametextBox";
            StudentNametextBox.Size = new Size(121, 23);
            StudentNametextBox.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(Confirm);
            flowLayoutPanel1.Controls.Add(取消);
            flowLayoutPanel1.Location = new Point(3, 179);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(357, 58);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // Confirm
            // 
            Confirm.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Confirm.DialogResult = DialogResult.OK;
            Confirm.ImageAlign = ContentAlignment.TopCenter;
            Confirm.Location = new Point(50, 3);
            Confirm.Margin = new Padding(50, 3, 3, 3);
            Confirm.Name = "Confirm";
            Confirm.Size = new Size(75, 23);
            Confirm.TabIndex = 0;
            Confirm.Text = "确认";
            Confirm.UseVisualStyleBackColor = true;
            Confirm.Click += Confirm_Click;
            // 
            // 取消
            // 
            取消.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            取消.DialogResult = DialogResult.Cancel;
            取消.Location = new Point(228, 3);
            取消.Margin = new Padding(100, 3, 3, 3);
            取消.Name = "取消";
            取消.Size = new Size(75, 23);
            取消.TabIndex = 1;
            取消.Text = "取消";
            取消.UseVisualStyleBackColor = true;
            // 
            // AddStudentWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(365, 241);
            Controls.Add(tableLayoutPanel1);
            Name = "AddStudentWindow";
            Text = "AddStudentWindow";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button Confirm;
        private Button 取消;
        private Label StudentNamelabel;
        private Label StudentBirthdaylabel;
        private Label StudentGenderlable;
        private TextBox StudentGendertextBox;
        private TextBox StudentBirthdaytextBox;
        private TextBox StudentNametextBox;
        private TextBox StudentIdtextBox;
        private Label StudentIdlable;
    }
}