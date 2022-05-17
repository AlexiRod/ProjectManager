namespace WindowsFormsApplication
{
    partial class EditUsersForm
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
            this.panelUsers = new System.Windows.Forms.Panel();
            this.labelUsers = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // panelUsers
            // 
            this.panelUsers.AutoScroll = true;
            this.panelUsers.BackColor = System.Drawing.Color.White;
            this.panelUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUsers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelUsers.Location = new System.Drawing.Point(0, 138);
            this.panelUsers.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelUsers.Name = "panelUsers";
            this.panelUsers.Size = new System.Drawing.Size(775, 566);
            this.panelUsers.TabIndex = 5;
            // 
            // labelUsers
            // 
            this.labelUsers.Location = new System.Drawing.Point(0, 81);
            this.labelUsers.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelUsers.Name = "labelUsers";
            this.labelUsers.Size = new System.Drawing.Size(775, 42);
            this.labelUsers.TabIndex = 1;
            this.labelUsers.Text = "Список исполнителей";
            this.labelUsers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Sitka Small", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelHeader.Location = new System.Drawing.Point(74, 16);
            this.labelHeader.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(620, 42);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "Редактирование списка исполнителей";
            // 
            // buttonSave
            // 
            this.buttonSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Location = new System.Drawing.Point(713, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(50, 50);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Location = new System.Drawing.Point(12, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(50, 50);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // EditUsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 704);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.labelUsers);
            this.Controls.Add(this.panelUsers);
            this.Font = new System.Drawing.Font("Sitka Small", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "EditUsersForm";
            this.Text = "EditUsersForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditUsersForm_FormClosing);
            this.Load += new System.EventHandler(this.EditUsersForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelUsers;
        private System.Windows.Forms.Label labelUsers;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ToolTip toolTip;
    }
}