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
    public partial class EditTaskForm : Form
    {
        public static List<User> users = new List<User>();
        public static List<Project> projects = new List<Project>();
        public static List<Goal> goals = new List<Goal>();
        public List<string> selectedGoals = new List<string>();
        private List<string> goalsForNewForm = new List<string>();
        public User selectedUser = new User("");

        public EditTaskForm()
        {
            InitializeComponent();
            users = MainForm.users;
            projects = MainForm.projects;
            goals = MainForm.goals;
        }

        List<CheckBox> checkBoxes = new List<CheckBox>();
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<Button> buttons = new List<Button>();
        private void EditTaskForm_Load(object sender, EventArgs e)
        {
            Display();
        }
        Point prevGoal = new Point(4, 10);
        int maxLeft = 0;


        /// <summary>
        /// Отображение всех данных на форме (выглядит страшно) 
        /// </summary>
        private void Display()
        {
            prevGoal = new Point(4, 10);
            maxLeft = 10;

            buttonSave.BackgroundImage = Resources.Closed;
            buttonSave.Click += (s, e) => { SaveChanges();this.Close(); };

            if (selectedUser.Name == "ProjectEditing")
                lblTasks.Text = "Список задач проекта " + selectedUser.Tasks[0].Name + ":";
            else if (selectedUser.Name == "EpicEditing")
                lblTasks.Text = "Список подзадач Темы " + selectedUser.Tasks[0].Name + ":";
            else
                lblTasks.Text = "Список задач пользователя " + selectedUser.Name + ":";



            foreach (var goal in goals)
            {
                CheckBox checkBox = new CheckBox()
                {
                    TextImageRelation = TextImageRelation.ImageBeforeText,
                    Font = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Italic),
                    Location = prevGoal,
                    TextAlign = ContentAlignment.TopLeft,
                    AutoSize = true,
                    BackColor = Color.White,
                };
                if (selectedGoals.Contains(goal.Name))
                {
                    goalsForNewForm.Add(goal.Name);
                    selectedGoals.Remove(goal.Name);
                    checkBox.Checked = true;
                }


                Bitmap bmp = new Bitmap(1, 1);
                if (goal is Epic)
                {
                    if (selectedUser.Name != "ProjectEditing")
                        continue;

                    bmp = Resources.Epic;
                    checkBox.ForeColor = Color.Lime;
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
                    if (selectedUser.Name == "EpicEditing")
                        continue;
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

            Button btnAdd = new Button()
            {
                FlatStyle = FlatStyle.Flat,
                Size = new Size(30, 30),
                Location = prevGoal,
                BackColor = Color.White,
                BackgroundImage = Resources.Add,
                BackgroundImageLayout = ImageLayout.Zoom,
                Name = "btnAdd",
            };
            panelTasks.Controls.Add(btnAdd);
            btnAdd.Click += (s, e) =>
            {
                AddTaskForm atf = new AddTaskForm();
                atf.ShowDialog();
                MainForm.SaveData();

                if (atf.newGoal.Name != "")
                    AddNewGoal(atf.newGoal);

                //foreach (Control control in panelTasks.Controls)
                //{
                //    panelTasks.Controls.Remove(control);
                //    control.Dispose();
                //}
                //Display();
                //this.Hide();
                //EditTaskForm etf = new EditTaskForm() { selectedGoals = new List<string>() };
                //etf.selectedUser = selectedUser;
                //etf.selectedGoals.Add(atf.newGoal.Name);
                //foreach (var goal in this.goalsForNewForm)
                //    etf.selectedGoals.Add(goal);
                //etf.ShowDialog();

                //this.Close();
            };



            foreach (PictureBox pictureBox in pictureBoxes)
                pictureBox.Left = maxLeft;
            foreach (Button btn in buttons)
                btn.Width += maxLeft - prevGoal.X;
        }

        /// <summary>
        /// Добавление задачи
        /// </summary>
        private void AddNewGoal(Goal goal)
        {
            foreach (Control button in panelTasks.Controls)
                if (button is Button && button.Name == "btnAdd")
                    prevGoal = button.Location;

            CheckBox checkBox = new CheckBox()
            {
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Font = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Italic),
                Location = prevGoal,
                TextAlign = ContentAlignment.TopLeft,
                AutoSize = true,
                BackColor = Color.White,
            };

            Bitmap bmp = new Bitmap(1, 1);

            if (goal is Epic)
            {

                if (selectedUser.Name == "ProjectEditing")
                {
                    bmp = Resources.Epic;
                    checkBox.ForeColor = Color.Lime;
                }
                else
                {
                    if (selectedUser.Name == "EpicEditing")
                        MessageBox.Show($"Добавленная задача является Темой (Epic) и не может быть подзадачей данной Темы, поэтому она не будет отображена в этом списке" +
                            $"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show($"Добавленная задача является Темой (Epic) и не может иметь исполнителей, поэтому она не будет отображена в этом списке" +
                            $"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


            if (goal is Story)
            {
                Story s = goal as Story;


                foreach (var item in s.Users)
                    if (item.Name == selectedUser.Name)
                        checkBox.Checked = true;
                bmp = Resources.Story;
                checkBox.ForeColor = Color.Yellow;
            }
            if (goal is Task)
            {
                Task t = goal as Task;
                if (t.User.Name == selectedUser.Name)
                    checkBox.Checked = true;

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
                if (selectedUser.Name == "EpicEditing")
                {
                    MessageBox.Show($"Добавленная задача является Ошибкой (Bug) и не может быть подзадачей данной Темы, поэтому она не будет отображена в этом списке" +
                       $"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Bug b = goal as Bug;
                if (b.User.Name == selectedUser.Name)
                    checkBox.Checked = true;

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
                Location = new Point(maxLeft, checkBox.Top + 11),
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
            btnBack.Width += maxLeft - prevGoal.X;
            pbStatus.Top = btnBack.Top + btnBack.Height / 2 - pbStatus.Height / 2;
            buttons.Add(btnBack);

            panelTasks.Controls.Add(btnBack);
            btnBack.SendToBack();

            foreach (Control button in panelTasks.Controls)
                if (button is Button && button.Name == "btnAdd")
                    button.Location = prevGoal;
        }

        /// <summary>
        /// Сохранеине
        /// </summary>
        private void SaveChanges()
        {
            foreach (var goal in selectedUser.Tasks)
            {
                if (goal is Story)
                {
                    Story s = goal as Story;
                    s.Users.Remove(selectedUser);
                }
                if (goal is Task)
                {
                    Task t = goal as Task;
                    t.User = new User("Нет исполнителя");
                }
                if (goal is Bug)
                {
                    Bug b = goal as Bug;
                    b.User = new User("Нет исполнителя");
                }
            }
            selectedUser.Tasks.Clear();


            foreach (CheckBox checkBox in checkBoxes)
                if (checkBox.Checked)
                    foreach (var goal in goals)
                        if (goal.Name == checkBox.Text)
                        {
                            if (goal is Epic)
                            {
                                if (selectedUser.Name == "ProjectEditing")
                                {
                                    foreach (Project project in projects)
                                        project.Goals.Remove(goal);

                                    Epic epic = goal as Epic;
                                    selectedUser.Tasks.Add(epic);
                                    selectedUser.Tasks.Sort();
                                }
                            }
                            if (goal is Story)
                            {
                                Story story = goal as Story;
                                if (story.MaxCount <= story.Users.Count && selectedUser.Name != "ProjectEditing")
                                    MessageBox.Show($"Число исполнителей Истории {story.Name} достигло максимума в {story.MaxCount} человек. " +
                                    $"Пользователь {selectedUser.Name} не будет назначен исполнителем для нее.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                else
                                {
                                    if (selectedUser.Name == "ProjectEditing")
                                        foreach (Project project in projects)
                                            project.Goals.Remove(goal);

                                    selectedUser.Tasks.Add(goal);
                                    selectedUser.Tasks.Sort();
                                    if (selectedUser.Name != "ProjectEditing" && selectedUser.Name != "EpicEditing")
                                        story.Users.Add(selectedUser);
                                }
                            }
                            if (goal is Task)
                            {
                                if (selectedUser.Name == "ProjectEditing")
                                    foreach (Project project in projects)
                                        project.Goals.Remove(goal);

                                Task task = goal as Task;
                                if (selectedUser.Name != "ProjectEditing" && selectedUser.Name != "EpicEditing")
                                {
                                    task.User.Tasks.Remove(task);
                                    task.User = selectedUser;
                                }
                                selectedUser.Tasks.Add(goal);
                                selectedUser.Tasks.Sort();
                            }
                            if (goal is Bug)
                            {
                                if (selectedUser.Name == "ProjectEditing")
                                    foreach (Project project in projects)
                                        project.Goals.Remove(goal);

                                if (selectedUser.Name == "EpicEditing")
                                    continue;

                                Bug bug = goal as Bug;
                                if (selectedUser.Name != "ProjectEditing" && selectedUser.Name != "EpicEditing")
                                {
                                    bug.User.Tasks.Remove(bug);
                                    bug.User = selectedUser;
                                }
                                selectedUser.Tasks.Add(goal);
                                selectedUser.Tasks.Sort();
                            }


                            break;
                        }

        }


        /// <summary>
        /// Удаление
        /// </summary>
        private void RemoveFromOtherProject(Goal goal)
        {
            foreach (Project project in projects)
                project.Goals.Remove(goal);

        }

    }
}
