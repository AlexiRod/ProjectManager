using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectLibrary
{
    public interface IAssignable
    {
        public List<User> Users { get; set; }
        public int MaxCount { get; set; }
    }
}
