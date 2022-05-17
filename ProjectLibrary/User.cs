using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectLibrary
{
    public class User
    {
        public string Name { get; set; }
        public List<Goal> Tasks { get; set; }

        public override string ToString()
        {
            string ret = $"Пользователь \"{Name}\". Список задач, в которых он задействован: \n";
            if (Tasks.Count == 0)
            {
                ret += "Пользователь не задействован ни в одной задаче.";
                return ret;
            }
            foreach (var task in Tasks)
                ret += task + "\n";
            return ret;
        }

        public User(string name)
        {
            Name = name;
            Tasks = new List<Goal>();
        }

    }
}
