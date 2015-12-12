using CommandLineDeploymentTool.Helpers;
using System;
using System.IO;

namespace CommandLineDeploymentTool.OperationTypes
{
    class OperationsIIS : OperationsBase
    {
        public OperationsIIS(Arguments args)
            : base(args)
        { 
            // do nothing
        }

        public override void ValidateArguments()
        {
            base.ValidateArguments();
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
            IISHelper.ApplicationPoolRecycle(this.args.ApplicationPoolName);
        }

        public override void Deploy()
        {
            base.Deploy();
        }

        public override void Start()
        {
            IISHelper.ApplicationPoolRecycle(this.args.ApplicationPoolName);
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
