using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Storm.Interaction;

namespace Storm.Worker.Executor
{
    public class ExecutorConfig
    {
        public string HostEndpoint { get; private set; }
        public CancellationToken CancellationToken { get; private set; }


        public IWcfHost WcfHost { get; private set; }
        public ExecutorConfig(string hostEndpoint, CancellationToken cancellationToken, IWcfHost wcfHost)
        {
            HostEndpoint = hostEndpoint;
            CancellationToken = cancellationToken;
            WcfHost = wcfHost;
        }
    }
}
