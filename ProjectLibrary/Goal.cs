using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectLibrary
{
    public class Goal : IComparable
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public bool isInProject { get; set; }

        public Goal(string name, DateTime dateTime, string status)
        {
            Name = name;
            Date = dateTime;
            Status = status;
            isInProject = false;
        }
        public Goal(string name, DateTime dateTime) : this(name, dateTime, "Открытая")
        {
        }

        public override string ToString()
        {
            return $"Проект {Name}.\nДата создания: {Date}.\nСтатус: {Status}.\nЗадействована в проекте: {(isInProject ? "Да" : "Нет")}.\n";
        }

        public int CompareTo(object obj)
        {
            List<string> names = new List<string>() { "Epic", "Story", "Task", "Bug" };
            int index = 0;
            if (this is Epic)
                index = 1;
            if (this is Story)
                index = 2;
            if (this is Task)
                index = 3;
            if (this is Bug)
                index = 4;
            int objIndex = 0;

            if (obj is Epic)
                objIndex = 1;
            if (obj is Story)
                objIndex = 2;
            if (obj is Task)
                objIndex = 3;
            if (obj is Bug)
                objIndex = 4;

            return index.CompareTo(objIndex);
        }
    }
}
