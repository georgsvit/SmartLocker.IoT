using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.CLI.Commands
{
    [Verb("return", HelpText = "Return tool")]
    class ReturnToolCommand : ICommand
    {
        [Option('i', "id", Required = true, HelpText = "Tool Id")]
        public Guid ToolId { get; set; }

        [Option('a', "access", Required = true, HelpText = "Tool access level")]
        public int AccessLevel { get; set; }

        public void Execute() { }

        CommandType ICommand.GetType() => CommandType.ReturnTool;
    }
}
