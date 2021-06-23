using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Contracts.DTOs
{
    public record ReturnToolRequest(
        [Required] Guid userId,
        [Required] Guid toolId,
        [Required] Guid lockerId,
        [Required] DateTime date
    );
}
