using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.CLI.Commands
{
    [Verb("open", HelpText = "Open Locker")]
    public class OpenCommand : ICommand
    {
        [Option('i', "id", Required = true, HelpText = "User Id")]
        public Guid UserId { get; set; }
        
        [Option('a', "access", Required = true, HelpText = "User access level")]
        public int AccessLevel { get; set; }

        public void Execute() { }

        CommandType ICommand.GetType() => CommandType.Open;
    }
}
