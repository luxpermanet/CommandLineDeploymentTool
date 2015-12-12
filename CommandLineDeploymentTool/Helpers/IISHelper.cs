using Microsoft.Web.Administration;

namespace CommandLineDeploymentTool.Helpers
{
    static class IISHelper
    {
        public static void ApplicationPoolRecycle(string applicationPoolName)
        {
            ServerManager iis = new ServerManager();
            iis.ApplicationPools[applicationPoolName].Recycle();
        }

        public static void ApplicationPoolStop(string applicationPoolName)
        {
            ServerManager iis = new ServerManager();
            iis.ApplicationPools[applicationPoolName].Stop();
        }

        public static void ApplicationPoolStart(string applicationPoolName)
        {
            ServerManager iis = new ServerManager();
            iis.ApplicationPools[applicationPoolName].Start();
        }

        public static void WebsiteStart(string siteName)
        {
            ServerManager iis = new ServerManager();
            iis.Sites[siteName].Start();
        }

        public static void WebsiteStop(string siteName)
        {
            ServerManager iis = new ServerManager();
            iis.Sites[siteName].Stop();
        }
    }
}
