namespace WindowsFormsApplication
{
    partial class AddProjectForm
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.txtbName = new System.Windows.Forms.TextBox();
            this.btnGoals = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(15, 90);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(215, 29);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Название проекта:";
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Sitka Small", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelHeader.Location = new System.Drawing.Point(182, 18);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(338, 42);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "Добавление проекта";
            // 
            // txtbName
            // 
            this.txtbName.Location = new System.Drawing.Point(239, 90);
            this.txtbName.Name = "txtbName";
            this.txtbName.Size = new System.Drawing.Size(458, 32);
            this.txtbName.TabIndex = 2;
            // 
            // btnGoals
            // 
            this.btnGoals.Location = new System.Drawing.Point(24, 142);
            this.btnGoals.Name = "btnGoals";
            this.btnGoals.Size = new System.Drawing.Size(672, 55);
            this.btnGoals.TabIndex = 5;
            this.btnGoals.Text = "Изменить список заданий проекта";
            this.btnGoals.UseVisualStyleBackColor = true;
            this.btnGoals.Click += new System.EventHandler(this.btnGoals_Click);
            // 
            // AddProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 564);
            this.Controls.Add(this.btnGoals);
            this.Controls.Add(this.txtbName);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.btnAdd);
            this.Font = new System.Drawing.Font("Sitka Small", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "AddProjectForm";
            this.Text = "AddProjectForm";
            this.Load += new System.EventHandler(this.AddProjectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtbName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Button btnGoals;
    }
}