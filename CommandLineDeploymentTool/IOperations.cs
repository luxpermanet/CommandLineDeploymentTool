
namespace CommandLineDeploymentTool
{
    interface IOperations
    {
        void ValidateArguments();
        void Backup();
        void Restore();
        void Stop();
        void Deploy();
        void Start();
        void Execute();
        void Rollback();
    }
}
