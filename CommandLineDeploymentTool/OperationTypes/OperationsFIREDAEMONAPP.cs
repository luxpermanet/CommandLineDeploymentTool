using CommandLineDeploymentTool.Helpers;
using System;

namespace CommandLineDeploymentTool.OperationTypes
{
    class OperationsFIREDAEMONAPP : OperationsBase
    {
        public OperationsFIREDAEMONAPP(Arguments args)
            : base(args)
        { 
            // do nothing
        }

        public override void ValidateArguments()
        {
            base.ValidateArguments();

            if (string.IsNullOrEmpty(this.args.FireDaemonPath))
                throw new ArgumentNullException("fireDaemonPath", "fireDaemonPath can not be null or empty");

            if (!this.args.NoStop)
            {
                string arguments = "--status " + "\"" + this.args.AppName + "\"";
                string result = ProcessHelper.CallNativeWindowProcess(this.args.FireDaemonPath, arguments);
                if (result.Contains("service does not exist"))
                {
                    throw new ArgumentException("\"" + this.args.AppName + "\"" + " could not be found in FireDaemon", "appName");
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
            string arguments = "--status " + "\"" + this.args.AppName + "\"";
            string result = ProcessHelper.CallNativeWindowProcess(this.args.FireDaemonPath, arguments);

            if (result.Contains("is running"))
            {
                arguments = "--stop " + "\"" + this.args.AppName + "\"";
                ProcessHelper.CallNativeWindowProcess(this.args.FireDaemonPath, arguments);
            }
            else
            {
                Console.WriteLine(this.args.AppName + " FireDaemon application is not running. No action will be taken.");
            }
        }

        public override void Deploy()
        {
            base.Deploy();
        }

        public override void Start()
        {
            string arguments = "--start " + "\"" + this.args.AppName + "\"";
            ProcessHelper.CallNativeWindowProcess(this.args.FireDaemonPath, arguments);
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
