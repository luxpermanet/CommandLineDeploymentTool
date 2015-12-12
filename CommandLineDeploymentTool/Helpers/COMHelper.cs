using COMAdmin;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading;

namespace CommandLineDeploymentTool.Helpers
{
    public class COMHelper
    {
        public static COMAdminCatalogCollection objCollection { get; set; }
        public static NameValueCollection collection { get; set; }
        public static COMAdminCatalog objAdmin { get; set; }
        private const string COMExtensionPattern = @"\.[\s\S]*";

        public static COMAdminCatalogCollection GetApplicationCollection()
        { 
            objAdmin = new COMAdmin.COMAdminCatalog();
            objCollection = (COMAdmin.COMAdminCatalogCollection)objAdmin.GetCollection("Applications");

            return objCollection;
        }

        public static List<ICatalogCollection> GetComponentCollection()
        {
            List<ICatalogCollection> comAdminCatalogCollectionClass = new List<ICatalogCollection>();
            objCollection = GetApplicationCollection();
            objCollection.Populate();

            foreach (COMAdmin.COMAdminCatalogObject objAppNames in objCollection)
            {
                COMAdmin.ICatalogCollection objComponents = (COMAdmin.ICatalogCollection)objCollection.GetCollection("Components", objAppNames.Key);
                objComponents.Populate();
                comAdminCatalogCollectionClass.Add(objComponents);
            }

            return comAdminCatalogCollectionClass;
        }

        public static NameValueCollection GetCOMComponents()
        {
            collection = new NameValueCollection();
            objCollection = GetApplicationCollection();
            objCollection.Populate();

            foreach (COMAdmin.COMAdminCatalogObject objAppNames in objCollection)
            {
                COMAdmin.ICatalogCollection objComponents = (COMAdmin.ICatalogCollection)objCollection.GetCollection("Components", objAppNames.Key);
                objComponents.Populate();
                foreach (COMAdmin.COMAdminCatalogObject Components in objComponents)
                {
                    collection.Add(Components.Name.ToString(), objAppNames.Name.ToString());
                }
            }

            return collection;
        }

        public static void DeleteCOMApplication(string applicationName)
        {
            ICatalogCollection catalogColl = GetApplicationCollection();
            ICatalogObject catalogObj;
            catalogColl.Populate();

            if (catalogColl.Count == 0)
            {
                Console.WriteLine("No application found...");
                return;
            }

            for (int i = 0; i < catalogColl.Count; i++)
            {
                catalogObj = (ICatalogObject)catalogColl.get_Item(i);
                if (applicationName == ((string)catalogObj.Name))
                {
                    catalogColl.Remove(i);
                    catalogColl.SaveChanges();
                    Thread.Sleep(100);
                    //Console.WriteLine(applicationName + " application has been deleted...")
                    return;
                }
            }
        }

        public static bool HasCOMComponent(string componentName)
        {
            List<ICatalogCollection> componentColl = GetComponentCollection();
            List<int> itemsToDelete = new List<int>();
            ICatalogObject catalogObj;
            
            foreach (ICatalogCollection item in componentColl)
            {
                itemsToDelete.Clear();
                item.Populate();

                if (item.Count == 0)
                    continue;

                for (int i = 0; i < item.Count; i++)
                {
                    catalogObj = (ICatalogObject)item.get_Item(i);
                    string iteratedCompName = catalogObj.Name as string;
                    iteratedCompName = iteratedCompName.Trim();
                    string desiredCompName = componentName.Trim();

                    Regex regex = new Regex(desiredCompName + COMExtensionPattern, RegexOptions.IgnoreCase);
                    string found = regex.Match(iteratedCompName).Value;
                    if (iteratedCompName.Length == found.Length)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void DeleteCOMComponent(string componentName)
        {
            List<ICatalogCollection> componentColl = GetComponentCollection();
            List<int> itemsToDelete = new List<int>();
            ICatalogObject catalogObj;

            foreach (ICatalogCollection item in componentColl)
            {
                itemsToDelete.Clear();
                item.Populate();

                if (item.Count == 0)
                    continue;

                for (int i = 0; i < item.Count; i++)
                {
                    catalogObj = (ICatalogObject)item.get_Item(i);
                    string iteratedCompName = catalogObj.Name as string;
                    iteratedCompName = iteratedCompName.Trim();
                    string desiredCompName = componentName.Trim();

                    Regex regex = new Regex(desiredCompName + COMExtensionPattern, RegexOptions.IgnoreCase);
                    string found = regex.Match(iteratedCompName).Value;
                    if (iteratedCompName.Length == found.Length)
                    {
                        itemsToDelete.Add(i);
                    }
                }

                bool removed = false;
                int key;
                for (int i = 0; i < itemsToDelete.Count; i++)
                {
                    key = itemsToDelete[i] - i;
                    Console.WriteLine("Removing: " + ((ICatalogObject)item.get_Item(key)).Name);
                    item.Remove(key);
                    removed = true;
                }

                if (removed)
                {
                    item.SaveChanges();
                    Thread.Sleep(100);
                    return;
                }
            }
        }

        public static void StartCOMApplication(string applicationName)
        {
            objCollection = GetApplicationCollection();
            objAdmin.StartApplication(applicationName);
            Thread.Sleep(100);
            //Console.WriteLine("Application has been started...");
        }

        public static void ShutdownCOMApplication(string applicationName)
        {
            objCollection = GetApplicationCollection();
            objAdmin.ShutdownApplication(applicationName);
            Thread.Sleep(100);
            //Console.WriteLine("Application has been shutdown...");
        }

        public static void InstallCOMComponent(string applicationName, string dllName)
        {
            objCollection = GetApplicationCollection();
            objAdmin.InstallComponent(applicationName, dllName, string.Empty, string.Empty);
            objCollection.SaveChanges();
            objCollection.Populate();
            Thread.Sleep(100);
            //Console.WriteLine("Component has been installed...");
        }

        public static void CreateApplication(string strAppName)
        {
            objCollection = GetApplicationCollection();
            COMAdmin.COMAdminCatalogObject obj = (COMAdmin.COMAdminCatalogObject)objCollection.Add();
            obj.set_Value("Name", strAppName);
            objCollection.SaveChanges();
            Thread.Sleep(100);
            //Console.WriteLine("Application has been created...");
        }
    }
}
