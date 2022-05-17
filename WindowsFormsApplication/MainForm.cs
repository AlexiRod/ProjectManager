using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ConsoleApplication;
using System.IO;
using ProjectLibrary;

namespace WindowsFormsApplication
{
    public partial class MainForm : Form
    {
        public static List<User> users = new List<User>();
        public static List<Project> projects = new List<Project>();
        public static List<Goal> goals = new List<Goal>();


        public static string pathToFiles = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())))),
            "ConsoleApplication", "bin", "Debug", "netcoreapp3.1");
        static string pathGoals = Path.Combine(pathToFiles, "goals.txt");
        static string pathUsers = Path.Combine(pathToFiles, "users.txt");
        static string pathProjects = Path.Combine(pathToFiles, "projects.txt");


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            UsersForm usersForm = new UsersForm();
            usersForm.ShowDialog();
        }
        private void btnProjects_Click(object sender, EventArgs e)
        {
            ProjectsForm projectsForm = new ProjectsForm();
            projectsForm.ShowDialog();
        }
        private void btnGoals_Click(object sender, EventArgs e)
        {
            GoalsForm goalsForm = new GoalsForm();
            goalsForm.ShowDialog();
        }


        /// <summary>
        /// Загрузка данных
        /// </summary>
        public static void LoadData()
        {
            try
            {
                string[] lines = File.ReadAllLines(pathGoals);
                List<Epic> epics = new List<Epic>();
                foreach (string item in lines)
                {
                    string[] data = item.Split('~');
                    string[] parts = data[0].Split('^');


                    DateTime d = new DateTime();
                    DateTime.TryParse(parts[2], out d);
                    bool isIn = parts[4] == "True" ? true : false;
                    if (parts[0].Contains("Story"))
                    {
                        Story story = new Story(parts[1], d, parts[3]);
                        story.isInProject = isIn;
                        goals.Add(story);
                    }
                    else if (parts[0].Contains("Task"))
                    {
                        Task task = new Task(parts[1], d, parts[3]);
                        task.isInProject = isIn;
                        goals.Add(task);
                    }
                    else if (parts[0].Contains("Bug"))
                    {
                        Bug bug = new Bug(parts[1], d, parts[3]);
                        bug.isInProject = isIn;
                        goals.Add(bug);
                    }
                    else if (parts[0].Contains("Epic"))
                    {
                        Epic epic = new Epic(parts[1], d, parts[3]);
                        epics.Add(epic);
                        goals.Add(epic);
                    }
                    goals.Sort();
                }
                int i = 0;
                foreach (string item in lines)
                {
                    string[] data = item.Split('~');
                    string[] parts = data[0].Split('^');

                    if (parts[0].Contains("Epic"))
                    {
                        string[] tasks = data[1].Split(';');
                        foreach (string task in tasks)
                            foreach (var g in goals)
                                if (g.Name == task)
                                {
                                    epics[i].Tasks.Add(g);
                                    epics[i].Tasks.Sort();
                                }

                        i++;
                    }
                }


                lines = File.ReadAllLines(pathUsers);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('~');
                    string[] tasks = parts[1].Split(';');
                    User user = new User(parts[0]);
                    foreach (var task in tasks)
                        foreach (var g in goals)
                            if (g.Name == task)
                            {
                                user.Tasks.Add(g);
                                user.Tasks.Sort();
                                if (g is Story)
                                {
                                    Story story = g as Story;
                                    story.Users.Add(user);
                                }
                                if (g is Task)
                                {
                                    Task t = g as Task;
                                    t.User = user;
                                }
                                if (g is Bug)
                                {
                                    Bug b = g as Bug;
                                    b.User = user;
                                }
                            }
                    users.Add(user);
                }

                lines = File.ReadAllLines(pathProjects);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('~');
                    string[] tasks = parts[1].Split(';');
                    Project project = new Project(parts[0]);
                    foreach (var task in tasks)
                        foreach (var g in goals)
                            if (g.Name == task)
                            {
                                project.Goals.Add(g);
                                project.Goals.Sort();
                            }

                    projects.Add(project);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("При загрузке данных проекта произошла ошибка, все списки и данные сейчас пустые. " +
                    "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                users = new List<User>();
                projects = new List<Project>();
                goals = new List<Goal>();
            }
        }

        /// <summary>
        /// Сохранение данных
        /// </summary>
        public static void SaveData()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (Project project in projects)
                {
                    string line = project.Name + "~";
                    foreach (Goal goal in project.Goals)
                        line += goal.Name + ";";
                    lines.Add(line);
                }
                File.WriteAllLines(pathProjects, lines);

                lines = new List<string>();
                foreach (User user in users)
                {
                    string line = user.Name + "~";
                    foreach (var goal in user.Tasks)
                        line += goal.Name + ";";
                    lines.Add(line);
                }
                File.WriteAllLines(pathUsers, lines);

                lines = new List<string>();
                foreach (Goal goal in goals)
                {
                    string line = string.Join('^', goal.GetType(), goal.Name, goal.Date, goal.Status, goal.isInProject) + "~";

                    if (goal is Epic)
                    {
                        Epic epic = goal as Epic;
                        foreach (var task in epic.Tasks)
                            line += task.Name + ";";
                        lines.Add(line);
                    }
                    if (goal is Story)
                    {
                        Story assGoal = goal as Story;
                        foreach (var user in assGoal.Users)
                            line += user.Name + ";";
                        lines.Add(line);
                    }
                    if (goal is Task)
                    {
                        Task t = goal as Task;
                        line += t.User.Name + ";";
                        lines.Add(line);
                    }
                    if (goal is Bug)
                    {
                        Bug b = goal as Bug;
                        line += b.User.Name + ";";
                        lines.Add(line);
                    }

                }
                File.WriteAllLines(pathGoals, lines);

            }
            catch (Exception ex)
            {
                MessageBox.Show("При сохранении данных проекта произошла ошибка, сохранены только корректно работающие данные. " +
                    "Сообщение ошибки: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
