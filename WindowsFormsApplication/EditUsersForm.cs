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
    public partial class EditUsersForm : Form
    {

        public static List<User> users = new List<User>();
        public static List<Project> projects = new List<Project>();
        public static List<Goal> goals = new List<Goal>();

        public List<string> selectedUsers = new List<string>();
        public Goal selectedGoal = new Goal("", DateTime.Now);

        List<CheckBox> checkBoxes = new List<CheckBox>();

        public EditUsersForm()
        {
            InitializeComponent();
            users = MainForm.users;
            projects = MainForm.projects;
            goals = MainForm.goals;
        }

        Point prevGoal = new Point(4, 10);


        /// <summary>
        /// Начальная отрисовка.
        /// </summary>
        private void EditUsersForm_Load(object sender, EventArgs e)
        {
            buttonSave.BackgroundImage = Resources.Closed;
            buttonAdd.BackgroundImage = Resources.Add;
            labelUsers.Text += " задачи " + selectedGoal.Name + ":";

            foreach (var user in users)
            {
                CheckBox checkBox = new CheckBox()
                {
                    Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold),
                    Location = prevGoal,
                    TextAlign = ContentAlignment.TopLeft,
                    AutoSize = true,
                    BackColor = Color.White
                };
                checkBox.Text = user.Name;
                toolTip.SetToolTip(checkBox, user.ToString());

                if (selectedUsers.Contains(user.Name))
                    checkBox.Checked = true;
                panelUsers.Controls.Add(checkBox);
                prevGoal.Y += checkBox.Height + 5;
                checkBoxes.Add(checkBox);

                Button btnBack = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(panelUsers.Width - 8, checkBox.Height + 3),
                    Location = new Point(checkBox.Left - 2, checkBox.Top - 2),
                    BackColor = Color.White,
                    Enabled = false,
                };

                panelUsers.Controls.Add(btnBack);
                btnBack.SendToBack();
            }
        }

        /// <summary>
        /// Добавление элемента.
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddUserForm auf = new AddUserForm();
            auf.ShowDialog();
            MainForm.SaveData();


            CheckBox checkBox = new CheckBox()
            {
                Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold),
                Location = prevGoal,
                TextAlign = ContentAlignment.TopLeft,
                AutoSize = true,
                BackColor = Color.White
            };

            checkBoxes.Add(checkBox);
            checkBox.Text = auf.newUser.Name;
            toolTip.SetToolTip(checkBox, auf.newUser.ToString());

            panelUsers.Controls.Add(checkBox);
            prevGoal.Y += checkBox.Height + 5;

            Button btnBack = new Button()
            {
                FlatStyle = FlatStyle.Flat,
                Size = new Size(panelUsers.Width - 8, checkBox.Height + 3),
                Location = new Point(checkBox.Left - 2, checkBox.Top - 2),
                BackColor = Color.White,
                Enabled = false,
            };

            panelUsers.Controls.Add(btnBack);
            btnBack.SendToBack();

            foreach (var task in auf.newUser.Tasks)
                if (task.Name == selectedGoal.Name)
                {
                    checkBox.Checked = true;
                    break;
                }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (CloseForm(true))
                this.Close();
        }
        private User FindUser(string name)
        {
            foreach (var user in users)
                if (user.Name == name)
                    return user;
            return null;
        }


        /// <summary>
        /// Логика закрытия.
        /// </summary>
        private bool CloseForm(bool giveMessage)
        {
            try
            {
                if (selectedGoal is Story)
                {
                    Story story = selectedGoal as Story;

                    foreach (var user in users)
                        if (user.Tasks.Contains(story))
                            user.Tasks.Remove(story);


                    story.Users.Clear();
                    foreach (CheckBox checkBox in checkBoxes)
                    {
                        User user = FindUser(checkBox.Text);

                        if (checkBox.Checked && user != null)
                        {
                            if (story.MaxCount <= story.Users.Count)
                                MessageBox.Show($"Число исполнителей Истории {story.Name} достигло максимума в {story.MaxCount} человек. " +
                                $"Нельзя добавить столько исполнителей для нее.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            else
                            {
                                if (!user.Tasks.Contains(story))
                                    user.Tasks.Add(story);
                                user.Tasks.Sort();
                                story.Users.Add(user);
                            }
                        }
                    }
                }
                if (selectedGoal is Task)
                {
                    Task task = selectedGoal as Task;

                    int count = 0;
                    foreach (CheckBox checkBox in checkBoxes)
                        if (checkBox.Checked)
                            count++;
                    if (count > 1)
                    {
                        MessageBox.Show($"У Задачи {task.Name} должен быть только один исполнитель.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    foreach (CheckBox checkBox in checkBoxes)
                    {
                        User user = FindUser(checkBox.Text);
                        if (checkBox.Checked && user != null)
                        {
                            task.User.Tasks.Remove(task);
                            task.User = user;
                            user.Tasks.Add(task);
                            user.Tasks.Sort();
                            break;
                        }
                        else if (user != null && user.Name == checkBox.Text)
                        {
                            task.User.Tasks.Remove(task);
                            task.User = new User("Нет исполнителя");
                            user.Tasks.Remove(task);
                        }

                    }
                }
                if (selectedGoal is Bug)
                {
                    Bug bug = selectedGoal as Bug;
                    int count = 0;
                    foreach (CheckBox checkBox in checkBoxes)
                        if (checkBox.Checked)
                            count++;
                    if (count > 1)
                    {
                        MessageBox.Show($"У Ошибки {bug.Name} должен быть только один исполнитель.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    foreach (CheckBox checkBox in checkBoxes)
                    {
                        User user = FindUser(checkBox.Text);
                        if (checkBox.Checked && user != null)
                        {
                            bug.User.Tasks.Remove(bug);
                            bug.User = user;
                            user.Tasks.Add(bug);
                            user.Tasks.Sort();
                            break;
                        }
                        else if (user != null && user.Name == checkBox.Text)
                        {
                            bug.User.Tasks.Remove(bug);
                            bug.User = new User("Нет исполнителя");
                            user.Tasks.Remove(bug);
                        }
                    }
                }

                if (giveMessage)
                    MessageBox.Show($"Список пользователей задачи { selectedGoal.Name} успешно изменен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainForm.SaveData();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("При изменении списка задач произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }


        private void EditUsersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseForm(false);
        }
    }
}
