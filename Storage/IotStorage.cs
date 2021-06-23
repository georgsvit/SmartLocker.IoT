using Hanssens.Net;
using SmartLocker.IoT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.Storage
{
    public class IotStorage : IStorage
    {
        private LocalStorage storage;

        public IotStorage()
        {
            this.storage = new(new LocalStorageConfiguration() { AutoLoad = true, AutoSave = true });

            this.storage.Load();

            if (this.storage.Count < 3)
            {
                this.storage.Store(StorageKey.Tools.ToString(), new List<Tool>());
                this.storage.Store(StorageKey.ViolationRegister.ToString(), new List<ViolationRegisterNote>());
                this.storage.Store(StorageKey.AccountingRegister.ToString(), new List<AccountingRegisterNote>());

                this.SaveChanges();
            }
        }

        private void SaveChanges()
        {
            this.storage.Persist();
        }

        public IEnumerable<Tool> GetAllTools()
        {
            var tools = this.storage.Query<Tool>(StorageKey.Tools.ToString());
            return tools;
        }

        public Tool GetTool(int toolIdx)
        {
            var tool = this.GetAllTools().ToList()[toolIdx];

            return tool;
        }

        public void TakeTool(Guid toolId)
        {
            var tools = this.GetAllTools();
            tools = tools.Where(t => t.Id != toolId);
            storage.Store(StorageKey.Tools.ToString(), tools);

            SaveChanges();
        }

        public void ReturnTool(Tool tool)
        {
            var tools = this.GetAllTools();
            tools = tools.Append(tool);
            this.storage.Store(StorageKey.Tools.ToString(), tools);

            SaveChanges();
        }

        public IEnumerable<ViolationRegisterNote> GetAllViolationNotes()
        {
            var notes = this.storage.Query<ViolationRegisterNote>(StorageKey.ViolationRegister.ToString());
            return notes;
        }

        public IEnumerable<AccountingRegisterNote> GetAllAccountingNotes()
        {
            var notes = this.storage.Query<AccountingRegisterNote>(StorageKey.AccountingRegister.ToString());
            return notes;
        }

        public void AddViolationNote(Guid userId, Guid toolId)
        {
            var notes = this.GetAllViolationNotes();
            notes = notes.Append(new ViolationRegisterNote(userId, toolId, DateTime.Now));
            this.storage.Store(StorageKey.ViolationRegister.ToString(), notes);

            SaveChanges();
        }

        public void AddAccountingNote(AccountingRegisterNote note)
        {
            var notes = this.GetAllAccountingNotes();
            notes = notes.Append(note);
            this.storage.Store(StorageKey.AccountingRegister.ToString(), notes);

            SaveChanges();
        }
    }
}
