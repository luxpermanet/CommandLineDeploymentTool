using CommandLineDeploymentTool.OperationTypes;
using System;

namespace CommandLineDeploymentTool
{
    class OperationsFactory
    {
        public static IOperations Create(Arguments args)
        {
            switch (args.DeployType)
            {
                case "COM":
                    return new OperationsCOM(args);
                case "EXE":
                    return new OperationsEXE(args);
                case "FIREDAEMONAPP":
                    return new OperationsFIREDAEMONAPP(args);
                case "IIS":
                    return new OperationsIIS(args);
                case "WINDOWSSERVICE":
                    return new OperationsWINDOWSSERVICE(args);
                default:
                    throw new NotImplementedException("/deployType:" + (args.DeployType ?? string.Empty) + " is not implemented");
            }
        }
    }
}
