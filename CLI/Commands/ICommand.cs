using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.CLI.Commands
{
    public interface ICommand
    {
        public CommandType GetType();
        void Execute();
    }
}
