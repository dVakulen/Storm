﻿using System;
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

namespace Helios.MultiNodeTests.TestKit
{
    public abstract class MultiNodeTest
    {
        public abstract TransportType TransportType { get; }

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

        public void SetUp()
        {
            ServerReceived = new AtomicCounter(0);
           

            _clientExecutor = new TryCatchExecutor(exception => {});
            _serverExecutor = new TryCatchExecutor(exception => {});
            var serverBootstrap = new ServerBootstrap()
                   .WorkerThreads(1)
                   .Executor(_serverExecutor)
                   .SetTransport(TransportType)
                   .SetEncoder(Encoder)
                   .SetDecoder(Decoder)
                   .SetAllocator(Allocator)
                   .SetConfig(Config)
                   .Build();

            _server = serverBootstrap.NewConnection(Node.Loopback());
        }

        public void CleanUp()
        {
            _server.Close();
            _server = null;
        }

        protected void StartServer()
        {
            StartServer((data, channel) =>
            {
                //if (!HighPerformance)
                //{
                //    ServerReceiveBuffer.Add(data);
                //}
                ServerReceived.GetAndIncrement();
                channel.Send(new NetworkData() { Buffer = data.Buffer, Length = data.Length, RemoteHost = channel.RemoteHost });
                //Console.Write("");
            });
        }

        /// <summary>
        /// Used to start the server with a specific receive data callback
        /// </summary>
        protected void StartServer(ReceivedDataCallback callback)
        {
            _server.Receive += (data, channel) =>
            {
                callback(data, channel);
            };
            _server.OnConnection += (address, channel) =>
            {
                channel.BeginReceive();
            };
         //   _server.OnError += (exception, connection) => _serverExecutor.Exceptions.Add(exception);
            _server.Open();
        }


        protected void Send(byte[] data)
        {
            var networkData = NetworkData.Create(_server.Local, data, data.Length);
            if(!HighPerformance)
                ClientSendBuffer.Add(networkData);
        }

        protected void WaitUntilNMessagesReceived(int count)
        {
            WaitUntilNMessagesReceived(count, TimeSpan.FromSeconds(5));
        }


        protected void WaitUntilNMessagesReceived(int count, TimeSpan timeout)
        {
            SpinWait.SpinUntil(() => ClientReceived.Current >= count, timeout);
        }

        //protected Exception[] ClientExceptions { get { return _clientExecutor.Exceptions.ToArray(); } }
        //protected Exception[] ServerExceptions { get { return _serverExecutor.Exceptions.ToArray(); } }

        private BasicExecutor _clientExecutor;
        private BasicExecutor _serverExecutor;

        protected ConcurrentCircularBuffer<NetworkData> ClientSendBuffer { get; private set; }

        protected ConcurrentCircularBuffer<NetworkData> ClientReceiveBuffer { get; private set; }

        protected ConcurrentCircularBuffer<NetworkData> ServerReceiveBuffer { get; private set; }

        public IConnection _server;
    }
}
