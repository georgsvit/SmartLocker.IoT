using SmartLocker.IoT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Storage
{
    public interface IStorage
    {
        // Tool data
        public IEnumerable<Tool> GetAllTools();
        public Tool GetTool(int toolIdx);
        public void TakeTool(Guid toolId);
        public void ReturnTool(Tool tool);

        // Registers data
        public IEnumerable<ViolationRegisterNote> GetAllViolationNotes();
        public IEnumerable<AccountingRegisterNote> GetAllAccountingNotes();
        public void AddViolationNote(Guid userId, Guid toolId);
        public void AddAccountingNote(AccountingRegisterNote note);        
    }
}
