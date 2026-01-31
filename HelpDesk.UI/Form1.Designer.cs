namespace HelpDesk.UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            btnUpdateTicket = new Button();
            btnCreateTicket = new Button();
            txtResolution = new TextBox();
            label8 = new Label();
            label7 = new Label();
            dateTimePicker1 = new DateTimePicker();
            cmbStatus = new ComboBox();
            label6 = new Label();
            cmbAssignedTo = new ComboBox();
            label5 = new Label();
            cmbCategory = new ComboBox();
            label4 = new Label();
            txtDescription = new TextBox();
            label3 = new Label();
            txtIssueTitle = new TextBox();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            chkConfirmDelete = new CheckBox();
            btnClearAll = new Button();
            btnDeleleteTicket = new Button();
            groupBox3 = new GroupBox();
            btnResetFilter = new Button();
            btnApplyFilter = new Button();
            cmbFilterStatus = new ComboBox();
            label10 = new Label();
            cmbFilterCategory = new ComboBox();
            label9 = new Label();
            dgTickets = new DataGridView();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblCounts = new ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgTickets).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnUpdateTicket);
            groupBox1.Controls.Add(btnCreateTicket);
            groupBox1.Controls.Add(txtResolution);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(cmbStatus);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cmbAssignedTo);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cmbCategory);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtDescription);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtIssueTitle);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(9, 38);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(432, 374);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Details";
            // 
            // btnUpdateTicket
            // 
            btnUpdateTicket.Location = new Point(225, 332);
            btnUpdateTicket.Margin = new Padding(2);
            btnUpdateTicket.Name = "btnUpdateTicket";
            btnUpdateTicket.Size = new Size(158, 30);
            btnUpdateTicket.TabIndex = 15;
            btnUpdateTicket.Text = "Update Ticket";
            btnUpdateTicket.UseVisualStyleBackColor = true;
            btnUpdateTicket.Click += btnUpdateTicket_Click;
            // 
            // btnCreateTicket
            // 
            btnCreateTicket.Location = new Point(48, 332);
            btnCreateTicket.Margin = new Padding(2);
            btnCreateTicket.Name = "btnCreateTicket";
            btnCreateTicket.Size = new Size(162, 30);
            btnCreateTicket.TabIndex = 14;
            btnCreateTicket.Text = "Create Ticket";
            btnCreateTicket.UseVisualStyleBackColor = true;
            btnCreateTicket.Click += btnCreateTicket_Click;
            // 
            // txtResolution
            // 
            txtResolution.Location = new Point(94, 214);
            txtResolution.Margin = new Padding(2);
            txtResolution.Multiline = true;
            txtResolution.Name = "txtResolution";
            txtResolution.Size = new Size(325, 84);
            txtResolution.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(11, 214);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(66, 15);
            label8.TabIndex = 12;
            label8.Text = "Resolution:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 305);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(78, 15);
            label7.TabIndex = 11;
            label7.Text = "Date Created:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Location = new Point(94, 302);
            dateTimePicker1.Margin = new Padding(2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(325, 23);
            dateTimePicker1.TabIndex = 10;
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(94, 187);
            cmbStatus.Margin = new Padding(2);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(325, 23);
            cmbStatus.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(11, 187);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(42, 15);
            label6.TabIndex = 8;
            label6.Text = "Status:";
            // 
            // cmbAssignedTo
            // 
            cmbAssignedTo.FormattingEnabled = true;
            cmbAssignedTo.Location = new Point(94, 158);
            cmbAssignedTo.Margin = new Padding(2);
            cmbAssignedTo.Name = "cmbAssignedTo";
            cmbAssignedTo.Size = new Size(325, 23);
            cmbAssignedTo.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 160);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(61, 15);
            label5.TabIndex = 6;
            label5.Text = "Assign To:";
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(94, 132);
            cmbCategory.Margin = new Padding(2);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(325, 23);
            cmbCategory.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 134);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 4;
            label4.Text = "Category:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(94, 46);
            txtDescription.Margin = new Padding(2);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(325, 84);
            txtDescription.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 43);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 2;
            label3.Text = "Description:";
            // 
            // txtIssueTitle
            // 
            txtIssueTitle.Location = new Point(94, 22);
            txtIssueTitle.Margin = new Padding(2);
            txtIssueTitle.Name = "txtIssueTitle";
            txtIssueTitle.Size = new Size(325, 23);
            txtIssueTitle.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 24);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 0;
            label2.Text = "Title:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(8, 5);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(204, 25);
            label1.TabIndex = 1;
            label1.Text = "HelpDesk Application";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(chkConfirmDelete);
            groupBox2.Controls.Add(btnClearAll);
            groupBox2.Controls.Add(btnDeleleteTicket);
            groupBox2.Location = new Point(9, 416);
            groupBox2.Margin = new Padding(2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2);
            groupBox2.Size = new Size(432, 85);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Actions Management";
            // 
            // chkConfirmDelete
            // 
            chkConfirmDelete.AutoSize = true;
            chkConfirmDelete.Location = new Point(48, 58);
            chkConfirmDelete.Margin = new Padding(2);
            chkConfirmDelete.Name = "chkConfirmDelete";
            chkConfirmDelete.Size = new Size(111, 19);
            chkConfirmDelete.TabIndex = 17;
            chkConfirmDelete.Text = "Confirm Delete?";
            chkConfirmDelete.UseVisualStyleBackColor = true;
            chkConfirmDelete.CheckedChanged += chkConfirmDelete_CheckedChanged;
            // 
            // btnClearAll
            // 
            btnClearAll.Location = new Point(225, 24);
            btnClearAll.Margin = new Padding(2);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(162, 30);
            btnClearAll.TabIndex = 16;
            btnClearAll.Text = "Clear All";
            btnClearAll.UseVisualStyleBackColor = true;
            // 
            // btnDeleleteTicket
            // 
            btnDeleleteTicket.Location = new Point(48, 24);
            btnDeleleteTicket.Margin = new Padding(2);
            btnDeleleteTicket.Name = "btnDeleleteTicket";
            btnDeleleteTicket.Size = new Size(162, 30);
            btnDeleleteTicket.TabIndex = 15;
            btnDeleleteTicket.Text = "Delete Ticket";
            btnDeleleteTicket.UseVisualStyleBackColor = true;
            btnDeleleteTicket.Click += btnDeleleteTicket_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnResetFilter);
            groupBox3.Controls.Add(btnApplyFilter);
            groupBox3.Controls.Add(cmbFilterStatus);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(cmbFilterCategory);
            groupBox3.Controls.Add(label9);
            groupBox3.Location = new Point(445, 38);
            groupBox3.Margin = new Padding(2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2);
            groupBox3.Size = new Size(470, 68);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Searching and Filtering";
            // 
            // btnResetFilter
            // 
            btnResetFilter.Location = new Point(364, 37);
            btnResetFilter.Margin = new Padding(2);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new Size(99, 24);
            btnResetFilter.TabIndex = 5;
            btnResetFilter.Text = "Reset Filter";
            btnResetFilter.UseVisualStyleBackColor = true;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.Location = new Point(273, 37);
            btnApplyFilter.Margin = new Padding(2);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(87, 24);
            btnApplyFilter.TabIndex = 4;
            btnApplyFilter.Text = "Apply Filter";
            btnApplyFilter.UseVisualStyleBackColor = true;
            // 
            // cmbFilterStatus
            // 
            cmbFilterStatus.FormattingEnabled = true;
            cmbFilterStatus.Location = new Point(141, 38);
            cmbFilterStatus.Margin = new Padding(2);
            cmbFilterStatus.Name = "cmbFilterStatus";
            cmbFilterStatus.Size = new Size(129, 23);
            cmbFilterStatus.TabIndex = 3;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(141, 21);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(68, 15);
            label10.TabIndex = 2;
            label10.Text = "Filter Status";
            // 
            // cmbFilterCategory
            // 
            cmbFilterCategory.FormattingEnabled = true;
            cmbFilterCategory.Location = new Point(10, 38);
            cmbFilterCategory.Margin = new Padding(2);
            cmbFilterCategory.Name = "cmbFilterCategory";
            cmbFilterCategory.Size = new Size(129, 23);
            cmbFilterCategory.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(10, 21);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(84, 15);
            label9.TabIndex = 0;
            label9.Text = "Filter Category";
            // 
            // dgTickets
            // 
            dgTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgTickets.Location = new Point(445, 110);
            dgTickets.Margin = new Padding(2);
            dgTickets.Name = "dgTickets";
            dgTickets.RowHeadersWidth = 62;
            dgTickets.Size = new Size(470, 391);
            dgTickets.TabIndex = 4;
            dgTickets.CellClick += dgTickets_CellClick;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, lblCounts });
            statusStrip1.Location = new Point(0, 497);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 10, 0);
            statusStrip1.Size = new Size(923, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 17);
            // 
            // lblCounts
            // 
            lblCounts.Name = "lblCounts";
            lblCounts.Size = new Size(0, 17);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(923, 519);
            Controls.Add(statusStrip1);
            Controls.Add(dgTickets);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Margin = new Padding(2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HelpDesk Application";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgTickets).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtDescription;
        private Label label3;
        private TextBox txtIssueTitle;
        private Label label2;
        private Label label1;
        private ComboBox cmbCategory;
        private Label label4;
        private Label label6;
        private ComboBox cmbAssignedTo;
        private Label label5;
        private Label label7;
        private DateTimePicker dateTimePicker1;
        private ComboBox cmbStatus;
        private TextBox txtResolution;
        private Label label8;
        private Button btnUpdateTicket;
        private Button btnCreateTicket;
        private GroupBox groupBox2;
        private CheckBox chkConfirmDelete;
        private Button btnClearAll;
        private Button btnDeleleteTicket;
        private GroupBox groupBox3;
        private Button btnResetFilter;
        private Button btnApplyFilter;
        private ComboBox cmbFilterStatus;
        private Label label10;
        private ComboBox cmbFilterCategory;
        private Label label9;
        private DataGridView dgTickets;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblCounts;
    }
}
