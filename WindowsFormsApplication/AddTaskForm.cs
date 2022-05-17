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
    public partial class AddTaskForm : Form
    {
        public Goal newGoal = new Goal("", DateTime.Now);
        public static List<User> users = new List<User>();
        public static List<Project> projects = new List<Project>();
        public static List<Goal> goals = new List<Goal>();
        public AddTaskForm()
        {
            InitializeComponent();
            users = MainForm.users;
            projects = MainForm.projects;
            goals = MainForm.goals;
        }
        List<CheckBox> cbTasks = new List<CheckBox>();
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<Button> buttons = new List<Button>();

        List<CheckBox> cbUsers = new List<CheckBox>();
        List<Button> buttonUsers = new List<Button>();

        List<CheckBox> cbProjects = new List<CheckBox>();
        List<Button> buttonProjects = new List<Button>();

        private void AddTaskForm_Load(object sender, EventArgs e)
        {
            buttonAdd.BackgroundImage = Resources.Closed;
            cBoxType.SelectedIndex = 0; 
            DisplayProjects();
        }

        private void DisplayTasks()
        {
            if (cbTasks.Count != 0)
            {
                foreach (CheckBox checkBox in cbTasks)
                    panelItems.Controls.Add(checkBox);
                foreach (PictureBox pictureBox in pictureBoxes)
                    panelItems.Controls.Add(pictureBox);
                foreach (Button button in buttons)
                    panelItems.Controls.Add(button);
                return;
            }


            Point prevGoal = new Point(4, 10);
            int maxLeft = 0;
            labelItems.Text = "Подзадачи:";

            foreach (var goal in goals)
            {
                if (goal is Epic || goal is Bug)
                    continue;
                CheckBox checkBox = new CheckBox()
                {
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Font = new Font(Font.FontFamily, Font.Size, FontStyle.Italic),
                    Location = prevGoal,
                    TextAlign = ContentAlignment.TopLeft,
                    AutoSize = true,
                    BackColor = Color.White
                };

                Bitmap bmp = new Bitmap(1, 1);

                if (goal is Story)
                {
                    bmp = Resources.Story;
                    checkBox.ForeColor = Color.Yellow;
                }
                if (goal is Task)
                {
                    bmp = Resources.Task;
                    checkBox.ForeColor = Color.Blue;
                }

                checkBox.Text = goal.Name;
                checkBox.Image = new Bitmap(bmp, new Size(checkBox.Height, checkBox.Height));
                toolTip.SetToolTip(checkBox, goal.ToString());
                cbTasks.Add(checkBox);
                panelItems.Controls.Add(checkBox);


                PictureBox pbStatus = new PictureBox()
                {
                    Location = new Point(checkBox.Left + checkBox.Width + 5, checkBox.Top + 11),
                    Size = checkBox.Image.Size,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    BorderStyle = BorderStyle.FixedSingle
                };
                if (pbStatus.Left > maxLeft)
                    maxLeft = pbStatus.Left;
                pictureBoxes.Add(pbStatus);

                if (goal.Status == "Открытая")
                    pbStatus.BackgroundImage = Resources.Open;
                if (goal.Status == "В работе")
                    pbStatus.BackgroundImage = Resources.Working;
                if (goal.Status == "Завершенная")
                    pbStatus.BackgroundImage = Resources.Closed;

                panelItems.Controls.Add(pbStatus);
                prevGoal.Y += checkBox.Height + 2;

                Button btnBack = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(pbStatus.Width + 5, checkBox.Height + 3),
                    Location = new Point(checkBox.Left - 2, checkBox.Top - 2),
                    BackColor = Color.White,
                    Enabled = false,
                };
                pbStatus.Top = btnBack.Top + btnBack.Height / 2 - pbStatus.Height / 2;
                buttons.Add(btnBack);

                panelItems.Controls.Add(btnBack);
                btnBack.SendToBack();
            }


            foreach (PictureBox pictureBox in pictureBoxes)
                pictureBox.Left = maxLeft;
            foreach (Button btn in buttons)
                btn.Width += maxLeft - prevGoal.X;
        }

        private void DisplayUsers()
        {
            if (cbUsers.Count != 0)
            {
                foreach (CheckBox checkBox in cbUsers)
                    panelItems.Controls.Add(checkBox);
                foreach (Button button in buttonUsers)
                    panelItems.Controls.Add(button);
                return;
            }

            Point prevGoal = new Point(4, 10);
            labelItems.Text = "Исполнители:";

            foreach (var user in users)
            {
                CheckBox checkBox = new CheckBox()
                {
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold),
                    Location = prevGoal,
                    TextAlign = ContentAlignment.TopLeft,
                    AutoSize = true,
                    BackColor = Color.White
                };
                checkBox.Text = user.Name;
                toolTip.SetToolTip(checkBox, user.ToString());

                //checkBox.CheckedChanged += ChangeUser;
               
                cbUsers.Add(checkBox);
                panelItems.Controls.Add(checkBox);
                prevGoal.Y += checkBox.Height + 5;

                Button btnBack = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(panelItems.Width - 8, checkBox.Height + 3),
                    Location = new Point(checkBox.Left - 2, checkBox.Top - 2),
                    BackColor = Color.White,
                    Enabled = false,
                };

                buttonUsers.Add(btnBack);
                panelItems.Controls.Add(btnBack);
                btnBack.SendToBack();
            }

        }

        //private void ChangeUser(object sender, EventArgs e)
        //{
        //    if (cBoxType.SelectedIndex == 2 || cBoxType.SelectedIndex == 3)
        //    {
        //        CheckBox lastCb = new CheckBox();
        //        foreach (CheckBox item in cbUsers)
        //        {
        //            if (item.Checked)
        //                lastCb = item;
        //            item.Checked = false;
        //        }
        //        lastCb.Checked = true;
                    
        //        CheckBox cb = sender as CheckBox;
        //        if (cb.CheckState == CheckState.Checked)
        //            foreach (CheckBox item in cbUsers)
        //                if (item.Text != cb.Text)
        //                    item.Checked = false;
        //    }
        //}

        private void DisplayProjects()
        {
            Point prevGoal = new Point(4, 10);

            foreach (Project project in projects)
            {
                CheckBox checkBox = new CheckBox()
                {
                    Font = new Font(Font.FontFamily, Font.Size + 1, FontStyle.Bold),
                    Location = prevGoal,
                    TextAlign = ContentAlignment.TopLeft,
                    AutoSize = true,
                    BackColor = Color.White
                };
                checkBox.Text = project.Name;
                toolTip.SetToolTip(checkBox, project.ToString());

                checkBox.CheckedChanged += (s, e) =>
                {
                    CheckBox cb = s as CheckBox;
                    if (cb.Checked)
                        foreach (CheckBox item in cbProjects)
                            if(item.Text != cb.Text)
                                item.Checked = false;
                };

                cbProjects.Add(checkBox);
                panelProjects.Controls.Add(checkBox);
                prevGoal.Y += checkBox.Height + 5;

                Button btnBack = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(panelProjects.Width - 8, checkBox.Height + 3),
                    Location = new Point(checkBox.Left - 2, checkBox.Top - 2),
                    BackColor = Color.White,
                    Enabled = false,
                };

                buttonUsers.Add(btnBack);
                panelProjects.Controls.Add(btnBack);
                btnBack.SendToBack();
            }
        }

        private void cBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in cbTasks)
                panelItems.Controls.Remove(checkBox);
            foreach (PictureBox pictureBox in pictureBoxes)
                panelItems.Controls.Remove(pictureBox);
            foreach (Button button in buttons)
                panelItems.Controls.Remove(button);
            foreach (Button button in buttonUsers)
                panelItems.Controls.Remove(button);
            foreach (CheckBox checkBox in cbUsers)
                panelItems.Controls.Remove(checkBox);
            panelBoth.Select();

            if (cBoxType.SelectedIndex == 0)
                DisplayTasks();
            else DisplayUsers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtbName.Text.Trim();
                if (name == string.Empty)
                {
                    MessageBox.Show("Пожалуйста, введите имя задачи", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                foreach (Goal g in goals)
                    if (g.Name == name)
                    {
                        MessageBox.Show("Задача с таким названием уже существует. Попробуйте другое", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                if (cBoxType.SelectedIndex == 0)
                {
                    Epic epic = new Epic(name, DateTime.Now);
                    foreach (CheckBox checkBox in cbTasks)
                    {
                        Goal goal = FindGoal(checkBox.Text);
                        if (checkBox.Checked && goal != null)
                            epic.Tasks.Add(goal);
                    }
                    goals.Add(epic);
                    newGoal = epic;
                    foreach (CheckBox checkBox in cbProjects)
                    {
                        Project project = FindProject(checkBox.Text);
                        if (checkBox.Checked && project != null)
                            project.Goals.Add(epic);
                    }
                }
                if (cBoxType.SelectedIndex == 1)
                {
                    Story story = new Story(name, DateTime.Now);
                    foreach (CheckBox checkBox in cbUsers)
                    {
                        User user = FindUser(checkBox.Text);
                        if (checkBox.Checked && user != null)
                        {
                            if (story.MaxCount <= story.Users.Count)
                                MessageBox.Show($"Число исполнителей Истории {story.Name} достигло максимума в {story.MaxCount} человек. " +
                                $"Пользователь {name} не будет назначен исполнителем для нее.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            else
                            {
                                user.Tasks.Add(story);
                                user.Tasks.Sort();
                                story.Users.Add(user);
                            }
                        }
                    }
                    goals.Add(story);
                    newGoal = story;
                    foreach (CheckBox checkBox in cbProjects)
                    {
                        Project project = FindProject(checkBox.Text);
                        if (checkBox.Checked && project != null)
                            project.Goals.Add(story);
                    }
                }
                if (cBoxType.SelectedIndex == 2)
                {
                    Task task = new Task(name, DateTime.Now);
                    int count = 0;
                    foreach (CheckBox checkBox in cbUsers)
                        if (checkBox.Checked)
                            count++;
                    if (count > 1)
                    {
                        MessageBox.Show($"У Задачи {task.Name} должен быть только один исполнитель.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    foreach (CheckBox checkBox in cbUsers)
                    {
                        User user = FindUser(checkBox.Text);
                        if (checkBox.Checked && user != null)
                        {
                            task.User = user;
                            user.Tasks.Add(task);
                            user.Tasks.Sort();
                            break;
                        }
                    }
                    goals.Add(task);
                    newGoal = task;
                    foreach (CheckBox checkBox in cbProjects)
                    {
                        Project project = FindProject(checkBox.Text);
                        if (checkBox.Checked && project != null)
                        {
                            project.Goals.Add(task);
                            task.isInProject = true;
                        }
                    }
                }
                if (cBoxType.SelectedIndex == 3)
                {
                    Bug bug = new Bug(name, DateTime.Now);
                    int count = 0;
                    foreach (CheckBox checkBox in cbUsers)
                        if (checkBox.Checked)
                            count++;
                    if (count > 1)
                    {
                        MessageBox.Show($"У Ошибки {bug.Name} должен быть только один исполнитель.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    foreach (CheckBox checkBox in cbUsers)
                    {
                        User user = FindUser(checkBox.Text);
                        if (checkBox.Checked && user != null)
                        {
                            bug.User = user;
                            user.Tasks.Add(bug);
                            user.Tasks.Sort();
                            break;
                        }
                    }
                    goals.Add(bug);
                    newGoal = bug;
                    foreach (CheckBox checkBox in cbProjects)
                    {
                        Project project = FindProject(checkBox.Text);
                        if (checkBox.Checked && project != null)
                            project.Goals.Add(bug);
                    }

                }

                MessageBox.Show($"Задача {name} успешно добавлена к списку задач." +
                    $"{(cBoxType.SelectedIndex == 0 ? " Задача является Темой (Epic), поэтому для нее нельзя будет назначить исполнителей." : "")}" +
                    $"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainForm.SaveData();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("При добавлении задачи произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private User FindUser(string name)
        {
            foreach (var user in users)
                if (user.Name == name)
                    return user;
            return null;
        }
        private Goal FindGoal(string name)
        {
            foreach (var goal in goals)
                if (goal.Name == name)
                    return goal;
            return null;
        }
        private Project FindProject(string name)
        {
            foreach (var goal in projects)
                if (goal.Name == name)
                    return goal;
            return null;
        }


    }
}
