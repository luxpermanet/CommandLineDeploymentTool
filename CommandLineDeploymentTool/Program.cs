using System;

namespace CommandLineDeploymentTool
{
    class Program
    {
        /*
         * IIS Sample
         * CommandLineDeploymentTool.exe /deployType:IIS /backupFolder:D:\Backups /appName:MyApp /appFolder:D:\MyAppFolder /deployFolder:D:\DeployFolder\MyApp
         * COM Sample
         * CommandLineDeploymentTool.exe /deployType:COM /backupFolder:D:\Backups /appName:MyApp /appFolder:D:\MyAppFolder /deployFolder:D:\DeployFolder\MyApp /categoryName:MyAppCategory
         * WINDOWSSERVICE Sample
         * CommandLineDeploymentTool.exe /deploytype:WINDOWSSERVICE /backupFolder:D:\Backups /appName:MyApp /appFolder:D:\MyAppFolder /deployFolder:D:\DeployFolder\MyApp
         */
        static int Main(string[] args)
        {
            Console.WriteLine();
            if (args.Length == 0 || args[0] == "/help" || args[0] == "/h" || args[0] == "/?")
            {
                Console.WriteLine(Arguments.HelpString);
                return 0;
            }

            Arguments arguments = new Arguments(args, true);
            Console.WriteLine("Executing '" + arguments.ToString() + "'");
            IOperations ops = OperationsFactory.Create(arguments);

            try
            {
                ops.ValidateArguments();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            if (arguments.Restore)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Rolling back due to restore request...");
                    ops.Rollback();
                    Console.WriteLine("Rolled back due to restore request...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    throw;
                }
            }
            else
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Executing deployment...");
                    ops.Execute();
                    Console.WriteLine("Executed deployment...");
                    Console.WriteLine();
                    Console.WriteLine("Rollback script:");
                    Console.WriteLine(arguments.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine();
                    Console.WriteLine("Rollback script:");
                    Console.WriteLine(arguments.ToString());
                    Console.WriteLine();
                    Console.WriteLine("Rolling back due to an error...");
                    ops.Rollback();
                    Console.WriteLine("Rolled back due to an error...");
                    throw;
                }
            }

            return 0;
        }
    }
}
