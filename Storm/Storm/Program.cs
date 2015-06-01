using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helios.MultiNodeTests.TestKit;
using ServiceStack.Text;
using Storm.Config;
using Storm.Core.Abstract;
using Storm.Interaction;
using Storm.Interaction.Bindings;
using Storm.Interfaces;
using Storm.Utilities;
using Storm.Worker.Executor;

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

            TestHelios();
            //   Settings.CreateAppSettings();
        }



        private static void Test2()
        {
            var clientID = Guid.NewGuid();
          

            var clientID1 = Guid.NewGuid();

            var bindingFact = new NetTcpBindingFactory();
            var hostEndpoint = bindingFact.GetEndpointPrefics() + "localhost/TestHost";
            var cansellationTOkenSOurce = new CancellationTokenSource();

            var host = new WcfHost<ITaskExecutor, NetTcpBindingFactory>(new NetTcpBindingFactory(),
                 hostEndpoint, new TaskExecutor());
            var executor = new Executor(new ExecutorConfig(hostEndpoint, cansellationTOkenSOurce.Token, host));
            //host.Start();
            executor.Run();
            //var host = new WcfHost<ITestASmLoader, NamedPipeBindingFactory>(new NamedPipeBindingFactory(),
            //    "net.pipe://localhost/Client_" + clientID1.ToString(), new TestAsm());
            //host.Start();

            Stopwatch stopwatch = new Stopwatch();
            var client = new WcfPermanentConsumer<ITaskExecutor, NetTcpBindingFactory>(new NetTcpBindingFactory(), hostEndpoint);
            client.OpenConnection();
            stopwatch.Start();
            for (int i = 0; i < 30000; i++)
            {
                client.Execute(e =>
                {
                    e.Execute("tests " + i);
                });
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
        static void TestHelios()
        {
            var ddd = JsonPrimitives.Create(2);
            var z = new JsonSerializer<JsonPrimitives>();
            var dssd = JsonSerializer.SerializeToString(ddd);
            var serializedString = z.SerializeToString(ddd);


            var test = new TcpThroughputHarness();
            Console.WriteLine("Client Send --> Server Receive --> Server Send --> Client Receive");
            var generations = 3;
            var threadCount = Environment.ProcessorCount;
            for (int i = 0; i < generations; i++)
            {
                var workItems = 10000 * (int)Math.Pow(10, i);
                Console.WriteLine("Testing for {0} 200b messages", workItems);
                Console.WriteLine(TimeSpan.FromMilliseconds(
                    Enumerable.Range(0, 6).Select(_ =>
                    {
                        test.SetUp();
                        var sw = Stopwatch.StartNew();
                        test.RunBenchmark(workItems);
                        var elapsed = sw.ElapsedMilliseconds;
                        test.CleanUp();
                        return elapsed;
                    }).Skip(1).Average()));
            }
        }
        private static void Test1()
        {
            var clientID = Guid.NewGuid();
            var client = new WcfConsumer<NetTcpBindingFactory>(new NetTcpBindingFactory());


            var clientID1 = Guid.NewGuid();

            var bindingFact = new NamedPipeBindingFactory();
            var hostEndpoint = bindingFact.GetEndpointPrefics() + "localhost/TestHost";
            var cansellationTOkenSOurce = new CancellationTokenSource();

            var host = new WcfHost<ITaskExecutor, NetTcpBindingFactory>(new NetTcpBindingFactory(),
                 hostEndpoint, new TaskExecutor());
            var executor = new Executor(new ExecutorConfig(hostEndpoint, cansellationTOkenSOurce.Token, host));
            //host.Start();
            executor.Run();
            //var host = new WcfHost<ITestASmLoader, NamedPipeBindingFactory>(new NamedPipeBindingFactory(),
            //    "net.pipe://localhost/Client_" + clientID1.ToString(), new TestAsm());
            //host.Start();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                client.Execute<ITaskExecutor>(e =>
                {
                    e.Execute("tests " + i );
                }, hostEndpoint);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
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

        private static void testLoader()
        {
            //var asmBytes = sample.GetAsm("w");
            //var assembly = Assembly.Load(asmBytes);
            //var z = assembly.GetTypes();
            //foreach (var basicBolt in z)
            //{
            //    Console.WriteLine(basicBolt.Name);
            //}
            //var z1 = assembly.GetReferencedAssemblies();
            //foreach (var basicBolt in z1)
            //{
            //    Console.WriteLine(basicBolt.Name);
            //}

            //var instances = from t in z
            //                //Assembly.GetExecutingAssembly().GetTypes()
            //                where t.GetInterfaces().Contains(typeof(IBasicBolt))
            //                      && t.GetConstructor(Type.EmptyTypes) != null
            //                select Activator.CreateInstance(t) as IBasicBolt;
            //foreach (var basicBolt in instances)
            //{
            //    Console.WriteLine(basicBolt.Execute1("tt"));
            //}
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
    public class TcpThroughputHarness : MultiNodeTest
    {
        public override TransportType TransportType
        {
            get { return TransportType.Tcp; }
        }

        public override bool HighPerformance
        {
            get { return true; }
        }

        public void RunBenchmark(int messages)
        {
            
            //arrange
            StartServer(); //uses an "echo server" callback
            var z = new HeliosClient();
            z.SetUp(this._server.Local);
            z.StartClient();
            var messageLength = 200;
            var sends = messages;
            var message = System.Text.Encoding.UTF8.GetBytes("sddasd");//  new byte[messageLength];
            //act
            for (var i = 0; i < sends; i++)
            {
                z.Send(message);
            }
             z.WaitUntilNMessagesReceived(sends, TimeSpan.FromMinutes(3)); //set a really long timeout, just in case

        }
    }


    public class JsonPrimitives
    {
        public int Int { get; set; }
        public long Long { get; set; }
        public float Float { get; set; }
        public double Double { get; set; }
        public bool Boolean { get; set; }
        public DateTime DateTime { get; set; }
        public string NullString { get; set; }

        public static JsonPrimitives Create(int i)
        {
            return new JsonPrimitives
            {
                Int = i,
                Long = i,
                Float = i,
                Double = i,
                Boolean = i % 2 == 0,
                DateTime = DateTimeExtensions.FromUnixTimeMs(1),
            };
        }
    }
}
