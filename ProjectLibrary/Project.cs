using System;
using System.Collections.Generic;

namespace ProjectLibrary
{
    public class Project
    {
        public int maxCount = 100;
        public string Name { get; set;   }
        public List<Goal> Goals { get; set; }

        public Project(string name)
        {
            Name = name;
            Goals = new List<Goal>();
        }
        public override string ToString()
        {
            string ret = $"Проект \"{Name}\". Список задач в этом проекте:\n";
            if (Goals.Count == 0)
            {
                ret += "В проекте нет ни одной задачи.";
                return ret;
            }
            foreach (var goal in Goals)
                ret += goal + "\n";
            return ret;
        }


    }
}
