using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Models
{
    public class AccountingRegisterNote
    {
        public DateTime? TakeDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Guid UserId { get; set; }
        public Guid ToolId { get; set; }

        public AccountingRegisterNote(DateTime? takeDate, DateTime? returnDate, Guid userId, Guid toolId)
        {
            TakeDate = takeDate;
            ReturnDate = returnDate;
            UserId = userId;
            ToolId = toolId;
        }
    }
}
