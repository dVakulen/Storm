using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Storm.Config;
using Storm.Core.Abstract;
using Storm.Interaction;
using Storm.Interaction.Bindings;
using Storm.Interfaces;
using Storm.Utilities;

namespace Storm
{
    class Program
    {
        static void Main(string[] args)
        {
            //Type objectType = (from asm in AppDomain.CurrentDomain.GetAssemblies()
            //                   from type in asm.GetTypes()
            //                   where ((typeof(IBasicBolt).IsAssignableFrom(type)
            //     && type.IsClass && type.IsAbstract))
            //                   select type).Single();
            //object obj = Activator.CreateInstance(objectType);

            //Console.WriteLine(((IBasicBolt)obj).Execute1("q"));




            //var start = new ProcessStartInfo
            //{
            //    FileName = image,
            //    Arguments = arguments,
            //    WorkingDirectory = Environment.CurrentDirectory,
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true,
            //    RedirectStandardError = true,
            //    CreateNoWindow = true,
            //    WindowStyle = ProcessWindowStyle.Hidden,
            //    ErrorDialog = false
            //};

            //return Process.Start(start);
           //WcfConsumer<ITestSample,NamedPipeBindingFactory> client = new WcfConsumer<ITestSample, NamedPipeBindingFactory>(new NamedPipeBindingFactory(), "net.pipe://localhost/Client_" + clientID.ToString(),new TestSample());
           // client.Start();


           // var clientID1 = Guid.NewGuid();
           // WcfConsumer<ITestSample, NamedPipeBindingFactory> client1 = new WcfConsumer<ITestSample, NamedPipeBindingFactory>(new NamedPipeBindingFactory(), "net.pipe://localhost/Client_" + clientID1.ToString(), new TestSample());
           // client1.Start();

           // client.Execute<ITestSample>(sample =>
           // {
           //     Console.WriteLine(sample.Getstring("w"));
           // },"net.pipe://localhost/Client_" + clientID1.ToString());

            Test1();
            //   Settings.CreateAppSettings();
        }

        private static void Test1()
        {
            var clientID = Guid.NewGuid();
            var client = new WcfConsumer<ITestASmLoader, NamedPipeBindingFactory>(new NamedPipeBindingFactory(),
                "net.pipe://localhost/Client_" + clientID.ToString(), new TestAsm());
            client.Start();


            var clientID1 = Guid.NewGuid();
            var client1 = new WcfConsumer<ITestASmLoader, NamedPipeBindingFactory>(new NamedPipeBindingFactory(),
                "net.pipe://localhost/Client_" + clientID1.ToString(), new TestAsm());
            client1.Start();

            client.Execute<ITestASmLoader>(sample =>
            {
                var asmBytes = sample.GetAsm("w");
                var assembly = Assembly.Load(asmBytes);
                var z = assembly.GetTypes();
                foreach (var basicBolt in z)
                {
                    Console.WriteLine(basicBolt.Name);
                }
                var z1 = assembly.GetReferencedAssemblies();
                foreach (var basicBolt in z1)
                {
                    Console.WriteLine(basicBolt.Name);
                }

                var instances = from t in z
                    //Assembly.GetExecutingAssembly().GetTypes()
                    where t.GetInterfaces().Contains(typeof (IBasicBolt))
                          && t.GetConstructor(Type.EmptyTypes) != null
                    select Activator.CreateInstance(t) as IBasicBolt;
                foreach (var basicBolt in instances)
                {
                    Console.WriteLine(basicBolt.Execute1("tt"));
                }
            }, "net.pipe://localhost/Client_" + clientID1.ToString());
        }

        private static void Proc()
        {

            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = "process.exe";
            process.StartInfo.Arguments = "-n";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.Start();
            process.WaitForExit();
          //  process.
        }
    }
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class TestAsm : ITestASmLoader //todo : remove
    {

        public byte[] GetAsm(string data)
        {
          return AssemblyHandling.LoadAssemblyBytes(Assembly.GetExecutingAssembly().Location); //(new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath
        }
    }
}
