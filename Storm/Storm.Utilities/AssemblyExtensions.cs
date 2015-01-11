using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Utilities
{
    public enum LoadMethod
    {
        /// <summary>
        /// Loads the assembly into the LoadFrom context, which enables the assembly and all it's references to be discovered
        /// and loaded into the target application domain. Despite it's penchant for DLL hell, this is probably the way to go by
        /// default as long as you make sure to pass the base directory of the application to an AssemblyResolver instance such
        /// that references can be properly resolved. This also allows for multiple assemblies of the same name to be loaded while
        /// maintaining separate file names. This is the recommended way to go.
        /// </summary>
        LoadFrom,

        /// <summary>
        /// Loads an assembly into memory using the raw file name. This loads the assembly anonymously, so it won't have
        /// a load context. Use this if you want the bits loaded, but make sure to pass the directory where this file lives to an 
        /// AssemblyResolver instance so you can find it again. This is similar to LoadFrom except you don't get the free 
        /// lookups for already existing assembly names via fusion. Use this for more control over assembly file loads.
        /// </summary>
        LoadFile,

        /// <summary>
        /// Loads the bits of the target assembly into memory using the raw file name. This is, in essence, a dynamic assembly
        /// for all the CLR cares. You won't ever be able to find this with an assembly resolver, so don't use this unless you look
        /// for it by name. Be careful with this one.
        /// </summary>
        LoadBits
    }
    public class AssemblyLoader : MarshalByRefObject
    {
        #region Public Methods

        public Assembly LoadAssembly(LoadMethod loadMethod, string assemblyPath, string pdbPath = null)
        {
            Assembly assembly = null;
            switch (loadMethod)
            {
                case LoadMethod.LoadFrom:
                    assembly = Assembly.LoadFrom(assemblyPath);
                    break;
                case LoadMethod.LoadFile:
                    assembly = Assembly.LoadFile(assemblyPath);
                    break;
                case LoadMethod.LoadBits:

                    // Attempt to load the PDB bits along with the assembly to avoid image exceptions.
                    pdbPath = string.IsNullOrEmpty(pdbPath) ? Path.ChangeExtension(assemblyPath, "pdb") : pdbPath;

                    // Only load the PDB if it exists--we may be dealing with a release assembly.
                    if (File.Exists(pdbPath))
                    {
                        assembly = Assembly.Load(
                            File.ReadAllBytes(assemblyPath),
                            File.ReadAllBytes(pdbPath));
                    }
                    else
                    {
                        assembly = Assembly.Load(File.ReadAllBytes(assemblyPath));
                    }

                    break;
                default:
                    // In case we upadate the enum but forget to update this logic.
                    throw new NotSupportedException("The target load method isn't supported!");
            }

            return assembly;
        }

        public IList<Assembly> LoadAssemblyWithReferences(LoadMethod loadMethod, string assemblyPath)
        {
            var list = new List<Assembly>();
            var assembly = this.LoadAssembly(loadMethod, assemblyPath);
            list.Add(assembly);

            foreach (var reference in assembly.GetReferencedAssemblies())
            {
                list.Add(Assembly.Load(reference));
            }

            return list;
        }

        public Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        #endregion
    }
    public static class AssemblyHandling
    {
        private static IEnumerable<string> GetBinFolders()
        {
            //TODO: The AppDomain.CurrentDomain.BaseDirectory usage is not correct in 
            //some cases. Need to consider PrivateBinPath too
            List<string> toReturn = new List<string>();
            //slightly dirty - needs reference to System.Web.  Could always do it really
            //nasty instead and bind the property by reflection!
            
                toReturn.Add(AppDomain.CurrentDomain.BaseDirectory);

            return toReturn;
        }

        private static void PreLoadDeployedAssemblies()
        {
            foreach (var path in GetBinFolders())
            {
                PreLoadAssembliesFromPath(path);
            }
        }

        private static void PreLoadAssembliesFromPath(string p)
        {
            //S.O. NOTE: ELIDED - ALL EXCEPTION HANDLING FOR BREVITY

            //get all .dll files from the specified path and load the lot
            FileInfo[] files = null;
            //you might not want recursion - handy for localised assemblies 
            //though especially.
            files = new DirectoryInfo(p).GetFiles("*.dll",
                SearchOption.AllDirectories);

            AssemblyName a = null;
            string s = null;
            foreach (var fi in files)
            {
                s = fi.FullName;
                //now get the name of the assembly you've found, without loading it
                //though (assuming .Net 2+ of course).
                a = AssemblyName.GetAssemblyName(s);
                //sanity check - make sure we don't already have an assembly loaded
                //that, if this assembly name was passed to the loaded, would actually
                //be resolved as that assembly.  Might be unnecessary - but makes me
                //happy :)
                if (!AppDomain.CurrentDomain.GetAssemblies().Any(assembly =>
                  AssemblyName.ReferenceMatchesDefinition(a, assembly.GetName())))
                {
                    //crucial - USE THE ASSEMBLY NAME.
                    //in a web app, this assembly will automatically be bound from the 
                    //Asp.Net Temporary folder from where the site actually runs.
                    Assembly.Load(a);
                }
            }
        }
        public static byte[] LoadAssemblyBytes(string assemblyPath)
        {
            return File.ReadAllBytes(assemblyPath);
        }

        public static void LoadAssembly(AppDomainSetup domainSetup)
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
