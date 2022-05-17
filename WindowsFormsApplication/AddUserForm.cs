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
    public partial class AddUserForm : Form
    {
        public static List<User> users = new List<User>();
        public static List<Project> projects = new List<Project>();
        public static List<Goal> goals = new List<Goal>();
        public List<string> containedGoals = new List<string>();
        public User newUser = new User("");

        public AddUserForm()
        {
            InitializeComponent();
            users = MainForm.users;
            projects = MainForm.projects;
            goals = MainForm.goals;
            btnAdd.BackgroundImage = Resources.Add;
        }

        List<CheckBox> checkBoxes = new List<CheckBox>();
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<Button> buttons = new List<Button>();
        private void AddUserForm_Load(object sender, EventArgs e)
        {
            Point prevGoal = new Point(4, 10);
            int maxLeft = 0;

            foreach (var goal in goals)
            {
                CheckBox checkBox = new CheckBox()
                {
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Font = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Italic),
                    Location = prevGoal,
                    TextAlign = ContentAlignment.MiddleLeft,
                    AutoSize = true,

                };

                Bitmap bmp = new Bitmap(1, 1);
                if (goal is Epic)
                {
                    continue;
                    //bmp = Resources.Epic;
                    //checkBox.ForeColor = Color.Lime;
                }
                if (goal is Story)
                {
                    bmp = Resources.Story;
                    checkBox.ForeColor = Color.Yellow;
                }
                if (goal is Task)
                {
                    bmp = Resources.Task;
                    checkBox.ForeColor = Color.Blue;
                    Task task = goal as Task;
                    if (task.User.Name != "Нет исполнителя")
                    {
                        checkBox.Font = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Underline);
                        checkBox.MouseEnter += (s, e) => { checkBox.BackColor = Color.DarkOrange; };
                        checkBox.MouseLeave += (s, e) => { checkBox.BackColor = Color.White; };
                    }
                }
                if (goal is Bug)
                {
                    bmp = Resources.Bug;
                    checkBox.ForeColor = Color.Red;
                    Bug bug = goal as Bug;
                    if (bug.User.Name != "Нет исполнителя")
                    {
                        checkBox.Font = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Underline);
                        checkBox.MouseEnter += (s, e) => { checkBox.BackColor = Color.DarkOrange; };
                        checkBox.MouseLeave += (s, e) => { checkBox.BackColor = Color.White; };
                    }
                }
                checkBox.Text = goal.Name;
                checkBox.Image = new Bitmap(bmp, new Size(checkBox.Height, checkBox.Height));
                toolTip.SetToolTip(checkBox, goal.ToString());
                checkBoxes.Add(checkBox);
                panelTasks.Controls.Add(checkBox);

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

                panelTasks.Controls.Add(pbStatus);
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

                panelTasks.Controls.Add(btnBack);
                btnBack.SendToBack();
            }


            foreach (PictureBox pictureBox in pictureBoxes)
                pictureBox.Left = maxLeft;
            foreach (Button btn in buttons)
                btn.Width += maxLeft - prevGoal.X;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtbName.Text.Trim();
                if (name == string.Empty)
                {
                    MessageBox.Show("Пожалуйста, введите имя пользователя", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                foreach (User u in users)
                    if (u.Name == name)
                    {
                        MessageBox.Show("Пользователь с таким именем уже существует. Попробуйте другое", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                User user = new User(name);
                foreach (CheckBox checkBox in checkBoxes)
                {
                    Goal goal = FindGoal(checkBox.Text);
                    if (checkBox.Checked && goal != null)
                    {
                        if (goal is Story)
                        {
                            Story story = goal as Story;
                            if (story.MaxCount <= story.Users.Count)
                                MessageBox.Show($"Число исполнителей Истории {story.Name} достигло максимума в {story.MaxCount} человек. " +
                                $"Пользователь {name} не будет назначен исполнителем для нее.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            else
                            {
                                if (!user.Tasks.Contains(goal))
                                    user.Tasks.Add(goal);
                                user.Tasks.Sort();
                                story.Users.Add(user);
                                containedGoals.Add(story.Name);
                            }
                        }
                        if (goal is Task)
                        {
                            Task task = goal as Task;
                            task.User.Tasks.Remove(task);
                            task.User = user;
                            user.Tasks.Add(goal);
                            user.Tasks.Sort();
                            containedGoals.Add(task.Name);
                        }
                        if (goal is Bug)
                        {
                            Bug bug = goal as Bug;
                            bug.User.Tasks.Remove(bug);
                            bug.User = user;
                            user.Tasks.Add(goal);
                            user.Tasks.Sort();
                            containedGoals.Add(bug.Name);
                        }
                    }
                }

                users.Add(user);
                newUser = user;
                MessageBox.Show($"Пользователь {name} успешно добавлен к списку исполнителей.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("При добавлении пользователя произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private Goal FindGoal(string name)
        {
            foreach (var goal in goals)
                if (goal.Name == name)
                    return goal;
            return null;
        }
    }
}
