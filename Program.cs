using Microsoft.Extensions.Configuration;
using SmartLocker.IoT.CLI;
using SmartLocker.IoT.CLI.Commands;
using SmartLocker.IoT.Models;
using SmartLocker.IoT.Services;
using SmartLocker.IoT.Storage;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SmartLocker.IoT
{
    class Program
    {
        static CliProcessor cli;
        static User user;
        static IStorage storage;
        static IConfiguration configuration;
        static HttpClientService httpClient;
        static Guid lockerId;
        static bool isBlocked;

        static async Task Main(string[] args)
        {
            InitializeConfiguration();

            httpClient = new(int.Parse(configuration["Timeout"]));
            lockerId = Guid.Parse(configuration["LockerId"]);
            storage = new IotStorage();
            user = new();
            cli = new();

            isBlocked = await httpClient.GetConfig(lockerId);

            while (!isBlocked)
            {
                if (user.Id != Guid.Empty && user.AccessLevel != -1) cli.PrintTools(storage.GetAllTools());

                ICommand command = cli.ProcessInput();
                if (command is not null) await GeneralCommandHandler(command);
            }

            cli.Print("Locker is blocked");
        }

        private static void InitializeConfiguration()
        {
            const string jsonConfigurationFileName = "appsettings.json";
            configuration = new ConfigurationBuilder()
                .AddJsonFile(jsonConfigurationFileName, optional: false, reloadOnChange: true)
                .Build();
        }

        static async Task GeneralCommandHandler(ICommand command)
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
                    await TakeToolCommandHandler(command);
                    break;
                case CommandType.ReturnTool:
                    await ReturnToolCommandHandler(command);
                    break;
            }
        }

        static void OpenCommandHandler(ICommand command)
        {
            if (user.Id == Guid.Empty && user.AccessLevel == -1)
            {
                OpenCommand cmd = (OpenCommand)command;
                user.Id = cmd.UserId;
                user.AccessLevel = cmd.AccessLevel;
                cli.Print("Locker was opened");
            } else
            {
                cli.Print("Locker already opened");
            }
        }

        static void CloseCommandHandler(ICommand command)
        {
            if (user.Id != Guid.Empty && user.AccessLevel != -1)
            {
                user.AccessLevel = -1;
                user.Id = Guid.Empty;
                cli.Print("Locker was closed");
            } else
            {
                cli.Print("Locker already closed");
            }
        }

        static async Task TakeToolCommandHandler(ICommand command)
        {
            TakeToolCommand cmd = (TakeToolCommand)command;

            var tool = storage.GetTool(cmd.ToolIdx);

            if (tool.AccessLevel > user.AccessLevel)
            {
                try
                {
                    if (!await httpClient.SendViolationRegisterNote(new(user.Id, lockerId, tool.Id, DateTime.Now)))
                    {
                        storage.AddViolationNote(user.Id, tool.Id);
                    }
                }
                catch (Exception)
                {
                    storage.AddViolationNote(user.Id, tool.Id);
                }
                cli.PrintViolationMsg();
            }

            try
            {
                if (!await httpClient.TakeTool(new(user.Id, tool.Id, DateTime.Now)))
                {
                    storage.AddAccountingNote(new(DateTime.Now, null, user.Id, tool.Id));
                }
            }
            catch (Exception)
            {
                storage.AddAccountingNote(new(DateTime.Now, null, user.Id, tool.Id));
            }

            storage.TakeTool(tool.Id);
            cli.Print("Tool was taken");
        }

        static async Task ReturnToolCommandHandler(ICommand command)
        {
            ReturnToolCommand cmd = (ReturnToolCommand)command;            

            try
            {
                if (!await httpClient.ReturnTool(new(user.Id, cmd.ToolId, lockerId, DateTime.Now)))
                {
                    storage.AddAccountingNote(new(null, DateTime.Now, user.Id, cmd.ToolId));
                }
            }
            catch (Exception)
            {
                storage.AddAccountingNote(new(null, DateTime.Now, user.Id, cmd.ToolId));
            }

            storage.ReturnTool(new Tool(cmd.ToolId, cmd.AccessLevel));

            cli.Print("Tool was returned");
        }
    }
}
