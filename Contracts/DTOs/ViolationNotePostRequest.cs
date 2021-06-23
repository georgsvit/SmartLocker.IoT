using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Contracts.DTOs
{
    public record ViolationNotePostRequest(
        Guid UserId,
        Guid LockerId,
        Guid ToolId,
        DateTime Date
        );
}
