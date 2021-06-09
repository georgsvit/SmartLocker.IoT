using SmartLocker.IoT.CLI;
using SmartLocker.IoT.CLI.Commands;
using SmartLocker.IoT.Models;
using SmartLocker.IoT.Storage;
using System;

namespace SmartLocker.IoT
{
    class Program
    {
        static CliProcessor cli = new();
        static User user = new();
        static IStorage storage = new IotStorage();

        static void Main(string[] args)
        {
            while (true)
            {
                if (user.Id != Guid.Empty && user.AccessLevel != -1) cli.PrintTools(storage.GetAllTools());

                ICommand command = cli.ProcessInput();
                if (command is not null) GeneralCommandHandler(command);
            }
        }

        static void GeneralCommandHandler(ICommand command)
        {
            switch (command.GetType())
            {
                case CommandType.Open:
                    OpenCommandHandler(command);
                    break;
                case CommandType.Close:
                    CloseCommandHandler(command);
                    break;
                case CommandType.TakeTool:
                    TakeToolCommandHandler(command);
                    break;
                case CommandType.ReturnTool:
                    ReturnToolCommandHandler(command);
                    break;
            }
        }

        static void OpenCommandHandler(ICommand command)
        {
            OpenCommand cmd = (OpenCommand)command;
            user.Id = cmd.UserId;
            user.AccessLevel = cmd.AccessLevel;
        }

        static void CloseCommandHandler(ICommand command)
        {
            user.AccessLevel = -1;
            user.Id = Guid.Empty;
        }

        static void TakeToolCommandHandler(ICommand command)
        {
            TakeToolCommand cmd = (TakeToolCommand)command;

            var tool = storage.GetTool(cmd.ToolIdx);

            // Add inet rule
            if (tool.AccessLevel > user.AccessLevel)
            {
                storage.AddViolationNote(user.Id, tool.Id);
                cli.PrintViolationMsg();
            }

            storage.TakeTool(tool.Id);
            storage.AddAccountingNote(new (DateTime.Now, null, user.Id, tool.Id));
        }

        static void ReturnToolCommandHandler(ICommand command)
        {
            ReturnToolCommand cmd = (ReturnToolCommand)command;
            storage.ReturnTool(new Tool(cmd.ToolId, cmd.AccessLevel));

            // Add inet rule
            storage.AddAccountingNote(new (null, DateTime.Now, user.Id, cmd.ToolId));
        }
    }
}
