using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectLibrary
{
    public class Story : Goal, IAssignable
    {
        public bool isInEpic { get; set; }
        public List<User> Users { get; set; }
        public int MaxCount { get; set; }

        public Story(string name, DateTime dateTime, string status) : base(name, dateTime, status)
        {
            Users = new List<User>();
            MaxCount = 50;
        }
        public Story(string name, DateTime dateTime) : base(name, dateTime)
        {
            Users = new List<User>();
            MaxCount = 50;
        }
        public override string ToString()
        {
            string ret = $"История {Name} (Story).\nДата создания: {Date}.\nСтатус: {Status}.\n" +
                $"Задействована в проекте: {(isInProject ? "Да" : "Нет")}.\nИсполнители:\n";

            if (Users.Count == 0)
            {
                ret += "Нет исполнителей";
                return ret;
            }
            List<string> concat = new List<string>();
            foreach (User user in Users)
                concat.Add(user.Name);
            ret += string.Join("; ", concat);
            return ret;
        }
    }
}
