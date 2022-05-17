namespace WindowsFormsApplication
{
    partial class EditTaskForm
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
            this.lblTasks = new System.Windows.Forms.Label();
            this.panelTasks = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Sitka Small", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.Location = new System.Drawing.Point(108, 16);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(485, 42);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Редактирование списка задач";
            // 
            // lblTasks
            // 
            this.lblTasks.Location = new System.Drawing.Point(13, 67);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(690, 29);
            this.lblTasks.TabIndex = 1;
            this.lblTasks.Text = "Список задач пользователя";
            this.lblTasks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTasks
            // 
            this.panelTasks.AutoScroll = true;
            this.panelTasks.BackColor = System.Drawing.Color.White;
            this.panelTasks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTasks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTasks.Location = new System.Drawing.Point(0, 112);
            this.panelTasks.Name = "panelTasks";
            this.panelTasks.Size = new System.Drawing.Size(709, 592);
            this.panelTasks.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Location = new System.Drawing.Point(647, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(50, 50);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // EditTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 704);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.panelTasks);
            this.Controls.Add(this.lblTasks);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Sitka Small", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximumSize = new System.Drawing.Size(727, 751);
            this.Name = "EditTaskForm";
            this.Text = "EditTaskForm";
            this.Load += new System.EventHandler(this.EditTaskForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.Panel panelTasks;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonSave;
    }
}