namespace WindowsFormsApplication
{
    partial class AddTaskForm
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
            this.labelHeader = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.txtbName = new System.Windows.Forms.TextBox();
            this.cBoxType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.panelBoth = new System.Windows.Forms.Panel();
            this.panelProjects = new System.Windows.Forms.Panel();
            this.panelItems = new System.Windows.Forms.Panel();
            this.labelItems = new System.Windows.Forms.Label();
            this.labelProjects = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panelBoth.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Sitka Small", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelHeader.Location = new System.Drawing.Point(206, 15);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(322, 42);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "Добавление задачи";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(8, 98);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(204, 29);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Название задачи:";
            // 
            // txtbName
            // 
            this.txtbName.Location = new System.Drawing.Point(214, 99);
            this.txtbName.Name = "txtbName";
            this.txtbName.Size = new System.Drawing.Size(510, 32);
            this.txtbName.TabIndex = 2;
            // 
            // cBoxType
            // 
            this.cBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxType.FormattingEnabled = true;
            this.cBoxType.Items.AddRange(new object[] {
            "Тема (Epic)",
            "История (Story)",
            "Задание (Task)",
            "Ошибка (Bug)"});
            this.cBoxType.Location = new System.Drawing.Point(159, 161);
            this.cBoxType.Name = "cBoxType";
            this.cBoxType.Size = new System.Drawing.Size(565, 37);
            this.cBoxType.TabIndex = 3;
            this.cBoxType.SelectedIndexChanged += new System.EventHandler(this.cBoxType_SelectedIndexChanged);
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(8, 164);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(143, 29);
            this.labelType.TabIndex = 1;
            this.labelType.Text = "Тип задачи:";
            // 
            // panelBoth
            // 
            this.panelBoth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBoth.Controls.Add(this.panelProjects);
            this.panelBoth.Controls.Add(this.panelItems);
            this.panelBoth.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBoth.Location = new System.Drawing.Point(0, 283);
            this.panelBoth.Name = "panelBoth";
            this.panelBoth.Size = new System.Drawing.Size(736, 404);
            this.panelBoth.TabIndex = 4;
            // 
            // panelProjects
            // 
            this.panelProjects.AutoScroll = true;
            this.panelProjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProjects.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelProjects.Location = new System.Drawing.Point(363, 0);
            this.panelProjects.Name = "panelProjects";
            this.panelProjects.Size = new System.Drawing.Size(371, 402);
            this.panelProjects.TabIndex = 4;
            // 
            // panelItems
            // 
            this.panelItems.AutoScroll = true;
            this.panelItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelItems.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelItems.Location = new System.Drawing.Point(0, 0);
            this.panelItems.Name = "panelItems";
            this.panelItems.Size = new System.Drawing.Size(364, 402);
            this.panelItems.TabIndex = 4;
            // 
            // labelItems
            // 
            this.labelItems.Location = new System.Drawing.Point(12, 229);
            this.labelItems.Name = "labelItems";
            this.labelItems.Size = new System.Drawing.Size(333, 38);
            this.labelItems.TabIndex = 1;
            this.labelItems.Text = "Подзадачи:";
            this.labelItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelProjects
            // 
            this.labelProjects.Location = new System.Drawing.Point(388, 229);
            this.labelProjects.Name = "labelProjects";
            this.labelProjects.Size = new System.Drawing.Size(336, 38);
            this.labelProjects.TabIndex = 1;
            this.labelProjects.Text = "Проекты:";
            this.labelProjects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Location = new System.Drawing.Point(674, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(50, 50);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // AddTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 687);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelProjects);
            this.Controls.Add(this.labelItems);
            this.Controls.Add(this.panelBoth);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.cBoxType);
            this.Controls.Add(this.txtbName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelHeader);
            this.Font = new System.Drawing.Font("Sitka Small", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "AddTaskForm";
            this.Text = "AddTaskForm";
            this.Load += new System.EventHandler(this.AddTaskForm_Load);
            this.panelBoth.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox txtbName;
        private System.Windows.Forms.ComboBox cBoxType;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Panel panelBoth;
        private System.Windows.Forms.Panel panelProjects;
        private System.Windows.Forms.Panel panelItems;
        private System.Windows.Forms.Label labelItems;
        private System.Windows.Forms.Label labelProjects;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonAdd;
    }
}