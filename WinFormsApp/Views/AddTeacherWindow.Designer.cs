﻿namespace WinFormsApp.Views
{
    partial class AddTeacherWindow
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
            TeacherIdlabel = new Label();
            TeacherNamelabel = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            Confirm = new Button();
            Cancel = new Button();
            TeacherNametextBox = new TextBox();
            TeacherIdtextBox = new TextBox();
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
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 73.3333359F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 26.666666F));
            tableLayoutPanel1.Size = new Size(348, 204);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayoutPanel2.Controls.Add(TeacherIdlabel, 0, 0);
            tableLayoutPanel2.Controls.Add(TeacherIdtextBox, 1, 0);
            tableLayoutPanel2.Controls.Add(TeacherNamelabel, 0, 1);
            tableLayoutPanel2.Controls.Add(TeacherNametextBox, 1, 1);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(342, 143);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // TeacherIdlabel
            // 
            TeacherIdlabel.AutoSize = true;
            TeacherIdlabel.Location = new Point(3, 0);
            TeacherIdlabel.Name = "TeacherIdlabel";
            TeacherIdlabel.Size = new Size(20, 17);
            TeacherIdlabel.TabIndex = 0;
            TeacherIdlabel.Text = "Id";
            // 
            // TeacherNamelabel
            // 
            TeacherNamelabel.AutoSize = true;
            TeacherNamelabel.Location = new Point(3, 47);
            TeacherNamelabel.Name = "TeacherNamelabel";
            TeacherNamelabel.Size = new Size(56, 17);
            TeacherNamelabel.TabIndex = 1;
            TeacherNamelabel.Text = "教师姓名";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(Confirm);
            flowLayoutPanel1.Controls.Add(Cancel);
            flowLayoutPanel1.Location = new Point(3, 152);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(342, 49);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // Confirm
            // 
            Confirm.Anchor = AnchorStyles.Left | AnchorStyles.Right;
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
            // TeacherNametextBox
            // 
            TeacherNametextBox.Location = new Point(88, 50);
            TeacherNametextBox.Name = "TeacherNametextBox";
            TeacherNametextBox.Size = new Size(121, 23);
            TeacherNametextBox.TabIndex = 4;
            // 
            // TeacherIdtextBox
            // 
            TeacherIdtextBox.Location = new Point(88, 3);
            TeacherIdtextBox.Name = "TeacherIdtextBox";
            TeacherIdtextBox.Size = new Size(121, 23);
            TeacherIdtextBox.TabIndex = 3;
            // 
            // AddTeacherWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(349, 201);
            Controls.Add(tableLayoutPanel1);
            Name = "AddTeacherWindow";
            Text = "AddTeacherWindow";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label TeacherIdlabel;
        private TextBox TeacherIdtextBox;
        private Label TeacherNamelabel;
        private TextBox TeacherNametextBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button Confirm;
        private Button Cancel;
    }
}