using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Models
{
    public class Tool
    {
        public Guid Id { get; set; }
        public int AccessLevel { get; set; }

        public Tool(Guid id, int accessLevel)
        {
            Id = id;
            AccessLevel = accessLevel;
        }
    }
}
