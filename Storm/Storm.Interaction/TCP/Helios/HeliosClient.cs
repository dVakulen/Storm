using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helios.Buffers;
using Helios.Net;
using Helios.Net.Bootstrap;
using Helios.Ops;
using Helios.Ops.Executors;
using Helios.Reactor.Bootstrap;
using Helios.Serialization;
using Helios.Topology;
using Helios.Util;
using Helios.Util.Collections;
using ServiceStack.Text;

namespace Helios.MultiNodeTests.TestKit
{
    public  class HeliosClient
    {
        public  TransportType TransportType {
            get
            {
                return TransportType.Tcp;;
            }
        }

        /// <summary>
        /// Disables message capture and resorts to just using counters
        /// </summary>
        public virtual bool HighPerformance { get { return false; } }        

        public virtual int BufferSize { get { return 1024; } }

        public AtomicCounter ClientReceived { get; protected set; }

        public AtomicCounter ServerReceived { get; protected set; }

        public virtual IMessageEncoder Encoder { get { return Encoders.DefaultEncoder; } }

        public virtual IMessageDecoder Decoder { get { return Encoders.DefaultDecoder; } }

        public virtual IByteBufAllocator Allocator { get { return UnpooledByteBufAllocator.Default; } }

        public virtual IConnectionConfig Config { get { return new DefaultConnectionConfig(); } }

        private IConnectionFactory _clientConnectionFactory;

        private INode _serverNode;
        public void SetUp(INode serverNode)
        {
            _serverNode = serverNode;
            ClientReceived = new AtomicCounter(0);

            _clientExecutor = new TryCatchExecutor(exception => {});
            _clientConnectionFactory = new ClientBootstrap()
                .Executor(_clientExecutor)
                .SetTransport(TransportType)
                .SetEncoder(Encoder)
                .SetDecoder(Decoder)
                .SetAllocator(Allocator)
                .SetConfig(Config)
                .Build();
        }

        public void CleanUp()
        {
            _client.Close();
            _client = null;
        }

        public void StartClient()
        {
            _client = _clientConnectionFactory.NewConnection(_serverNode);
            _client.Receive += (data, channel) =>
            {
                ClientReceived.GetAndIncrement();
            };
            _client.OnConnection += (address, channel) => channel.BeginReceive();
            _client.Open();
        }

        public void Send<T>(T data)
        {

            //if (_client == null)
            //    StartClient();
            //var networkData = NetworkData.Create(_serverNode, data, data.Length);
            //_client.Send(networkData);
        }

        public void WaitUntilNMessagesReceived(int count)
        {
            WaitUntilNMessagesReceived(count, TimeSpan.FromSeconds(5));
        }


        public void WaitUntilNMessagesReceived(int count, TimeSpan timeout)
        {
            SpinWait.SpinUntil(() => ClientReceived.Current >= count, timeout);
        }

        //protected Exception[] ClientExceptions { get { return _clientExecutor.Exceptions.ToArray(); } }
        //protected Exception[] ServerExceptions { get { return _serverExecutor.Exceptions.ToArray(); } }

        private BasicExecutor _clientExecutor;

        private IConnection _client;
    }
}
