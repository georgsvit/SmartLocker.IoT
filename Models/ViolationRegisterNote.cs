using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Models
{
    public class ViolationRegisterNote
    {
        public Guid UserId { get; set; }
        public Guid ToolId { get; set; }
        public DateTime Date { get; set; }

        public ViolationRegisterNote(Guid userId, Guid toolId, DateTime date)
        {
            UserId = userId;
            ToolId = toolId;
            Date = date;
        }
    }
}
