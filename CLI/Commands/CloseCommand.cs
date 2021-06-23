using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.CLI.Commands
{
    [Verb("close", HelpText = "Close Locker")]
    class CloseCommand : ICommand
    {
        public void Execute() { }

        CommandType ICommand.GetType() => CommandType.Close;
    }
}
