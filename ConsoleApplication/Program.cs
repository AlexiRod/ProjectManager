using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ProjectLibrary;
using System.Drawing;
using System.IO;

namespace ConsoleApplication
{
    class Program
    {
        static List<User> users = new List<User>();
        static List<Project> projects = new List<Project>();
        static List<Goal> goals = new List<Goal>();

        #region Load
        /// <summary>
        /// Справка.
        /// </summary>

        private static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{Environment.NewLine}Главное меню{Environment.NewLine}Достпуные команды:");
            Console.WriteLine("\"-users\" - работа с пользователями.");
            Console.WriteLine("\"-projects\"- работа с проектами");
            Console.WriteLine("\"-tasks\"- работа с задачами в проектах");
            Console.WriteLine("\"-menu\" - переход к меню со списком всех доступных команд и их описанием.");
            Console.WriteLine("\"-exit\" - завершение работы программы.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Команда: ");
        }


        private static SignalHandler signalHandler;
        static void Main(string[] args)
        {
            signalHandler += HandleConsoleSignal;
            ConsoleHelper.SetSignalHandler(signalHandler, true);
            LoadData();
            string command;
            do
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Приветствую в менеджере проектов. Здесь можно управлять проектами (вау).");
                Help();
                command = Console.ReadLine().Trim();

                switch (command)
                {
                    case "-users":
                        WorkWithUsers();
                        break;
                    case "-projects":
                        WorkWithProjects();
                        break;
                    case "-tasks":
                        WorkWithGoals();
                        break;
                    case "-menu":
                        Console.Clear();
                        break;
                    case "-exit":
                        Console.Write("Программа завершена. Удачного дня.");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Write("Неизвестная команда.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            while (true);

        }

        #endregion



        #region Users

        private static void WorkWithUsers()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Доступные команды для работы с пользователями: ");
                Console.WriteLine("\"-show\" - просмотр списка всех существующих пользователей");
                Console.WriteLine("\"-add\" - добавить пользователя");
                Console.WriteLine("\"-remove\" - удалить пользователя");
                Console.WriteLine("\"-menu\" - выход в главное меню");
                Console.Write("Команда: ");

                string command;
            ReadCommand: command = Console.ReadLine().Trim();

                switch (command)
                {
                    case "-show":
                        ShowAllUsers();
                        break;
                    case "-add":
                        AddUser();
                        break;
                    case "-remove":
                        RemoveUser();
                        break;
                    case "-menu":
                        Console.Clear();
                        return;

                    default:
                        Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                        goto ReadCommand;
                }
            }
        }

        /// <summary>
        /// Методы работы с пользователями
        /// </summary>

        private static void ShowAllUsers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Общее число зарегестрированных пользователей: {users.Count}. Список пользователей:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            int count = 1;
            foreach (User user in users)
                Console.WriteLine(count++ + ") " + user);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Для продолжения нажмите любую кнопку");
            Console.ReadKey();
        }

        private static void AddUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Введите \"-add <Имя_пользователя>\", чтобы добавить нового пользователя с указанным именем или \"-close\", чтобы вернуться назад: ");
        ReadCommand:
            string command = Console.ReadLine().Trim();
            string[] parts = command.Split(new char[] { ' ' });
            if (parts.Length == 0 || parts[0] == null)
            {
                Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                goto ReadCommand;
            }
            switch (parts[0])
            {
                case "-close":
                    return;
                case "-add":
                    if (parts.Length < 2)
                    {
                        Console.Write("Необходимо ввести имя пользователя. Пожалуйста, повторите ввод: ");
                        goto ReadCommand;
                    }

                    string name = "";
                    for (int i = 1; i < parts.Length; i++)
                        name += parts[i] + " ";

                    foreach (User user in users)
                        if (user.Name == name)
                        {
                            Console.Write($"Пользователь с именем {name} уже существует. Пожалуйста, повторите ввод: ");
                            goto ReadCommand;
                        }

                    users.Add(new User(name.Trim()));
                    Console.WriteLine($"Пользователь {name} успешно добавлен в список. ");
                    Console.ReadLine();
                    break;
                default:
                    Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                    goto ReadCommand;
            }
        }

        private static void RemoveUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
        ReadCommand: Console.Write("Введите \"-remove <Имя_пользователя>\", чтобы удалить пользователя с указанным именем или \"-close\", чтобы вернуться назад: ");

            string command = Console.ReadLine().Trim();
            string[] parts = command.Split(new char[] { ' ' });
            if (parts.Length == 0 || parts[0] == null)
            {
                Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                goto ReadCommand;
            }
            switch (parts[0])
            {
                case "-close":
                    return;
                case "-remove":
                    if (parts.Length < 2)
                    {
                        Console.Write("Необходимо ввести имя пользователя. Пожалуйста, повторите ввод: ");
                        goto ReadCommand;
                    }

                    string name = "";
                    for (int i = 1; i < parts.Length; i++)
                        name += parts[i] + " ";

                    if (!FindAndRemoveUser(name.Trim()))
                    {
                        Console.WriteLine($"Пользователя с указанным именем {name} нет в списке. Повторите попытку.");
                        goto ReadCommand;
                    }

                    Console.WriteLine($"Пользователь {name} успешно удален из списка.");
                    Console.ReadLine();
                    break;
                default:
                    Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                    goto ReadCommand;
            }
        }

        #endregion


        #region Projects

        private static void WorkWithProjects()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Доступные команды для работы с проектами: ");
                Console.WriteLine("\"-show\" - просмотр списка всех существующих проектов и их изменение");
                Console.WriteLine("\"-add\" - добавить проект");
                Console.WriteLine("\"-fix\" - изменить название проекта");
                Console.WriteLine("\"-remove\" - удалить проект");
                Console.WriteLine("\"-menu\" - выход в главное меню");
                Console.Write("Команда: ");

                string command;
            ReadCommand: command = Console.ReadLine().Trim();

                switch (command)
                {
                    case "-show":
                        ShowAllProjects();
                        break;
                    case "-add":
                        AddProject();
                        break;
                    case "-fix":
                        if (projects.Count == 0)
                        {
                            Console.Write("Невозможно изменить проект, так как не существует ни одного проекта. Введите другую команду: ");
                            goto ReadCommand;
                        }
                        FixProject();
                        break;
                    case "-remove":
                        RemoveProject();
                        break;
                    case "-menu":
                        Console.Clear();
                        return;

                    default:
                        Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                        goto ReadCommand;
                }
            }
        }
       
        /// <summary>
        /// Методы работы с проектами
        /// </summary>

        private static void ShowAllProjects()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Общее число проектов: {projects.Count}. Список проектов:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            int count = 1;
            foreach (Project project in projects)
                Console.WriteLine(count++ + ") " + project);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Для продолжения нажмите любую кнопку");
            Console.ReadKey();
        }

        private static void AddProject()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Введите \"-add <Название_проекта>\", чтобы добавить новый проект с указанным названием или \"-close\", чтобы вернуться назад: ");
        ReadCommand:
            string command = Console.ReadLine().Trim();
            string[] parts = command.Split(new char[] { ' ' });
            if (parts.Length == 0 || parts[0] == null)
            {
                Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                goto ReadCommand;
            }
            switch (parts[0])
            {
                case "-close":
                    return;
                case "-add":
                    if (parts.Length < 2)
                    {
                        Console.Write("Необходимо ввести название проекта. Пожалуйста, повторите ввод: ");
                        goto ReadCommand;
                    }

                    string name = "";
                    for (int i = 1; i < parts.Length; i++)
                        name += parts[i] + " ";

                    foreach (Project project in projects)
                        if (project.Name == name)
                        {
                            Console.Write($"Проект с названием {name} уже существует. Пожалуйста, повторите ввод: ");
                            goto ReadCommand;
                        }


                    projects.Add(new Project(name.Trim()));
                    Console.WriteLine($"Проект {name} успешно добавлен в список. Редактирование его задач доступно по команде \"-tasks\" в главном меню.");
                    Console.ReadLine();
                    break;
                default:
                    Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                    goto ReadCommand;
            }
        }

        private static void FixProject()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Список проектов:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            int count = 1;
            foreach (Project project in projects)
                Console.WriteLine(count++ + ") " + project);
            count--;
            Console.ForegroundColor = ConsoleColor.Green;
            int n = 0;
            do
            {
                Console.Write($"Введите номер проекта (число в диапозоне [1; {count}]), название которого хотите изменить: ");
            } while (!int.TryParse(Console.ReadLine(), out n) || n <= 0 || n > count);

            Console.Write($"Введите новое название для проекта {projects[n - 1].Name}: ");
            string name = Console.ReadLine();
            while (name == string.Empty)
            {
                Console.Write("Введите непустую строку: ");
                name = Console.ReadLine();
            }
            projects[n - 1].Name = name;
            Console.WriteLine($"Название проекта успешно изменено на {name}.");
            Console.ReadKey();
        }

        private static void RemoveProject()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
        ReadCommand: Console.Write("Введите \"-remove <Название_проекта>\", чтобы удалить проект с указанным названием или \"-close\", чтобы вернуться назад: ");

            string command = Console.ReadLine().Trim();
            string[] parts = command.Split(new char[] { ' ' });
            if (parts.Length == 0 || parts[0] == null)
            {
                Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                goto ReadCommand;
            }
            switch (parts[0])
            {
                case "-close":
                    return;
                case "-remove":
                    if (parts.Length < 2)
                    {
                        Console.Write("Необходимо ввести название проекта. Пожалуйста, повторите ввод: ");
                        goto ReadCommand;
                    }

                    string name = "";
                    for (int i = 1; i < parts.Length; i++)
                        name += parts[i] + " ";

                    if (!FindAndRemoveProject(name.Trim()))
                    {
                        Console.WriteLine($"Проекта с указанным названием {name} не существует. Повторите попытку.");
                        goto ReadCommand;
                    }

                    Console.WriteLine($"Проект {name} успешно удален.");
                    Console.ReadLine();
                    break;
                default:
                    Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                    goto ReadCommand;
            }
        }


        #endregion


        #region Goals
        private static void WorkWithGoals()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Доступные команды для работы с задачами: ");
                Console.WriteLine("\"-show\" - список всех задач во всех проектах");
                Console.WriteLine("\"-add\" - добавить новую задачу");
                Console.WriteLine("\"-fix\" - изменить исполнителей/подзадачи выбранной задачи");
                Console.WriteLine("\"-project\" - изменить привязку задачи к проекту");
                Console.WriteLine("\"-status\" - изменить статус выбранной задачи");
                Console.WriteLine("\"-remove\" - удалить задачу");
                Console.WriteLine("\"-menu\" - выход в главное меню");
                Console.Write("Команда: ");

                string command;
            ReadCommand: command = Console.ReadLine().Trim();

                switch (command)
                {
                    case "-show":
                        if (goals.Count == 0)
                        {
                            Console.Write("Пока что не существует ни одной задачи. Введите другую команду: ");
                            goto ReadCommand;
                        }
                        ShoWAllGoals();
                        break;
                    case "-add":
                        AddGoal();
                        break;
                    case "-fix":
                        if (goals.Count == 0)
                        {
                            Console.Write("Невозможно изменить исполнителей, так как не существует ни одной задачи. Введите другую команду: ");
                            goto ReadCommand;
                        }
                        FixGoal();
                        break;
                    case "-project":
                        if (goals.Count == 0)
                        {
                            Console.Write("Невозможно изменить привязку, так как не существует ни одной задачи. Введите другую команду: ");
                            goto ReadCommand;
                        }
                        FixGoalProject();
                        break;
                    case "-status":
                        if (goals.Count == 0)
                        {
                            Console.Write("Невозможно изменить статус, так как не существует ни одной задачи. Введите другую команду: ");
                            goto ReadCommand;
                        }
                        FixStatus();
                        break;
                    case "-remove":
                        if (goals.Count == 0)
                        {
                            Console.Write("Невозможно удалить задачу, так как не существует ни одной задачи. Введите другую команду: ");
                            goto ReadCommand;
                        }
                        RemoveGoal();
                        break;
                    case "-menu":
                        Console.Clear();
                        return;

                    default:
                        Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                        goto ReadCommand;
                }
            }
        }

        /// <summary>
        /// Методы работы с задачами
        /// </summary>


        private static void ShoWAllGoals()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Введите соответствующий фильтр для отображения задач.");
            Console.WriteLine("\"-all\" - Все задачи");
            Console.WriteLine("\"-1\" - Открытая задача");
            Console.WriteLine("\"-2\" - Задача в работе");
            Console.WriteLine("\"-3\" - Завершенная задача");
            Console.WriteLine("\"-close\", чтобы вернуться назад");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Команда: ");

            string command;
        ReadCommand: command = Console.ReadLine().Trim();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Задачи с соответствующим фильтром: ");
            int count = 1;
            switch (command)
            {
                case "-all":
                    foreach (var goal in goals)
                        Console.WriteLine(count++ + ") " + goal);
                    break;
                case "-1":
                    foreach (var goal in goals)
                        if (goal.Status == "Открытая")
                            Console.WriteLine(count++ + ") " + goal);
                    break;
                case "-2":
                    foreach (var goal in goals)
                        if (goal.Status == "В работе")
                            Console.WriteLine(count++ + ") " + goal);
                    break;
                case "-3":
                    foreach (var goal in goals)
                        if (goal.Status == "Завершенная")
                            Console.WriteLine(count++ + ") " + goal);
                    break;
                case "-close":
                    return;


                default:
                    Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                    goto ReadCommand;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Для продолжения нажмите любую кнопку");
            Console.ReadKey();
        }

        private static void AddGoal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Введите название будущей задачи: ");

            string name = Console.ReadLine().Trim();

            while (name == string.Empty)
            {
                Console.Write("Введите непустую строку: ");
                name = Console.ReadLine();
            }

            while (FindGoal(name))
            {
                Console.WriteLine("Задача с таким названием уже существует! Поробуйте другое: ");
                name = Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
        ReadType: Console.WriteLine("Выберите тип задачи, введя соответствующую цифру: ");
            Console.WriteLine("\"1\" - Тема (Epic)");
            Console.WriteLine("\"2\" - История (Story)");
            Console.WriteLine("\"3\" - Задание (Task)");
            Console.WriteLine("\"4\" - Ошибка (Bug)");
            Console.Write("Цифра: ");

            Goal goal;
            string command = Console.ReadLine().Trim();
            switch (command)
            {
                case "1":
                    goal = new Epic(name, DateTime.Now);
                    break;
                case "2":
                    goal = new Story(name, DateTime.Now);
                    break;
                case "3":
                    goal = new Task(name, DateTime.Now);
                    break;
                case "4":
                    goal = new Bug(name, DateTime.Now);
                    break;

                default:
                    Console.Write("Неизвестный тип. Пожалуйста, повторите ввод: ");
                    goto ReadType;
            }

            if (goal is Epic)
            {
                foreach (var g in goals)
                {
                    if (!(g is Epic))
                    {
                        WorkWithEpicTasks(goal as Epic);
                        return;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("В списке задач сейчас находятся только Темы (Epic), поэтому добавить подзадачи нельзя. ");
            }
            else
                WorkWitnNotEpicTascs(goal);

            AddGoalIntoProject(goal);
        }

        private static void FixGoal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Введите номер задачи, который хотите изменить или \"-close\", чтобы вернуться назад: ");
            int count = 1;

            foreach (var g in goals)
                Console.WriteLine(count++ + ") " + g);
            count--;
            Console.Write("Номер задачи: ");
            string read = Console.ReadLine();
            if (read == "-close")
                return;
            int n;
            if (!int.TryParse(read, out n) || n < 1 || n > count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Некорректный номер задачи. ");
                Console.ReadKey();
                return;
            }

            Goal goal = goals[n - 1];
            if (goal is Epic)
            {
                Console.WriteLine("Задача является Темой (Epic), поэтому в ней можно переназначить подзадачи. ");
                Console.ReadKey();
                WorkWithEpicTasks(goal as Epic);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Задача НЕ является Темой (Epic), поэтому в ней можно переназначить/добавить исполнителей. ");
                Console.ReadKey();
                WorkWitnNotEpicTascs(goal);
                Console.ReadKey();
            }
        }
       
        private static void FixGoalProject()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Введите номер задачи, принадлежность к проекту которой хотите изменить или \"-close\", чтобы вернуться назад: ");
            int count = 1;

            foreach (var g in goals)
                Console.WriteLine(count++ + ") " + g);
            count--;
            Console.Write("Номер задачи (учтите, что она автоматически удалится из всех проектов, где была задействована раннее): ");
            int n; 
            string read = Console.ReadLine();
            if (read == "-close")
                return;
            if (!int.TryParse(read, out n) || n < 1 || n > count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Некорректный номер задачи. ");
                Console.ReadKey();
                return;
            }

            Goal goal = goals[n - 1];

            foreach (Project project in projects)
                foreach (var g in project.Goals)
                    if (g.Name == goal.Name)
                    {
                        project.Goals.Remove(g);
                        break;
                    }
            Console.WriteLine("Задача успешно удалена из всех проектов, где была задействована. Теперь вы можете заново выбрать проекты, к которым она будет привязана.");
            Console.ReadKey();
            AddGoalIntoProject(goal);
        }
     
        private static void FixStatus()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Введите номер задачи, который хотите изменить или \"-close\", чтобы вернуться назад: ");
            int count = 1;

            foreach (var g in goals)
                Console.WriteLine(count++ + ") " + g);
            count--;
            Console.Write("Номер задачи: ");
            int n;
            string read = Console.ReadLine();
            if (read == "-close")
                return;
            if (!int.TryParse(read, out n) || n < 1 || n > count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Некорректный номер задачи. ");
                Console.ReadKey();
                return;
            }

            Goal goal = goals[n - 1];
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Введите одну из 3 цифр для изменения статуса задачи.");
            Console.WriteLine("\"1\" - Открытая задача");
            Console.WriteLine("\"2\" - Задача в работе");
            Console.WriteLine("\"3\" - Завершенная задача");
            Console.WriteLine("Введите \"-close\", чтобы вернуться назад");
            Console.Write("Команда: ");

            string command;
        ReadCommand: command = Console.ReadLine().Trim();

            switch (command)
            {
                case "1":
                    goal.Status = "Открытая";
                    Console.WriteLine("Статус задачи успешно изменен на \"Открытая\". ");
                    Console.ReadKey();
                    break;
                case "2":
                    goal.Status = "В работе";
                    Console.WriteLine("Статус задачи успешно изменен на \"В работе\". ");
                    Console.ReadKey();
                    break;
                case "3":
                    goal.Status = "Завершенная";
                    Console.WriteLine("Статус задачи успешно изменен на \"Завершенная\". ");
                    Console.ReadKey();
                    break;
                case "-close":
                    return;


                default:
                    Console.Write("Неизвестная команда. Пожалуйста, повторите ввод: ");
                    goto ReadCommand;
            }
        }
        
        private static void RemoveGoal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Введите номер задачи, которую хотите удалить или \"-close\", чтобы вернуться назад: ");
            int count = 1;

            foreach (var g in goals)
                Console.WriteLine(count++ + ") " + g);
            count--;
            Console.Write("Номер задачи (учтите, задача будет удалена из всех проектов, в которых она задействована): ");
            int n;
            if (!int.TryParse(Console.ReadLine(), out n) || n < 1 || n > count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Некорректный номер задачи. ");
                Console.ReadKey();
                return;
            }

            Goal goal = goals[n - 1];
            foreach (Project project in projects)
                foreach (var g in project.Goals)
                    if (g.Name == goal.Name)
                    {
                        project.Goals.Remove(g);
                        break;
                    }

            goals.RemoveAt(n - 1);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Задача {goal.Name} успешно удалена из списка задач. Для продолжения нажмите любую клавишу. ");
            Console.ReadKey();
        }

        #endregion


        #region Support

        /// <summary>
        /// Вспомогательные методы.
        /// </summary>

        private static bool FindAndRemoveUser(string name)
        {
            foreach (User user in users)
                if (user.Name == name)
                {
                    users.Remove(user);
                    return true;
                }

            return false;
        }
        private static bool FindAndRemoveProject(string name)
        {
            foreach (Project project in projects)
                if (project.Name == name)
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

                    return true;
                }

            return false;
        }
        private static bool FindGoal(string name)
        {
            foreach (Goal goal in goals)
                if (goal.Name == name)
                    return true;

            return false;
        }
        private static bool FindIsInGoal(string name, User user)
        {
            foreach (var goal in user.Tasks)
                if (goal.Name == name)
                    return true;

            return false;
        }

        private static void WorkWithEpicTasks(Epic epic)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (goals.Count == 0)
            {
                Console.WriteLine("Задача является Темой (Epic), но других задач пока нет, поэтому подзадачи добавить нельзя. ");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Введите строго через пробел номера задач, которые хотите добавить как подзадачи в данной теме или пустую строку, чтобы не назначать подзадачи" +
                " (учтите, что, если список подзадач непустой, то он очистится и заполнится заново задачами с введенными номерами):");
            epic.Tasks.Clear();

            int count = 1;
            List<Goal> curGoals = new List<Goal>();
            foreach (Goal g in goals)
                if (!(g is Epic) && !(g is Bug))
                {
                    Console.WriteLine(count++ + ") " + g.Name +  (g is Task ? " (Task)" : " (Story)"));
                    curGoals.Add(g);
                    curGoals.Sort();
                }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Номера задач строго через пробел или пустая строка: ");
            string[] nums = Console.ReadLine().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var num in nums)
            {
                int n;
                if (!int.TryParse(num, out n) || n <= 0 || n > curGoals.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Задача №{num}: не соответствующий номер.");
                    continue;
                }

                foreach (var task in epic.Tasks)
                    if (task.Name == curGoals[n - 1].Name)
                    {
                        epic.Tasks.Remove(curGoals[n - 1]);
                        break;
                    }

                epic.Tasks.Add(curGoals[n - 1]);
                epic.Tasks.Sort();
                curGoals[n - 1].isInProject = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Задача №{num} ({curGoals[n - 1].Name}): успешно добавлена как подзадача.");
            }
        }

        private static void WorkWitnNotEpicTascs(Goal goal)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (users.Count == 0)
            {
                Console.WriteLine("В спсике нет ни одного пользователя. Добавить задаче исполнителей невозможно.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Введите строго через пробел номера пользователей, которых хотите назначить исполнителями/добавить к списку исполнителей данной задачи" +
                " или пустую строку, если не хотите назначать/добавлять исполнителей. Учтите, что для задач типа Task и Bug может быть только один исполнитель, поэтому " +
                "назначен будет пользователем с первым введенным номером: ");


            int count = 1;
            foreach (User user in users)
                Console.WriteLine(count++ + ") " + user.Name);
            count--;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Номера пользователей строго через пробел или пустая строка: ");
            string[] nums = Console.ReadLine().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var num in nums)
            {
                int n;
                if (!int.TryParse(num, out n) || n <= 0 || n > count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Пользователь №{num}: не соответствующий номер.");
                    continue;
                }

                if (goal is Task)
                {
                    Task task = goal as Task;
                    task.User.Tasks.Remove(task);
                    task.User = users[n - 1];
                    users[n - 1].Tasks.Add(goal);
                    users[n - 1].Tasks.Sort();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Первый введенный пользователь №{num} ({users[n - 1].Name}): успешно стал исполнителем Задания {goal.Name}.");
                    break;
                }
                if (goal is Bug)
                {
                    Bug bug = goal as Bug;
                    bug.User.Tasks.Remove(bug);
                    bug.User = users[n - 1];
                    users[n - 1].Tasks.Add(goal);
                    users[n - 1].Tasks.Sort();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Первый введенный пользователь №{num} ({users[n - 1].Name}): успешно стал исполнителем Ошибки {goal.Name}.");
                    break;
                }

                IAssignable assGoal = (IAssignable)goal;
                if (assGoal != null)
                {
                    if (assGoal.MaxCount <= assGoal.Users.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Пользователь №{num}: количество исполнителей Истории превышает допустимое значение({assGoal.MaxCount}).");
                        continue;
                    }

                }

                if (!FindIsInGoal(goal.Name, users[n - 1]))
                {
                    users[n - 1].Tasks.Add(goal);
                    users[n - 1].Tasks.Sort();
                    assGoal.Users.Add(users[n - 1]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Пользователь №{num} ({users[n - 1].Name}): успешно стал исполнителем.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Пользователь №{num} ({users[n - 1].Name}): уже является исполнителем.");
                }

            }
        }

        private static void AddGoalIntoProject(Goal goal)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (projects.Count == 0)
            {
                Console.WriteLine("На данный момент нет ни одного проекта, поэтому эта задача просто добавится в список задач.");
                foreach (Goal g in goals)
                    if (g.Name == goal.Name)
                    {
                        goals.Remove(g);
                        break;
                    }

                goals.Add(goal);
                goals.Sort();
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Введите номер проекта, в который хотите добавить данную задачу или любую другую строку, если не хотите ее добавлять: ");

            int count = 1;
            foreach (Project project in projects)
                Console.WriteLine(count++ + ") " + project.Name);
            count--;
            Console.Write($"Номера проекта из диапозона [1; {count}], в который вы хотите добавить задачу или любая другая строка: ");
            int n;
            if (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Введен некорректный номер, поэтому задача будет добавлена к списку задач, но не будет привязана ни к одному проекту.");
                foreach (Goal g in goals)
                    if (g.Name == goal.Name)
                    {
                        goals.Remove(g);
                        break;
                    }
                goals.Add(goal);
                goals.Sort();
                Console.ReadKey();
                return;
            }
            foreach (Goal g in goals)
                if (g.Name == goal.Name)
                {
                    goals.Remove(g);
                    break;
                }
            goals.Add(goal);
            goals.Sort();

            if (projects[n - 1].maxCount <= projects[n - 1].Goals.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Количество задач в проекте {projects[n - 1].Name} достигло максимального значения {projects[n - 1].maxCount}. Задача будет добавлена к списку задач, но не будет привязана ни к одному проекту.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Для продолжения нажмите любую кнопку");
                Console.ReadKey();
                return; 
            }

            goal.isInProject = true;
            projects[n - 1].Goals.Add(goal);
            projects[n - 1].Goals.Sort();

            Console.WriteLine($"Задача добавлена к общему списку задач, а также привязана к проекту {projects[n - 1].Name}.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Для продолжения нажмите любую кнопку");
            Console.ReadKey();
        }



        /// <summary>
        /// Сохрание при закрытии.
        /// </summary>


        internal delegate void SignalHandler(ConsoleSignal consoleSignal);
        internal enum ConsoleSignal
        {
            CtrlC = 0,
            CtrlBreak = 1,
            Close = 2,
            LogOff = 5,
            Shutdown = 6
        }
        internal static class ConsoleHelper
        {
            [DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
            public static extern bool SetSignalHandler(SignalHandler handler, bool add);
        }

        private static void HandleConsoleSignal(ConsoleSignal consoleSignal)
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
                File.WriteAllLines("projects.txt", lines);

                lines = new List<string>();
                foreach (User user in users)
                {
                    string line = user.Name + "~";
                    foreach (var goal in user.Tasks)
                        line += goal.Name + ";";
                    lines.Add(line);
                }
                File.WriteAllLines("users.txt", lines);

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
                File.WriteAllLines("goals.txt", lines);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Возникла ошибка! " + ex.Message);
            }
        }
       
        /// <summary>
        /// Загрузка данных.
        /// </summary>

        public static void LoadData()
        {
            try
            {
                string[] lines = File.ReadAllLines("goals.txt");
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


                lines = File.ReadAllLines("users.txt");
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

                lines = File.ReadAllLines("projects.txt");
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Внимание! При загрузке данных проекта произошла ошибка, все списки и данные сейчас пустые. Сообщение ошибки: " + ex.Message);
                users = new List<User>();
                projects = new List<Project>();
                goals = new List<Goal>();
                Console.ReadKey();
            }
        }
        #endregion
    }
}
