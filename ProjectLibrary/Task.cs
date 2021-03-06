using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectLibrary
{
    public class Task : Goal
    {
        public User User { get; set; }
        public bool isInEpic { get; set; }

        public Task(string name, DateTime dateTime, string status) : base(name, dateTime, status)
        {
            User = new User("Нет исполнителя");
        }
        public Task(string name, DateTime dateTime) : base(name, dateTime)
        {
            User = new User("Нет исполнителя");
        }
        public override string ToString()
        {
            return $"Задание {Name} (Task).\nДата создания: {Date}.\nСтатус: {Status}.\n" +
                $"Задействована в проекте: {(isInProject ? "Да" : "Нет")}.\nИсполнитель: {User.Name}.\n"; 
        }
    }
}
