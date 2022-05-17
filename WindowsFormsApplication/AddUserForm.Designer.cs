namespace WindowsFormsApplication
{
    partial class AddUserForm
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
            this.components = new System.ComponentModel.Container();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtbName = new System.Windows.Forms.TextBox();
            this.lblTasks = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelTasks = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Sitka Small", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.Location = new System.Drawing.Point(139, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(427, 42);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Добавление пользователя";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(17, 81);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(218, 29);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Имя пользователя:";
            // 
            // txtbName
            // 
            this.txtbName.Location = new System.Drawing.Point(239, 82);
            this.txtbName.Name = "txtbName";
            this.txtbName.Size = new System.Drawing.Size(441, 32);
            this.txtbName.TabIndex = 2;
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Location = new System.Drawing.Point(126, 131);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(467, 29);
            this.lblTasks.TabIndex = 1;
            this.lblTasks.Text = "Список задач, в которых он задействован:";
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(647, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 50);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelTasks
            // 
            this.panelTasks.AutoScroll = true;
            this.panelTasks.BackColor = System.Drawing.Color.White;
            this.panelTasks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTasks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTasks.Location = new System.Drawing.Point(0, 182);
            this.panelTasks.Name = "panelTasks";
            this.panelTasks.Size = new System.Drawing.Size(709, 522);
            this.panelTasks.TabIndex = 5;
            // 
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(709, 704);
            this.Controls.Add(this.panelTasks);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblTasks);
            this.Controls.Add(this.txtbName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Sitka Small", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximumSize = new System.Drawing.Size(727, 751);
            this.Name = "AddUserForm";
            this.Text = "AddUserForm";
            this.Load += new System.EventHandler(this.AddUserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtbName;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panelTasks;
        private System.Windows.Forms.ToolTip toolTip;
    }
}