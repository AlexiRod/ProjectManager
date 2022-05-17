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
    public partial class ProjectsForm : Form
    {
        public static List<User> users = new List<User>();
        public static List<Project> projects = new List<Project>();
        public static List<Goal> goals = new List<Goal>();
        bool isRestarting = false;

        public ProjectsForm()
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
            MessageBox.Show("В данной форме можно управлять проектами вашей программы.\nДля того, чтобы добавить проект, нажмите " +
                "кнопку в контекстном меню и в открывшейся форме настройте содержимое нового проекта. Учитывайте, что, если вы захотите добавить " +
                "в новый проект задания из уже существующего, то они переместятся из одного в другой, чтобы поддерживать правило \"Для одного задания " +
                "один проект\", однако ничего не мешает одному заданию быть сразу в нескольких Темах.\nДля удаления безвозвратного удаления проекта нажмите на иконку справа от его названия. Учтите, что задания проекта " +
                "не безвозвратно удалятся, а лишь будут отвязаны от него.\nДважды кликнув по названию проекта, можно будет изменить его название. Изменять текст" +
                " можно будет до тех пор, пока курсор находится на буквах. При его переносе проект сохранит введенное название. Точно также можно менять названия " +
                "задач в проекте. \nВ каждом проекте для каждого его задания отображаются тип задания, его статус, а также " +
                " либо его подзадачи, либо его исполнители. \nДля добавления заданий в существующий проект или их изменения нужно нажать на кнопку плюса " +
                "в разделе \"Задания\". В открывающемся окне можно будет настроить содержимое конкретного проекта, выбрав существующие или добавив новые задачи. " +
                "Правило о заданиях и проектах также будет работать при таких действиях.\nВ задачах типа Epic можно таким же образом редактировать подзадачи, а " +
                "в остальных выбирать исполнителей. Работа формы с исполнителями аналогичная, с учетом особенностей типов задач. Каждую подзадачу или исполнителя " +
                "можно удалить, нажав на голубую иконку слева от названия, или добавив, нажав на зеленый плюс слева от слова \"Подзадачи\"/\"Исполнитель\".\n" +
                "При наведении курсора на задачу, проект или пользователя, на некоторое время будет показана информация о нем.",
                "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ProjectsForm_Load(object sender, EventArgs e)
        {
            goals.Sort();
            tsmiAdd.Click += (s, e) => { new AddProjectForm().ShowDialog(); this.Hide(); new ProjectsForm().ShowDialog(); };
            tsmiHelp.Click += ShowHelp;
            this.Width += 10;
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

            this.HorizontalScroll.Value = 0;
            this.VerticalScroll.Value = 0;

            Point prevPoint = new Point(10, 30);
            foreach (Project project in projects)
            {
                project.Goals.Sort();
                Panel panel = new Panel()
                {
                    AutoSize = true,
                    Width = 800,
                    Height = 500,
                    AutoScroll = true,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = prevPoint,
                };


                TextBox txtbName = new TextBox()
                {
                    Location = new Point(3, 3),
                    Size = new Size(panel.Width - 8, 50),
                    Text = project.Name,
                    Font = new Font(Font.FontFamily, Font.Size + 3, FontStyle.Bold),
                    TextAlign = HorizontalAlignment.Center,
                    BorderStyle = BorderStyle.FixedSingle,
                    ReadOnly = true
                };
                txtbName.MouseDoubleClick += (s, e) => { txtbName.ReadOnly = false; };
                txtbName.MouseLeave += (s, e) => { txtbName.ReadOnly = true; project.Name = txtbName.Text; };
                toolTip.SetToolTip(txtbName, project.ToString());

                Button btnDeleteProject = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(txtbName.Height - 2, txtbName.Height - 2),
                    Location = new Point(txtbName.Left + txtbName.Width - txtbName.Height, txtbName.Top + 2),
                    BackColor = Color.White,
                    BackgroundImage = Resources.Delete2,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    Name = project.Name + "~" + DateTime.Now,
                };
                btnDeleteProject.Click += DeleteProject;
                panel.Controls.Add(btnDeleteProject);
                btnDeleteProject.BringToFront();

                Button btnTasks = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(txtbName.Width, txtbName.Height + 8),
                    Location = new Point(txtbName.Left, txtbName.Top + txtbName.Height + 5),
                    Text = "Задания:",
                };

                Point prevTask = new Point(btnTasks.Left, btnTasks.Top + btnTasks.Height);
                foreach (var goal in project.Goals)
                {
                    Panel pGoal = new Panel()
                    {
                        //TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(txtbName.Width - 2, txtbName.Height * 2 + 2),
                        Location = new Point(prevTask.X, prevTask.Y + 5),
                        BackColor = Color.White,
                        AutoScroll = true,
                        BorderStyle = BorderStyle.FixedSingle,
                        AutoSize = true,
                    };
                    panel.Controls.Add(pGoal);

                    TextBox tbGoal = new TextBox()
                    {
                        //AutoSize = false,
                        //Location = new Point(pbType.Left + pbType.Width, pGoal.Top + 1),
                        //Size = new Size(pGoal.Left + pGoal.Width - (pbType.Left + pbType.Width * 2 + 2) - 2, pGoal.Height / 2 - 2),
                        //Width = panel.Width - tbGoal.Height * 2;
                        //Height = Font.Height+2,
                        Font = new Font(Font.FontFamily, Font.Size + 1, FontStyle.Italic),
                        TextAlign = HorizontalAlignment.Left,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                        Name = goal.Name + "~" + DateTime.Now,
                        ReadOnly = true,
                    };
                    tbGoal.MouseDoubleClick += (s, e) => { tbGoal.ReadOnly = false; };
                    tbGoal.MouseLeave += (s, e) => { tbGoal.ReadOnly = true; goal.Name = tbGoal.Text; };
                    toolTip.SetToolTip(tbGoal, goal.ToString());


                    PictureBox pbType = new PictureBox()
                    {
                        Location = new Point(3, 3),
                        Size = new Size(tbGoal.Height, tbGoal.Height),
                        BackgroundImageLayout = ImageLayout.Zoom,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    tbGoal.Location = new Point(pbType.Left + pbType.Width + 1, pbType.Top);
                    tbGoal.Width = panel.Width - tbGoal.Left - pbType.Width * 2 - 10;
                    PictureBox pbStatus = new PictureBox()
                    {
                        Location = new Point(tbGoal.Left + tbGoal.Width - 4, tbGoal.Top),
                        Size = pbType.Size,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    Button btnDelete = new Button()
                    {
                        FlatStyle = FlatStyle.Flat,
                        Size = pbType.Size,
                        Location = new Point(pbStatus.Left + pbStatus.Width + 1, pbStatus.Top),
                        BackColor = Color.White,
                        BackgroundImage = Resources.Delete,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        Name = project.Name + "~" + goal.Name + "~" + DateTime.Now,
                    };
                    btnDelete.Click += DeleteGoal;

                    Label lblProject = new Label()
                    {
                        AutoSize = true,
                        Location = new Point(tbGoal.Left, tbGoal.Top + tbGoal.Height + 5),
                        //Size = new Size(pGoal.Width /3, pGoal.Height),
                        Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Regular),
                        TextAlign = ContentAlignment.MiddleLeft,
                        BorderStyle = BorderStyle.None,
                        BackColor = Color.White,
                    };

                    if (goal is Epic)
                    {
                        Epic epic = goal as Epic;
                        pbType.BackgroundImage = Resources.Epic;
                        tbGoal.ForeColor = Color.Lime;
                        lblProject.Text = " Подзадачи:";

                        Button btnFix = new Button()
                        {
                            FlatStyle = FlatStyle.Flat,
                            Size = new Size(pbStatus.Height, pbStatus.Height),
                            Location = new Point(pbType.Left, pbType.Top + pbType.Height + 2),
                            BackColor = Color.White,
                            BackgroundImage = Resources.Add,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            //Name = "btnFix",
                        };
                        pGoal.Controls.Add(btnFix);
                        btnFix.Click += (s, e) =>
                        {
                            EditTaskForm etf = new EditTaskForm() { selectedUser = new User("EpicEditing"), selectedGoals = new List<string>() };
                            foreach (var goal in epic.Tasks)
                                etf.selectedGoals.Add(goal.Name);
                            etf.selectedUser.Tasks.Add(new Goal(epic.Name, DateTime.Now));
                            etf.ShowDialog();

                            epic.Tasks.Clear();
                            foreach (var item in etf.selectedUser.Tasks)
                                //if(!project.Goals.Contains(item))
                                epic.Tasks.Add(item);
                            epic.Tasks.Sort();

                            MainForm.SaveData();
                            this.Hide();
                            new ProjectsForm().ShowDialog();
                            this.Close();
                        };


                        Point previousTask = new Point(lblProject.Left + btnDelete.Width, lblProject.Top + lblProject.Height + 15);
                        foreach (var task in epic.Tasks)
                        {
                            Button btnTask = new Button()
                            {
                                AutoSize = true,
                                Location = previousTask,
                                Width = tbGoal.Width - btnDelete.Width,
                                Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                FlatStyle = FlatStyle.Flat,
                                BackColor = Color.White,

                            };
                            toolTip.SetToolTip(btnTask, task.ToString());

                            PictureBox pbT = new PictureBox()
                            {
                                Location = new Point(previousTask.X - btnDelete.Width, previousTask.Y),
                                Size = new Size(btnDelete.Height, btnDelete.Height),
                                BackgroundImageLayout = ImageLayout.Zoom,
                                BorderStyle = BorderStyle.FixedSingle
                            };
                            if (task is Story)
                            {
                                pbT.BackgroundImage = Resources.Story;
                                btnTask.ForeColor = Color.Yellow;
                                btnTask.Text = task.Name + " (Story)";
                            }
                            if (task is Task)
                            {
                                pbT.BackgroundImage = Resources.Task;
                                btnTask.ForeColor = Color.Blue;
                                btnTask.Text = task.Name + " (Task)";
                            }
                            if (task is Bug)
                            {
                                pbT.BackgroundImage = Resources.Bug;
                                btnTask.ForeColor = Color.Red;
                                btnTask.Text = task.Name + " (Bug)";
                            }

                            PictureBox pbS = new PictureBox()
                            {
                                Location = new Point(btnTask.Left + btnTask.Width + 2, previousTask.Y),
                                Size = new Size(btnDelete.Height, btnDelete.Height),
                                BackgroundImageLayout = ImageLayout.Zoom,
                                BorderStyle = BorderStyle.FixedSingle
                            };
                            if (task.Status == "Открытая")
                                pbS.BackgroundImage = Resources.Open;
                            if (task.Status == "В работе")
                                pbS.BackgroundImage = Resources.Working;
                            if (task.Status == "Завершенная")
                                pbS.BackgroundImage = Resources.Closed;


                            Button deleteTask = new Button()
                            {
                                Location = new Point(pbT.Left /*+ btnUser.Width + 2*/ - btnDelete.Width - 2, btnTask.Top),
                                Size = btnDelete.Size,
                                FlatStyle = FlatStyle.Flat,
                                BackColor = Color.White,
                                BackgroundImage = Resources.Delete2,
                                BackgroundImageLayout = ImageLayout.Zoom,
                                Name = task.Name + "~" + epic.Name + "~" + DateTime.Now,
                            };
                            deleteTask.Click += DeleteEpicTask;
                            pGoal.Controls.Add(deleteTask);
                            pGoal.Controls.Add(pbT);
                            pGoal.Controls.Add(pbS);

                            btnTask.FlatAppearance.BorderSize = 0;
                            pGoal.Controls.Add(btnTask);
                            previousTask.Y += btnTask.Height + 2;
                        }
                        if (epic.Tasks.Count == 0)
                            pGoal.Controls.Add(new Label()
                            {
                                AutoSize = true,
                                Location = previousTask,
                                Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                BorderStyle = BorderStyle.None,
                                BackColor = Color.White,
                                ForeColor = Color.Red,
                                Text = "* Нет подзадач"
                            });
                    }
                    if (goal is Story)
                    {
                        pbType.BackgroundImage = Resources.Story;
                        tbGoal.ForeColor = Color.Yellow;
                        lblProject.Text = "Исполнители:";
                    }
                    if (goal is Task)
                    {
                        pbType.BackgroundImage = Resources.Task;
                        tbGoal.ForeColor = Color.Blue;
                        lblProject.Text = "Исполнитель:";
                    }
                    if (goal is Bug)
                    {
                        pbType.BackgroundImage = Resources.Bug;
                        tbGoal.ForeColor = Color.Red;
                        lblProject.Text = "Исполнитель:";
                    }
                    tbGoal.Text = goal.Name;

                    if (goal is Story)
                    {
                        Story story = goal as Story;
                        Button btnFix = new Button()
                        {
                            FlatStyle = FlatStyle.Flat,
                            Size = new Size(pbStatus.Height, pbStatus.Height),
                            Location = new Point(pbType.Left, pbType.Top + pbType.Height + 2),
                            BackColor = Color.White,
                            BackgroundImage = Resources.Add,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            //Name = "btnFix",
                        };
                        pGoal.Controls.Add(btnFix);
                        btnFix.Click += (s, e) =>
                        {
                            EditUsersForm euf = new EditUsersForm() { selectedGoal = goal, selectedUsers = new List<string>() };
                            foreach (var user in story.Users)
                                euf.selectedUsers.Add(user.Name);
                            euf.ShowDialog();

                            this.Hide();
                            new ProjectsForm().ShowDialog();
                            this.Close();
                        };



                        Point prevUser = new Point(lblProject.Left, lblProject.Top + lblProject.Height + 8);

                        foreach (User user in story.Users)
                        {

                            Button btnUser = new Button()
                            {
                                AutoSize = true,
                                Location = prevUser,
                                Width = tbGoal.Width,
                                Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                FlatStyle = FlatStyle.Flat,
                                BackColor = Color.White,
                                //Name = goal.Name + "~" + DateTime.Now,

                                Text = "* " + user.Name,
                            };
                            toolTip.SetToolTip(btnUser, user.ToString());


                            Button deleteUser = new Button()
                            {
                                Location = new Point(btnUser.Left /*+ btnUser.Width + 2*/ - btnDelete.Width - 1, btnUser.Top + 2),
                                Size = btnDelete.Size,
                                FlatStyle = FlatStyle.Flat,
                                BackColor = Color.White,
                                BackgroundImage = Resources.Delete2,
                                BackgroundImageLayout = ImageLayout.Zoom,
                                Name = user.Name + "~" + goal.Name + "~" + DateTime.Now,
                            };
                            deleteUser.Click += DeleteUser;
                            pGoal.Controls.Add(deleteUser);

                            btnUser.FlatAppearance.BorderSize = 0;
                            pGoal.Controls.Add(btnUser);
                            prevUser.Y += btnUser.Height + 2;
                        }
                        if (story.Users.Count == 0)
                            pGoal.Controls.Add(new Label()
                            {
                                AutoSize = true,
                                Location = prevUser,
                                Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                BorderStyle = BorderStyle.None,
                                BackColor = Color.White,
                                ForeColor = Color.Red,
                                Text = "* Нет исполнителей"
                            });
                    }
                    if (goal is Task)
                    {
                        Point prevUser = new Point(lblProject.Left, lblProject.Top + lblProject.Height + 10);
                        Task task = goal as Task;
                        Button btnFix = new Button()
                        {
                            FlatStyle = FlatStyle.Flat,
                            Size = new Size(pbStatus.Height, pbStatus.Height),
                            Location = new Point(pbType.Left, pbType.Top + pbType.Height + 2),
                            BackColor = Color.White,
                            BackgroundImage = Resources.Add,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            //Name = "btnFix",
                        };
                        pGoal.Controls.Add(btnFix);
                        btnFix.Click += (s, e) =>
                        {
                            EditUsersForm euf = new EditUsersForm() { selectedGoal = goal, selectedUsers = new List<string>() };
                            euf.selectedUsers.Add(task.User.Name);
                            euf.ShowDialog();

                            this.Hide();
                            new ProjectsForm().ShowDialog();
                            this.Close();
                        };

                        if (task.User.Name == "Нет исполнителя")

                            pGoal.Controls.Add(new Label()
                            {
                                AutoSize = true,
                                Location = prevUser,
                                Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                BorderStyle = BorderStyle.None,
                                BackColor = Color.White,
                                ForeColor = Color.Red,
                                Text = "* Нет исполнителя"
                            });
                        else
                        {
                            Button btnUser = new Button()
                            {
                                AutoSize = true,
                                Location = prevUser,
                                Width = tbGoal.Width,
                                Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                FlatStyle = FlatStyle.Flat,
                                BackColor = Color.White,
                                //Name = goal.Name + "~" + DateTime.Now,

                                Text = "* " + task.User.Name,
                            };
                            toolTip.SetToolTip(btnUser, task.User.ToString());


                            Button deleteUser = new Button()
                            {
                                Location = new Point(btnUser.Left /*+ btnUser.Width + 2*/ - btnDelete.Width - 2, btnUser.Top + 2),
                                Size = btnDelete.Size,
                                FlatStyle = FlatStyle.Flat,
                                BackColor = Color.White,
                                BackgroundImage = Resources.Delete2,
                                BackgroundImageLayout = ImageLayout.Zoom,
                                Name = task.User.Name + "~" + goal.Name + "~" + DateTime.Now,
                            };
                            deleteUser.Click += DeleteUser;
                            pGoal.Controls.Add(deleteUser);

                            btnUser.FlatAppearance.BorderSize = 0;
                            pGoal.Controls.Add(btnUser);
                        }

                    }

                    if (goal is Bug)
                    {
                        Point prevUser = new Point(lblProject.Left, lblProject.Top + lblProject.Height + 10);
                        Bug bug = goal as Bug;

                        Button btnFix = new Button()
                        {
                            FlatStyle = FlatStyle.Flat,
                            Size = new Size(pbStatus.Height, pbStatus.Height),
                            Location = new Point(pbType.Left, pbType.Top + pbType.Height + 2),
                            BackColor = Color.White,
                            BackgroundImage = Resources.Add,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            //Name = "btnFix",
                        };
                        pGoal.Controls.Add(btnFix);
                        btnFix.Click += (s, e) =>
                        {
                            EditUsersForm euf = new EditUsersForm() { selectedGoal = goal, selectedUsers = new List<string>() };
                            euf.selectedUsers.Add(bug.User.Name);
                            euf.ShowDialog();

                            this.Hide();
                            new ProjectsForm().ShowDialog();
                            this.Close();
                        };


                        if (bug.User.Name == "Нет исполнителя")
                            pGoal.Controls.Add(new Label()
                            {
                                AutoSize = true,
                                Location = prevUser,
                                Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                BorderStyle = BorderStyle.None,
                                BackColor = Color.White,
                                ForeColor = Color.Red,
                                Text = "* Нет исполнителя"
                            });
                        else
                        {
                            Button btnUser = new Button()
                            {
                                AutoSize = true,
                                Location = prevUser,
                                Width = tbGoal.Width,
                                Font = new Font(Font.FontFamily, Font.Size - 1, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleLeft,
                                FlatStyle = FlatStyle.Flat,
                                BackColor = Color.White,
                                //Name = goal.Name + "~" + DateTime.Now,

                                Text = "* " + bug.User.Name,
                            };
                            toolTip.SetToolTip(btnUser, bug.User.ToString());


                            Button deleteUser = new Button()
                            {
                                Location = new Point(btnUser.Left /*+ btnUser.Width + 2*/ - btnDelete.Width - 2, btnUser.Top + 2),
                                Size = btnDelete.Size,
                                FlatStyle = FlatStyle.Flat,
                                BackColor = Color.White,
                                BackgroundImage = Resources.Delete2,
                                BackgroundImageLayout = ImageLayout.Zoom,
                                Name = bug.User.Name + "~" + goal.Name + "~" + DateTime.Now,
                            };
                            deleteUser.Click += DeleteUser;
                            pGoal.Controls.Add(deleteUser);

                            btnUser.FlatAppearance.BorderSize = 0;
                            pGoal.Controls.Add(btnUser);
                        }

                    }



                    if (goal.Status == "Открытая")
                        pbStatus.BackgroundImage = Resources.Open;
                    if (goal.Status == "В работе")
                        pbStatus.BackgroundImage = Resources.Working;
                    if (goal.Status == "Завершенная")
                        pbStatus.BackgroundImage = Resources.Closed;


                    pGoal.Controls.Add(pbType);
                    pGoal.Controls.Add(pbStatus);
                    pGoal.Controls.Add(tbGoal);
                    pGoal.Controls.Add(btnDelete);
                    pGoal.Controls.Add(lblProject);
                    //pGoal.Controls.Add(tbProject);
                    //tbProject.BringToFront();

                    prevTask = new Point(pGoal.Left, pGoal.Top + pGoal.Height);

                    //this.Select();

                }

                panel.Controls.Add(txtbName);
                panel.Controls.Add(btnTasks);

                Button btnAdd = new Button()
                {
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(btnTasks.Height - 2, btnTasks.Height - 2),
                    Location = new Point(btnTasks.Left + 1, btnTasks.Top + 1),
                    BackColor = Color.White,
                    BackgroundImage = Resources.Add,
                    BackgroundImageLayout = ImageLayout.Zoom,
                };
                panel.Controls.Add(btnAdd);
                btnAdd.Click += (s, e) =>
                {
                    EditTaskForm etf = new EditTaskForm() { selectedUser = new User("ProjectEditing"), selectedGoals = new List<string>() };
                    foreach (var goal in project.Goals)
                        etf.selectedGoals.Add(goal.Name);
                    etf.selectedUser.Tasks.Add(new Goal(project.Name, DateTime.Now));
                    etf.ShowDialog();

                    project.Goals.Clear();
                    foreach (var item in etf.selectedUser.Tasks)
                        //if(!project.Goals.Contains(item))
                        project.Goals.Add(item);
                    project.Goals.Sort();

                    MainForm.SaveData();
                    this.Hide();
                    new ProjectsForm().ShowDialog();
                    this.Close();
                };
                btnAdd.BringToFront();


                this.Select();
                Controls.Add(panel);
                prevPoint.Y += panel.Height + 10;
                this.HorizontalScroll.Value = 0;
                this.VerticalScroll.Value = 0;

            }
            this.HorizontalScroll.Value = 0;
            this.VerticalScroll.Value = 0;
        }
        /// <summary>
        /// Удаление данных.
        /// </summary>
        private void DeleteProject(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            try
            {
                string[] parts = btn.Name.Split('~');
                string projectName = parts[0];
                DialogResult res = MessageBox.Show($"Вы действительно хотите удалить проект {projectName}?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    foreach (Project project in projects)
                        if (project.Name == projectName)
                        {
                            for (int i = 0; i < project.Goals.Count; i++)
                            {
                                Goal goal = project.Goals[i];
                                project.Goals.Remove(goal);
                                goal.isInProject = false;
                                foreach (Project p in projects)
                                    if (p.Goals.Contains(goal))
                                        goal.isInProject = true;
                                i--;
                            }
                            projects.Remove(project);
                            break;
                        }

                    this.Close();
                    isRestarting = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("При удалении проекта произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteEpicTask(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            try
            {
                string[] parts = btn.Name.Split('~');
                string taskName = parts[0];
                string epicName = parts[1];
                DialogResult res = MessageBox.Show($"Вы действительно хотите удалить подзадачу {taskName} из темы {epicName}?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    foreach (var goal in goals)
                        if (goal is Epic && goal.Name == epicName)
                        {
                            Epic epic = goal as Epic;
                            foreach (var task in epic.Tasks)
                                if (task.Name == taskName)
                                {
                                    epic.Tasks.Remove(task);
                                    epic.Tasks.Sort();

                                    this.Close();
                                    isRestarting = true;
                                    //Display();
                                    return;
                                }
                        }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("При удалении подзадачи произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteUser(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            try
            {
                string[] parts = btn.Name.Split('~');
                string userName = parts[0];
                string goalName = parts[1];
                DialogResult res = MessageBox.Show($"Вы действительно хотите удалить исполнителя {userName} у задачи {goalName}?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    foreach (var goal in goals)
                        if (goal.Name == goalName)
                        {
                            if (goal is Story)
                            {
                                Story assGoal = goal as Story;
                                foreach (User user in assGoal.Users)
                                    if (user.Name == userName)
                                    {
                                        assGoal.Users.Remove(user);
                                        user.Tasks.Remove(goal);
                                        user.Tasks.Sort();
                                        this.Close();
                                        isRestarting = true;
                                        //Display();
                                        return;
                                    }
                            }
                            if (goal is Task)
                            {
                                Task task = goal as Task;
                                task.User.Tasks.Remove(goal);
                                task.User = new User("Нет исполнителя");
                                //task.User.Tasks.Sort();
                                this.Close();
                                isRestarting = true;
                                //Display();
                                return;
                            }
                            if (goal is Bug)
                            {
                                Bug bug = goal as Bug;
                                bug.User.Tasks.Remove(goal);
                                bug.User = new User("Нет исполнителя");
                                this.Close();
                                isRestarting = true;
                                return;
                            }

                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("При удалении исполнителя произошла ошибка. " +
                "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteGoal(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            try
            {
                string[] parts = btn.Name.Split('~');
                string projectName = parts[0];
                string goalName = parts[1];
                DialogResult res = MessageBox.Show($"Вы действительно хотите удалить задачу {goalName} из списка задач проекта {projectName}?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    foreach (Project project in projects)
                        if (project.Name == projectName)
                            foreach (var goal in project.Goals)
                                if (goal.Name == goalName)
                                {
                                    project.Goals.Remove(goal);
                                    project.Goals.Sort();


                                    goal.isInProject = false;
                                    foreach (Project p in projects)
                                    {
                                        if (goal.isInProject)
                                            break;
                                        foreach (var g in p.Goals)
                                            if (g.Name == goal.Name)
                                            {
                                                goal.isInProject = true;
                                                break;
                                            }
                                    }

                                    this.Close();
                                    isRestarting = true;
                                    //Display();
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
        /// 
        private void ProjectsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.SaveData();
        }

        private void ProjectsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isRestarting)
            {
                this.Hide();
                ProjectsForm pf = new ProjectsForm();
                pf.ShowDialog();
                this.Close();
            }
        }

        
    }
}
