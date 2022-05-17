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
    public partial class GoalsForm : Form
    {
        public static List<User> users = new List<User>();
        public static List<Project> projects = new List<Project>();
        public static List<Goal> goals = new List<Goal>();
        public GoalsForm()
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
            MessageBox.Show("В данной форме можно управлять задачами вашей программы.\nДля того, чтобы добавить задачу, нажмите кнопку " +
                "в контекстном меню или в самом низу списка и в открывшейся форме настройте данные новой задачи. Учитывайте, что, исполнителй" +
                " нельзя назначать на задачи типа Epic, а Task и Bug могут иметь только одного исполнителя.\nДля безвозвратного удаления " +
                "задачи нажмите на синюю иконку слева от ее названия. " +
                "Задча удалится из всех проектов, а также отвяжется от исполнителей.\nДважды кликнув по названию задачи, можно будет изменить его." +
                " Изменять текст можно будет до тех пор, пока курсор находится на буквах. При его переносе задача сохранит введенное название." +
                "\nДля каждого задания отображаются его тип, статус, а также проект, в ктором оно задействовано. Статус задания можно менять " +
                "в выпадающем списке.\nПри наведении курсора на задачу или проект, на некоторое время будет показана информация " +
                "о нем.", "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void GoalsForm_Load(object sender, EventArgs e)
        {
            tsmiAdd.Click += AddGoal;
            tsmiHelp.Click += ShowHelp;
            Display();
            this.Width += 50;
        }

        int size = 80 - 2;
        Point prevPoint = new Point(3, 80);


        /// <summary>
        /// Стартовая отрисовка.
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

            foreach (var goal in goals)
            {
                DisplayGoal(goal);
            }

            Button btnAdd = new Button()
            {
                FlatStyle = FlatStyle.Flat,
                Size = new Size(size, size),
                Location = new Point(prevPoint.X + 2, prevPoint.Y),
                BackColor = Color.White,
                BackgroundImage = Resources.Add,
                BackgroundImageLayout = ImageLayout.Zoom,
            };
            Controls.Add(btnAdd);
            btnAdd.Click += AddGoal;
        }

        /// <summary>
        /// Методы работы с задачей.
        /// </summary>
        private void AddGoal (object sender, EventArgs e)
        {
            AddTaskForm atf = new AddTaskForm();
            atf.ShowDialog();
            MainForm.SaveData();

            if (atf.newGoal.Name != "")
            {

                MainForm.SaveData();
                this.Hide();
                new GoalsForm().ShowDialog();
                this.Close();
            }
        }
        private void DisplayGoal(Goal goal)
        {
            Panel panel = new Panel()
            {
                Width = this.Width - 2,
                Height = size + 2,
                BorderStyle = BorderStyle.FixedSingle,
                Location = prevPoint,
            };
            prevPoint.Y += panel.Height + 4;

            Button btnDelete = new Button()
            {
                FlatStyle = FlatStyle.Flat,
                Size = new Size(size, size),
                Location = new Point(1, 3),
                BackColor = Color.White,
                BackgroundImage = Resources.Delete2,
                BackgroundImageLayout = ImageLayout.Zoom,
                Name = goal.Name + "~" + DateTime.Now,
            };
            btnDelete.Click += DeleteGoal;

            PictureBox pbType = new PictureBox()
            {
                Location = new Point(btnDelete.Left + btnDelete.Width + 2, btnDelete.Top),
                Size = btnDelete.Size,
                BackgroundImageLayout = ImageLayout.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
            };

            TextBox txtbName = new TextBox()
            {
                Location = new Point(pbType.Left + pbType.Width + 2, pbType.Top+2),
                Size = new Size(panel.Width - pbType.Left - size-7, size / 2),
                Text = goal.Name,
                Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Left,
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true
            };
            txtbName.MouseDoubleClick += (s, e) => { txtbName.ReadOnly = false; };
            txtbName.MouseLeave += (s, e) => { txtbName.ReadOnly = true; goal.Name = txtbName.Text; };
            toolTip.SetToolTip(txtbName, goal.ToString());

            if (goal is Epic)
            {
                pbType.BackgroundImage = Resources.Epic;
                txtbName.ForeColor = Color.Lime;
            }
            if (goal is Story)
            {
                pbType.BackgroundImage = Resources.Story;
                txtbName.ForeColor = Color.Yellow;
            }
            if (goal is Task)
            {
                pbType.BackgroundImage = Resources.Task;
                txtbName.ForeColor = Color.Blue;
            }
            if (goal is Bug)
            {
                pbType.BackgroundImage = Resources.Bug;
                txtbName.ForeColor = Color.Red;
            }
            txtbName.Text = goal.Name;


            ComboBox comboBox = new ComboBox()
            {
                Location = new Point(txtbName.Left + txtbName.Width + 2, txtbName.Top + txtbName.Height + 5),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Открытая", "В работе", "Завершенная" },
                Left = txtbName.Left + (int)(txtbName.Width * 0.72f),
            };
            comboBox.Width = panel.Width - comboBox.Left - 6;
            comboBox.SelectedIndexChanged += (s, e) => { goal.Status = comboBox.SelectedItem.ToString(); };
            comboBox.SelectedItem = goal.Status;



            Label lblProject = new Label()
            {
                AutoSize = false,
                Location = new Point(txtbName.Left, txtbName.Top + txtbName.Height + 7),
                Size = new Size(comboBox.Left - txtbName.Left -2, txtbName.Height),
                Font = new Font(Font.FontFamily, Font.Size, FontStyle.Italic),
                TextAlign = ContentAlignment.TopLeft,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
            };

            foreach (Project project in projects)
            {
                foreach (var g in project.Goals)
                {
                    if (g.Name == goal.Name)
                    {
                        if (lblProject.Text != string.Empty)
                            lblProject.Text += "; " + project.Name;
                        else
                            lblProject.Text = project.Name;
                        toolTip.SetToolTip(lblProject, project.ToString());
                        break;
                    }
                }
            }
            if (lblProject.Text == string.Empty)
                lblProject.Text = "Не задействована в проектах";



            panel.Controls.Add(btnDelete);
            panel.Controls.Add(pbType);
            panel.Controls.Add(txtbName);
            panel.Controls.Add(comboBox);
            panel.Controls.Add(lblProject);
            comboBox.BringToFront();

            this.Select();
            Controls.Add(panel);
        }
        private void DeleteGoal(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;
            try
            {
                string[] parts = btn.Name.Split('~');
                string goalName = parts[0];
                DialogResult res = MessageBox.Show($"Вы действительно хотите безвозвратно удалить задачу {goalName}?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Goal goal = FindGoal(goalName);
                    if (goal == null)
                        return;

                    foreach (Project project in projects)
                        foreach (var g in project.Goals)
                            if (g.Name == goal.Name)
                            {
                                project.Goals.Remove(g);
                                break;
                            }

                    if (goal is Epic)
                    {
                        Epic epic = goal as Epic;
                        epic.Tasks.Clear();
                    }
                    if (goal is Story)
                    {
                        Story story = goal as Story;
                        foreach (User user in story.Users)
                            user.Tasks.Remove(goal);
                    }
                    if (goal is Task)
                    {
                        Task task = goal as Task;
                        task.User.Tasks.Remove(goal);
                    }
                    if (goal is Bug)
                    {
                        Bug bug = goal as Bug;
                        bug.User.Tasks.Remove(goal);
                    }
                 
                    goals.Remove(goal);
                    MainForm.SaveData();
                    this.Hide();
                    new GoalsForm().ShowDialog();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("При удалении задачи произошла ошибка. " +
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
