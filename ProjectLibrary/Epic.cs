using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectLibrary
{
    public class Epic : Goal
    {
        public List<Goal> Tasks { get; set; }
        public Epic(string name, DateTime dateTime, string status) : base(name, dateTime, status)
        {
            Tasks = new List<Goal>();
        }
        public Epic(string name, DateTime dateTime) : base(name, dateTime)
        {
            Tasks = new List<Goal>();
        }

        public override string ToString()
        {
            return $"Тема {Name} (Epic).\nДата создания: {Date}.\nСтатус: {Status}.\nЗадействована в проекте: {(isInProject ? "Да" : "Нет")}.\n";
        }
    }
}
