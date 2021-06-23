using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public int AccessLevel { get; set; }

        public User()
        {
            Id = Guid.Empty;
            AccessLevel = -1;
        }
    }
}
