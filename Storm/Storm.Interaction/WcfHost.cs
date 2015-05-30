using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Storm.Config;
using Storm.Interaction.Bindings;
using Storm.Interfaces;

namespace Storm.Interaction
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class WcfHost<TClientInterface, TFactory>
        where TFactory : IBindingsFactory<Binding>
    {
        ServiceHost clientHost;
        private readonly TFactory BindingsFactory;
        public WcfHost(TFactory factory, string hostAddress, TClientInterface serviceSingleton)
        {
            BindingsFactory = factory;
            clientHost = new ServiceHost(serviceSingleton);
            clientHost.AddServiceEndpoint((typeof(TClientInterface)), new NetNamedPipeBinding(), hostAddress);//"net.pipe://localhost/Client_" + _clientID.ToString()
        }

        public void Start()
        {
           // if (clientHost == null) throw new TypeInitializationException("WcfConsumer ", "must be initialized before accesing");
            clientHost.Open();
        }

        public void Stop()
        {
            clientHost.Close();
        }
    }
}
