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
    public partial class UsersForm : Form
    {
        public static List<User> users = new List<User>();
        public static List<Project> projects = new List<Project>();
        public static List<Goal> goals = new List<Goal>();
        int countOfUsersOnRow = 2;
        //ToolStripMenuItem selectedCount = new ToolStripMenuItem();

        public UsersForm()
        {
            InitializeComponent();
            users = MainForm.users;
            projects = MainForm.projects;
            goals = MainForm.goals;
        }

        /// <summary>
        /// Справка.
        /// </summary>
        private void ShowHelp(object sender, EventArgs e)
        {
            MessageBox.Show($"В данной форме можно управлять пользователями вашей программы.\n" +
                $"Для того, чтобы добавить пользователя, нажмите кнопку в контекстном меню и в открывшейся форме настройте данные нового " +
                $"пользователя. Учитывайте, что, исполнителй нельзя назначать на задачи типа Epic.\nДля безвозвратного удаления " +
                $"пользователя нажмите на синюю иконку слева от его имени. Все задания исполнителя будут отвязаны от него.\nДважды кликнув по имени" +
                $" пользователя, можно будет изменить его название. Изменять текст можно будет до тех пор, пока курсор находится на буквах. " +
                $"При его переносе пользователь сохранит введенное имя. Точно также можно менять названия задач пользователя.\nДля каждого " +
                $"задания отображаются  его тип, статус, а также проект, в ктором оно задействовано.\nДля добавления заданий в список " +
                $"пользователя или их изменения нужно нажать на кнопку плюса внизу. В открывающемся окне можно будет привязать задачи к " +
                $"пользователю, выбрав существующие или добавив новые задачи. Задачи, типа Task или Bug, уже имеющие исполнителя, подчеркиваются " +
                $"и выделяются красным при наведении. Это означает, что при их выборе, исполнитель этой задачи переназначится.\n" +
                $"Также можно создать новую задачу при нажатии плюса в самом низу списка существующих задач. В открывшейся форме нужно задать " +
                $"данные новой задачи, исходя из ее типа.\nДля удаления задачи у пользователя нажмите на красную иконку справа от ее названия." +
                $" В этом случае она будет отвязана от пользоватля, но не будет безвозвратно удалена. При наведении курсора на задачу," +
                $" проект или пользователя, на некоторое время будет показана информация о нем.", "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void UsersForm_Load(object sender, EventArgs e)
        {
            tsmiHelp.Click += ShowHelp;
            tsmiAddUser.Click += (s, e) =>
            {
                new AddUserForm().ShowDialog();
                MainForm.SaveData();
                this.Hide();
                new UsersForm().ShowDialog();
                this.Close();
            };
            Display();
        }

        /// <summary>
        /// Отображение всех данных на форме (выглядит страшно).
        /// </summary>
        private void Display()
        {
            foreach (Control item in Controls)
                if (item is Panel)
                {
                    this.Select();
                    Controls.Remove(item);
                    item.Dispose();
                }

            int maxHeight = 600;
            Point prevPoint = new Point(10, 30);
            foreach (User user in users)
            {
                Point lastTask = new Point(10, 30);
                Panel panel = new Panel()
                {
                    //AutoSize = true,
                    Width = 500,
                    Height = maxHeight,
                    AutoScroll = true,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = prevPoint,
                };


                TextBox txtbName = new TextBox()
                {
                    // AutoSize = false,
                    Location = new Point(3, 3),
                    Size = new Size(panel.Width - 8, 40),
                    Text = user.Name,
                    Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold),
                    TextAlign = HorizontalAlignment.Center,
                    BorderStyle = BorderStyle.FixedSingle,
                    ReadOnly = true
                };
                txtbName.MouseDoubleClick += (s, e) => { txtbName.ReadOnly = false; };
                txtbName.MouseLeave += (s, e) => { txtbName.ReadOnly = true; user.Name = txtbName.Text; };

                Button btnDeleteUser = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(txtbName.Height - 2, txtbName.Height - 2),
                    Location = new Point(txtbName.Left + 1, txtbName.Top + 1),
                    BackColor = Color.White,
                    BackgroundImage = Resources.Delete2,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    Name = user.Name + "~" + DateTime.Now,
                };
                btnDeleteUser.Click += DeleteUser;
                panel.Controls.Add(btnDeleteUser);
                btnDeleteUser.BringToFront();

                Button btnTasks = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(txtbName.Width, txtbName.Height + 8),
                    Location = new Point(txtbName.Left, txtbName.Top + txtbName.Height + 5),
                    Text = "Задания:",
                };
                lastTask = new Point(btnTasks.Left, btnTasks.Top + btnTasks.Height + 4);

                Point prevTask = new Point(btnTasks.Left, btnTasks.Top + btnTasks.Height);
                foreach (var goal in user.Tasks)
                {
                    Button btn = new Button()
                    {
                        FlatStyle = FlatStyle.Flat,
                        Size = new Size(txtbName.Width, txtbName.Height * 2 + 3),
                        Location = new Point(prevTask.X, prevTask.Y + 5),
                        BackColor = Color.White,

                    };
                    prevTask = new Point(btn.Left, btn.Top + btn.Height);
                    panel.Controls.Add(btn);
                    //btn.SendToBack();

                    PictureBox pbType = new PictureBox()
                    {
                        Location = new Point(btn.Left + 2, btn.Top + 1),
                        Size = new Size(btn.Height - 2, btn.Height - 3),
                        BackgroundImageLayout = ImageLayout.Zoom,
                        BorderStyle = BorderStyle.FixedSingle,
                    };
                    lastTask = pbType.Location;
                    lastTask.Y += pbType.Height + 4;

                    Label tbGoal = new Label()
                    {
                        AutoSize = false,
                        Location = new Point(pbType.Left + pbType.Width, btn.Top + 2),
                        Size = new Size(btn.Left + btn.Width - (pbType.Left + pbType.Width * 3 + 2) - 2, btn.Height / 2 - 2),
                        Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Italic),
                        TextAlign = ContentAlignment.BottomLeft,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                        Name = user.Name + "~" + DateTime.Now,
                    };
                    toolTip.SetToolTip(tbGoal, goal.ToString());

                    if (goal is Epic)
                    {
                        pbType.BackgroundImage = Resources.Epic;
                        tbGoal.ForeColor = Color.Lime;
                    }
                    if (goal is Story)
                    {
                        pbType.BackgroundImage = Resources.Story;
                        tbGoal.ForeColor = Color.Yellow;
                    }
                    if (goal is Task)
                    {
                        pbType.BackgroundImage = Resources.Task;
                        tbGoal.ForeColor = Color.Blue;
                    }
                    if (goal is Bug)
                    {
                        pbType.BackgroundImage = Resources.Bug;
                        tbGoal.ForeColor = Color.Red;
                    }
                    tbGoal.Text = goal.Name;

                    Label tbProject = new Label()
                    {
                        AutoSize = false,
                        Location = new Point(tbGoal.Left, tbGoal.Top + tbGoal.Height + 1),
                        Size = new Size(btn.Left + btn.Width - (pbType.Left + pbType.Width * 3 + 2) - 2, btn.Height / 2 - 2),
                        Font = new Font(Font.FontFamily, Font.Size - 2, FontStyle.Regular),
                        TextAlign = ContentAlignment.TopLeft,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                        Name = user.Name + "~" + DateTime.Now,
                    };

                    foreach (Project project in projects)
                    {
                        foreach (var g in project.Goals)
                        {
                            if (g.Name == goal.Name)
                            {
                                if (tbProject.Text != string.Empty)
                                    //if (project.Name.Trim() != string.Empty)
                                    tbProject.Text += "; " + project.Name;
                                else
                                    tbProject.Text = project.Name;
                                toolTip.SetToolTip(tbProject, project.ToString());
                                break;
                            }
                        }
                    }
                    if (tbProject.Text == string.Empty)
                        tbProject.Text = "Не задействована в проектах";


                    PictureBox pbStatus = new PictureBox()
                    {
                        Location = new Point(tbGoal.Left + tbGoal.Width + 1, btn.Top + 1),
                        Size = new Size(btn.Height - 2, btn.Height - 3),
                        BackgroundImageLayout = ImageLayout.Zoom,
                        BorderStyle = BorderStyle.FixedSingle,
                    };

                    if (goal.Status == "Открытая")
                        pbStatus.BackgroundImage = Resources.Open;
                    if (goal.Status == "В работе")
                        pbStatus.BackgroundImage = Resources.Working;
                    if (goal.Status == "Завершенная")
                        pbStatus.BackgroundImage = Resources.Closed;


                    Button btnDelete = new Button()
                    {
                        FlatStyle = FlatStyle.Flat,
                        Size = new Size(pbType.Width - 1, pbType.Height - 1),
                        Location = new Point(pbStatus.Left + pbStatus.Width + 2, pbStatus.Top + 1),
                        BackColor = Color.White,
                        BackgroundImage = Resources.Delete,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        Name = user.Name + "~" + goal.Name + "~" + DateTime.Now,
                    };
                    btnDelete.Click += DeleteGoal;

                    panel.Controls.Add(pbType);
                    pbType.BringToFront();
                    panel.Controls.Add(pbStatus);
                    pbStatus.BringToFront();
                    panel.Controls.Add(tbGoal);
                    tbGoal.BringToFront();
                    panel.Controls.Add(tbProject);
                    tbProject.BringToFront();
                    panel.Controls.Add(btnDelete);
                    btnDelete.BringToFront();
                }

                panel.Controls.Add(txtbName);
                panel.Controls.Add(btnTasks);


                Button btnAdd = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(txtbName.Height * 2, txtbName.Height * 2),
                    Location = lastTask,
                    BackColor = Color.White,
                    BackgroundImage = Resources.Add,
                    BackgroundImageLayout = ImageLayout.Zoom,
                };
                panel.Controls.Add(btnAdd);
                btnAdd.Click += (s, e) =>
                {
                    EditTaskForm etf = new EditTaskForm() { selectedUser = user, selectedGoals = new List<string>() };
                    foreach (var goal in user.Tasks)
                        etf.selectedGoals.Add(goal.Name);

                    etf.ShowDialog();
                    MainForm.SaveData();
                    this.Hide();
                    new UsersForm().ShowDialog();
                    this.Close();
                };





                this.Select();
                Controls.Add(panel);
                maxHeight = Math.Max(maxHeight, panel.Height);
                foreach (var p in Controls)
                    if (p is Panel)
                        (p as Panel).Height = Math.Max(maxHeight, (p as Panel).Height);
                prevPoint.X += panel.Width + 10;

                if (prevPoint.X > countOfUsersOnRow * panel.Width)
                {
                    prevPoint.X = 10;
                    prevPoint.Y += maxHeight + 10;

                }

            }

        }

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUser(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;

            try
            {
                string[] parts = btn.Name.Split('~');
                string userName = parts[0];
                DialogResult res = MessageBox.Show($"Вы действительно хотите удалить пользователя {userName}?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    foreach (User user in users)
                        if (user.Name == userName)
                        {
                            for (int i = 0; i < user.Tasks.Count; i++)
                            {
                                Goal goal = user.Tasks[i];
                                if (goal is Story)
                                {
                                    Story assGoal = goal as Story;
                                    assGoal.Users.Remove(user);
                                    user.Tasks.Remove(goal);
                                    i--;
                                }
                                if (goal is Task)
                                {
                                    Task task = goal as Task;
                                    user.Tasks.Remove(goal);
                                    task.User = new User("Нет исполнителя");
                                    i--;
                                }
                                if (goal is Bug)
                                {
                                    Bug bug = goal as Bug;
                                    user.Tasks.Remove(goal);
                                    bug.User = new User("Нет исполнителя");
                                    i--;
                                }

                            }

                            users.Remove(user);
                            MainForm.SaveData();
                            this.Hide();
                            new UsersForm().ShowDialog();
                            this.Close();

                            return;
                        }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("При удалении пользователя произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаление задачи.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteGoal(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            try
            {
                string[] parts = btn.Name.Split('~');
                string userName = parts[0];
                string goalName = parts[1];
                DialogResult res = MessageBox.Show($"Вы действительно хотите удалить задачу {goalName} из списка задач исполнителя {userName}?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    foreach (User user in users)
                        if (user.Name == userName)
                            foreach (var goal in user.Tasks)
                                if (goal.Name == goalName)
                                {
                                    if (goal is Story)
                                    {
                                        Story assGoal = goal as Story;
                                        assGoal.Users.Remove(user);
                                        user.Tasks.Remove(goal);
                                    }
                                    if (goal is Task)
                                    {
                                        Task task = goal as Task;
                                        user.Tasks.Remove(goal);
                                        task.User = new User("Нет исполнителя");
                                    }
                                    if (goal is Bug)
                                    {
                                        Bug bug = goal as Bug;
                                        user.Tasks.Remove(goal);
                                        bug.User = new User("Нет исполнителя");
                                    }

                                    MainForm.SaveData();
                                    this.Hide();
                                    new UsersForm().ShowDialog();
                                    this.Close();

                                    return;
                                }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("При удалении задачи произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Логика закрытия.
        /// </summary>
        private void UsersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.SaveData();
        }

    }
}
