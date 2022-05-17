namespace WindowsFormsApplication
{
    partial class MainForm
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
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnProjects = new System.Windows.Forms.Button();
            this.btnGoals = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUsers
            // 
            this.btnUsers.Location = new System.Drawing.Point(228, 144);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(406, 92);
            this.btnUsers.TabIndex = 0;
            this.btnUsers.Text = "Работа с пользователями";
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnProjects
            // 
            this.btnProjects.Location = new System.Drawing.Point(228, 259);
            this.btnProjects.Name = "btnProjects";
            this.btnProjects.Size = new System.Drawing.Size(406, 92);
            this.btnProjects.TabIndex = 0;
            this.btnProjects.Text = "Работа с проектами";
            this.btnProjects.UseVisualStyleBackColor = true;
            this.btnProjects.Click += new System.EventHandler(this.btnProjects_Click);
            // 
            // btnGoals
            // 
            this.btnGoals.Location = new System.Drawing.Point(228, 374);
            this.btnGoals.Name = "btnGoals";
            this.btnGoals.Size = new System.Drawing.Size(406, 92);
            this.btnGoals.TabIndex = 0;
            this.btnGoals.Text = "Работа с задачами";
            this.btnGoals.UseVisualStyleBackColor = true;
            this.btnGoals.Click += new System.EventHandler(this.btnGoals_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(877, 610);
            this.Controls.Add(this.btnGoals);
            this.Controls.Add(this.btnProjects);
            this.Controls.Add(this.btnUsers);
            this.Font = new System.Drawing.Font("Sitka Small", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MainForm";
            this.Text = "Менеджер задач";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnProjects;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGoals;
    }
}

