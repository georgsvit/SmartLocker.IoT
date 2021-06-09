using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.CLI.Commands
{
    [Verb("take", HelpText = "Take tool")]
    class TakeToolCommand : ICommand
    {
        [Option('i', "index", Required = true, HelpText = "Tool Index")]
        public int ToolIdx { get; set; }

        public void Execute()
        {
            Console.WriteLine("Tool was taken");
            this.ToolIdx--;
        }

        CommandType ICommand.GetType() => CommandType.TakeTool;
    }
}
