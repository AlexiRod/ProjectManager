using ProjectLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication.Properties;

namespace WindowsFormsApplication
{
    public partial class AddProjectForm : Form
    {
        public static List<User> users = new List<User>();
        public static List<Project> projects = new List<Project>();
        public static List<Goal> goals = new List<Goal>();
         Project curProject = new Project("");

        public AddProjectForm()
        {
            InitializeComponent(); 
            users = MainForm.users;
            projects = MainForm.projects;
            goals = MainForm.goals;
        }

        private void AddProjectForm_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = Resources.Add;
        }

        private void btnGoals_Click(object sender, EventArgs e)
        {

            try
            {
                string name = txtbName.Text.Trim();
                if (name == string.Empty)
                {
                    MessageBox.Show("Пожалуйста, введите название проекта.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                foreach (Project p in projects)
                    if (p.Name == name)
                    {
                        MessageBox.Show("Проект с таким именем уже существует. Попробуйте другое", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                curProject.Name = name;
                EditTaskForm etf = new EditTaskForm() { selectedUser = new User("ProjectEditing"), selectedGoals = new List<string>() };
                foreach (var goal in curProject.Goals)
                    etf.selectedGoals.Add(goal.Name);
                etf.selectedUser.Tasks.Add(new Goal(curProject.Name, DateTime.Now));
                etf.ShowDialog();

                curProject.Goals.Clear();
                foreach (var item in etf.selectedUser.Tasks)
                    //if(!project.Goals.Contains(item))
                    curProject.Goals.Add(item);
                curProject.Goals.Sort();
                
                MessageBox.Show($"Список заданий проекта {name} успешно изменен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("При добавлении пользователя произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtbName.Text.Trim();
                if (name == string.Empty)
                {
                    MessageBox.Show("Пожалуйста, введите название проекта.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                foreach (Project p in projects)
                    if (p.Name == name)
                    {
                        MessageBox.Show("Проект с таким именем уже существует. Попробуйте другое", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                curProject.Name = name;
                if (curProject.Goals.Count == 0)
                {
                    DialogResult res = MessageBox.Show($"Список заданий проекта {name} пуст. Все равно добавить данный проект?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No)
                        return;
                }

                projects.Add(curProject);
                MainForm.SaveData();
                
                MessageBox.Show($"Проект {name} успешно добавлен к списку проектов.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("При добавлении пользователя произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
