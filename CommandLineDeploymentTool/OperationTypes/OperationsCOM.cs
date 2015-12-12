using CommandLineDeploymentTool.Helpers;
using System;
using System.IO;

namespace CommandLineDeploymentTool.OperationTypes
{
    class OperationsCOM : OperationsBase
    {
        private const string EXT = ".dll";

        public OperationsCOM(Arguments args)
            : base(args)
        { 
            // do nothing
        }

        public override void ValidateArguments()
        {
            base.ValidateArguments();

            if (string.IsNullOrEmpty(this.args.CategoryName))
                throw new ArgumentNullException("categoryName", "categoryName can not be null or empty");

            if (!this.args.NoStop && !COMHelper.HasCOMComponent(this.args.AppName))
                throw new ArgumentException(this.args.AppName + " component could not be found", "appName");
        }

        public override void Backup()
        {
            string destDir = Path.Combine(this.args.BackupFolder, this.args.AppName);
            destDir = Path.Combine(destDir, DateTime.Now.ToString("Backup_yyyyMMdd_HHmmss"));
            this.args.RestorePath = destDir;
            CopyHelper.DirectoryCopyWithRef(this.args.AppFolder, destDir, this.args.DeployFolder, true);
        }

        public override void Restore()
        {
            base.Restore();
        }

        public override void Stop()
        {
            COMHelper.ShutdownCOMApplication(this.args.CategoryName);

            if (COMHelper.HasCOMComponent(this.args.AppName))
                COMHelper.DeleteCOMComponent(this.args.AppName);

            COMHelper.ShutdownCOMApplication(this.args.CategoryName);
        }

        public override void Deploy()
        {
            base.Deploy();
        }

        public override void Start()
        {
            string dllPath = Path.Combine(this.args.AppFolder, this.args.AppName + EXT);
            COMHelper.InstallCOMComponent(this.args.CategoryName, dllPath);
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
