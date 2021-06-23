using CommandLine;
using SmartLocker.IoT.CLI.Commands;
using SmartLocker.IoT.Extensions;
using SmartLocker.IoT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker.IoT.CLI
{
    public class CliProcessor
    {
        private Parser Parser { get; set; }

        public CliProcessor()
        {
            this.Parser = Parser.Default;
        }

        public ICommand ProcessInput()
        {
            string[] args = StringExtension.SplitCommandLine(Console.ReadLine()).ToArray();

            ICommand command = null;

            var parserResult = this.Parser.ParseArguments<OpenCommand, CloseCommand, TakeToolCommand, ReturnToolCommand>(args)
                .WithParsed<ICommand>(cmd => { cmd.Execute(); command = cmd; });


            return parserResult.Tag switch
            {
                ParserResultType.Parsed => command,
                _ => null,
            };
        }

        public void PrintTools(IEnumerable<Tool> tools)
        {
            if (tools.Count() == 0)
            {
                Console.WriteLine("Locker is empty");
            } else
            {
                Console.WriteLine("Tool list:");

                int i = 1;
                foreach (var tool in tools)
                {
                    Console.WriteLine($"#{i} ID: {tool.Id} Access Level: {tool.AccessLevel}");
                    i++;
                }
            }
        }

        public void PrintViolationMsg()
        {
            Console.WriteLine("Violation was registered");
        }

        public void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
