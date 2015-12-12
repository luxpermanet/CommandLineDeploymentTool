using System;
using System.Diagnostics;
using System.IO;

namespace CommandLineDeploymentTool.OperationTypes
{
    class OperationsEXE : OperationsBase
    {
        public OperationsEXE(Arguments args)
            : base(args)
        {
            // do nothing
        }

        public override void ValidateArguments()
        {
            base.ValidateArguments();

            if (!this.args.NoStop)
            {
                Process[] processes = this.GetApp();
                if (processes == null || processes.Length == 0)
                {
                    throw new ArgumentException(this.args.AppName + " could not be found", "appName");
                }
            }
        }

        public override void Backup()
        {
            base.Backup();
        }

        public override void Restore()
        {
            base.Restore();
        }

        public override void Stop()
        {
            Process[] processes = this.GetApp();
            foreach (Process process in processes)
            {
                process.Kill();
                process.WaitForExit();
                Console.WriteLine(process.ProcessName + " has exited successfully");
            }
        }

        private Process[] GetApp()
        {
            string appName = this.args.AppName.Substring(0, this.args.AppName.LastIndexOf('.'));
            Process[] processes = Process.GetProcessesByName(appName);
            return processes;
        }

        public override void Deploy()
        {
            base.Deploy();
        }

        public override void Start()
        {
            string appPath = Path.Combine(this.args.AppFolder, this.args.AppName);
            Process.Start(appPath);
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Rollback()
        {
            base.Rollback();
        }
    }
}
