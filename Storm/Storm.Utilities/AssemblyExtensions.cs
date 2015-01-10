using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Utilities
{
    public class AssemblyHandling
    {
        public static byte[] LoadAssemblyBytes(string assemblyPath)
        {
            return File.ReadAllBytes(assemblyPath);
        }

        public void LoadAssembly(AppDomainSetup domainSetup)
        {
            AppDomain childDomain = null;
            try{
            //{
            //    AppDomainSetup domainSetup = new AppDomainSetup()
            //    {
            //        ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
            //        ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile,
            //        ApplicationName = AppDomain.CurrentDomain.SetupInformation.ApplicationName,
            //        LoaderOptimization = LoaderOptimization.MultiDomainHost
            //    };

                // Create the child AppDomain used for the service tool at runtime.
                childDomain = AppDomain.CreateDomain(
                    "Child AppDomain", null, domainSetup);

                // Create an instance of the runtime in the second AppDomain. 
                // A proxy to the object is returned.
                //IRuntime runtime = (IRuntime)childDomain.CreateInstanceAndUnwrap(
                //    typeof(Runtime).Assembly.FullName, typeof(Runtime).FullName);

                //// start the runtime.  call will marshal into the child runtime appdomain
                //Console.WriteLine(runtime.Run("q"));
            }
            finally
            {
                // runtime has exited, finish off by unloading the runtime appdomain
                if (childDomain != null) AppDomain.Unload(childDomain);
            }
        }
    }
}
