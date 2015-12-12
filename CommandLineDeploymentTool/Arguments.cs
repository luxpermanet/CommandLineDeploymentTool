using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineDeploymentTool
{
    class Arguments
    {
        public string DeployType { get; private set; }
        public string BackupFolder { get; private set; }
        public string AppName { get; private set; }
        public string AppFolder { get; private set; }
        public string DeployFolder { get; private set; }
        public string CategoryName { get; private set; }
        public string FireDaemonPath { get; private set; }
        public bool Restore { get; set; }
        public string RestorePath { get; set; }
        public string ApplicationPoolName { get; private set; }
        public bool NoBackup { get; private set; }
        public bool NoStop { get; private set; }
        public bool NoStart { get; private set; }

        public Arguments(string[] args, bool fineTuneArgs)
        {
            if (fineTuneArgs)
            {
                // handles quotations if necessary
                args = this.FineTune(args);
            }
            this.AssignFields(args);
        }

        private string[] FineTune(string[] args)
        {
            List<string> argList = new List<string>();
            foreach (string arg in args)
            {
                if (arg.Trim().StartsWith("/"))
                {
                    argList.Add(arg);
                }
                else
                {
                    string lastItem = argList[argList.Count - 1];
                    lastItem += " " + arg;
                    argList[argList.Count - 1] = lastItem;
                }
            }

            return argList.ToArray();
        }

        private void AssignFields(string[] args)
        {
            foreach (string arg in args)
            {
                int indexOfColon = arg.Contains(":") ? arg.IndexOf(':') : arg.Length;

                string argKey = arg.Substring(1, indexOfColon - 1);
                string argVal = arg.Length > indexOfColon ? arg.Substring(indexOfColon + 1, arg.Length - indexOfColon - 1) : string.Empty;

                switch (argKey)
                { 
                    case "deployType":
                        this.DeployType = argVal;
                        break;
                    case "backupFolder":
                        this.BackupFolder = argVal;
                        break;
                    case "appName":
                        this.AppName = argVal;
                        break;
                    case "appFolder":
                        this.AppFolder = argVal;
                        break;
                    case "deployFolder":
                        this.DeployFolder = argVal;
                        break;
                    case "categoryName":
                        this.CategoryName = argVal;
                        break;
                    case "fireDaemonPath":
                        this.FireDaemonPath = argVal;
                        break;
                    case "restore":
                        this.Restore = true;
                        break;
                    case "restorePath":
                        this.RestorePath = argVal;
                        break;
                    case "applicationPool":
                        this.ApplicationPoolName = argVal;
                        break;
                    case "noBackup":
                        this.NoBackup = true;
                        break;
                    case "noStop":
                        this.NoStop = true;
                        break;
                    case "noStart":
                        this.NoStart = true;
                        break;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Environment.GetCommandLineArgs()[0]).Append(" ");

            if (!string.IsNullOrEmpty(this.DeployType))
                sb.Append("/deployType:" + this.DeployType).Append(" ");
            if (!string.IsNullOrEmpty(this.BackupFolder))
                sb.Append("/backupFolder:" + this.BackupFolder).Append(" ");
            if (!string.IsNullOrEmpty(this.AppName))
                sb.Append("/appName:" + this.AppName).Append(" ");
            if (!string.IsNullOrEmpty(this.AppFolder))
                sb.Append("/appFolder:" + this.AppFolder).Append(" ");
            if (!string.IsNullOrEmpty(this.DeployFolder))
                sb.Append("/deployFolder:" + this.DeployFolder).Append(" ");
            if (!string.IsNullOrEmpty(this.CategoryName))
                sb.Append("/categoryName:" + this.CategoryName).Append(" ");
            if (!string.IsNullOrEmpty(this.FireDaemonPath))
                sb.Append("/fireDaemonPath:" + this.FireDaemonPath).Append(" ");
            if (this.Restore)
                sb.Append("/restore:").Append(" ");
            if (!string.IsNullOrEmpty(this.RestorePath))
                sb.Append("/restorePath:" + this.RestorePath).Append(" ");
            if (!string.IsNullOrEmpty(this.ApplicationPoolName))
                sb.Append("/applicationPool:" + this.ApplicationPoolName).Append(" ");
            if (this.NoBackup)
                sb.Append("/noBackup:").Append(" ");
            if (this.NoStop)
                sb.Append("/noStop:").Append(" ");
            if (this.NoStart)
                sb.Append("/noStart:").Append(" ");

            return sb.ToString();
        }

        public static string HelpString
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(@"Usage:");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /deployType:<deployType>");
                sb.AppendLine(@"Desc.   : Type of deployment");
                sb.AppendLine(@"Sample  : /deployType:COM");
                sb.AppendLine(@"Sample  : /deployType:EXE");
                sb.AppendLine(@"Sample  : /deployType:FIREDAEMONAPP");
                sb.AppendLine(@"Sample  : /deployType:IIS");
                sb.AppendLine(@"Sample  : /deployType:WINDOWSSERVICE");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /backupRootFolder:<backupRootFolder>");
                sb.AppendLine(@"Desc.   : Use this pat as a root to backups");
                sb.AppendLine(@"Sample  : /backupRootFolder:D:\Backups");
                sb.AppendLine();
                sb.AppendLine(@"Format                  : /appName:<appName>");
                sb.AppendLine(@"Desc.                   : Name of the application (Generally dll or exe name)");
                sb.AppendLine(@"Sample (COM)            : /appName:MyApp (dll name)");
                sb.AppendLine(@"Sample (EXE)            : /appName:MyApp (exe name)");
                sb.AppendLine(@"Sample (FIREDAEMONAPP)  : /appName:MyApp (name seen in firedaemon)");
                sb.AppendLine(@"Sample (IIS)            : /appName:MyApp (not important in iis case, you may give anything)");
                sb.AppendLine(@"Sample (WINDOWSSERVICE) : /appName:Myapp (exe name)");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /appFolder:<appFolder>");
                sb.AppendLine(@"Desc.   : Root folder of the application");
                sb.AppendLine(@"Sample  : /appFolder:D:\MyAppFolder");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /deployFolder:<deployFolder>");
                sb.AppendLine(@"Desc.   : Root folder of the deployed (new version) application");
                sb.AppendLine(@"Sample  : /deployFolder:D:\DeployFolder");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /categoryName:<categoryName>");
                sb.AppendLine(@"Desc.   : Name of the com category (Needed for category shutdown etc.). Only used in COM deployments");
                sb.AppendLine(@"Sample  : /categoryName:MyAppCategory");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /fireDaemonPath:<fireDaemonPath>");
                sb.AppendLine(@"Desc.   : Path for the FireDaemon.exe. Only used in FIREDAEMONAPP deployments");
                sb.AppendLine(@"Sample  : /fireDaemonPath:C:\Program Files\FireDaemon\FireDaemon.exe");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /restore");
                sb.AppendLine(@"Desc.   : Restore from backup, if the deployment fails. Or if you want to get back to previous version");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /restorePath:<restorePath>");
                sb.AppendLine(@"Desc.   : Path of the backup from which to restore");
                sb.AppendLine(@"Sample  : /restorePath:D:\Backups\MyApp\Backup_20150101_013030");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /applicationPool:<applicationPool>");
                sb.AppendLine(@"Desc.   : Name of the IIS ApplicationPool only used in IIS deployments");
                sb.AppendLine(@"Sample  : /applicationPool:DefaultAppPool");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /noBackup");
                sb.AppendLine(@"Desc.   : Bypasses backup step (Normal exection: Backup, Stop, Deploy, Start");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /noStop");
                sb.AppendLine(@"Desc.   : Bypasses stop step (Normal exection: Backup, Stop, Deploy, Start");
                sb.AppendLine();
                sb.AppendLine(@"Format  : /noStart");
                sb.AppendLine(@"Desc.   : Bypasses start step (Normal exection: Backup, Stop, Deploy, Start");

                return sb.ToString();
            }
        }
    }
}
