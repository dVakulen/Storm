using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Storm.Interaction;
using Storm.Interaction.Bindings;
using Storm.Interfaces;

namespace Storm.Worker.Executor
{
    public class Executor : IExecutor
    {
        private readonly CancellationToken _cancellationToken;
        private readonly string _hostEndpoint;
        private IWcfHost _wcfHost;


        public Executor(ExecutorConfig executorConfig)
        {
            _cancellationToken = executorConfig.CancellationToken;
            _hostEndpoint = executorConfig.HostEndpoint;
            _wcfHost = executorConfig.WcfHost;
        }

        public void Run()
        {
            //var th = new Thread(() =>
            //{
            //    _wcfHost.Start();

            //});
            //th.Priority =ThreadPriority.Highest;
            //th.Start();
            Task.Run(() =>
            {
                _wcfHost.Start();

            }, _cancellationToken);
        }

        public void Stop()
        {
            _wcfHost.Stop();
        }
    }
}
