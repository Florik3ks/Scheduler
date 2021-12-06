using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Assignment
    {
        public string Name { get; set; }
        public string ID { get; }
        public Assignment(string name)
        {
            Name = name;
            ID = Guid.NewGuid().ToString();
        }
        public Assignment(string name, string id)
        {
            Name = name;
            ID = id;
        }
    }
}
