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
            StudentNamelabel = new Label();
            StudentNametextBox = new TextBox();
            StudentBirthdaylabel = new Label();
            StudentBirthdaytextBox = new TextBox();
            StudentGenderlabel = new Label();
            StudentGendertextBox = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            Confirm = new Button();
            Cancel = new Button();
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
            tableLayoutPanel2.Controls.Add(StudentNamelabel, 0, 0);
            tableLayoutPanel2.Controls.Add(StudentNametextBox, 1, 0);
            tableLayoutPanel2.Controls.Add(StudentBirthdaylabel, 0, 1);
            tableLayoutPanel2.Controls.Add(StudentBirthdaytextBox, 1, 1);
            tableLayoutPanel2.Controls.Add(StudentGenderlabel, 0, 2);
            tableLayoutPanel2.Controls.Add(StudentGendertextBox, 1, 2);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(357, 170);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // StudentNamelabel
            // 
            StudentNamelabel.AutoSize = true;
            StudentNamelabel.Location = new Point(3, 0);
            StudentNamelabel.Name = "StudentNamelabel";
            StudentNamelabel.Size = new Size(56, 17);
            StudentNamelabel.TabIndex = 0;
            StudentNamelabel.Text = "学生姓名";
            // 
            // StudentNametextBox
            // 
            StudentNametextBox.Location = new Point(92, 3);
            StudentNametextBox.Name = "StudentNametextBox";
            StudentNametextBox.Size = new Size(121, 23);
            StudentNametextBox.TabIndex = 3;
            // 
            // StudentBirthdaylabel
            // 
            StudentBirthdaylabel.AutoSize = true;
            StudentBirthdaylabel.Location = new Point(3, 56);
            StudentBirthdaylabel.Name = "StudentBirthdaylabel";
            StudentBirthdaylabel.Size = new Size(56, 17);
            StudentBirthdaylabel.TabIndex = 1;
            StudentBirthdaylabel.Text = "学生生日";
            // 
            // StudentBirthdaytextBox
            // 
            StudentBirthdaytextBox.Location = new Point(92, 59);
            StudentBirthdaytextBox.Name = "StudentBirthdaytextBox";
            StudentBirthdaytextBox.Size = new Size(121, 23);
            StudentBirthdaytextBox.TabIndex = 4;
            // 
            // StudentGenderlabel
            // 
            StudentGenderlabel.AutoSize = true;
            StudentGenderlabel.Location = new Point(3, 112);
            StudentGenderlabel.Name = "StudentGenderlabel";
            StudentGenderlabel.Size = new Size(56, 17);
            StudentGenderlabel.TabIndex = 2;
            StudentGenderlabel.Text = "学生性别";
            // 
            // StudentGendertextBox
            // 
            StudentGendertextBox.Location = new Point(92, 115);
            StudentGendertextBox.Name = "StudentGendertextBox";
            StudentGendertextBox.Size = new Size(121, 23);
            StudentGendertextBox.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(Confirm);
            flowLayoutPanel1.Controls.Add(Cancel);
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
            // Cancel
            // 
            Cancel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Cancel.DialogResult = DialogResult.Cancel;
            Cancel.Location = new Point(228, 3);
            Cancel.Margin = new Padding(100, 3, 3, 3);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(75, 23);
            Cancel.TabIndex = 1;
            Cancel.Text = "取消";
            Cancel.UseVisualStyleBackColor = true;
            // 
            // AddStudentWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(365, 240);
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
        private Button Cancel;
        private Label StudentNamelabel;
        private Label StudentBirthdaylabel;
        private TextBox StudentBirthdaytextBox;
        private TextBox StudentNametextBox;
        private Label StudentGenderlabel;
        private TextBox StudentGendertextBox;
    }
}