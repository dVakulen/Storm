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
    public class WcfConsumer<TClientInterface, TFactory>
        //   where T : ICommunicationObject
        where TFactory : IBindingsFactory<Binding>
    {
        private string serverAddress;
        readonly Guid clientID;
        ServiceHost clientHost;
        private readonly TFactory BindingsFactory;
        public WcfConsumer(TFactory factory, string consumerAddress, object serviceSingleton)
        {
            BindingsFactory = factory;
            clientID = Guid.NewGuid();
            clientHost = new ServiceHost(serviceSingleton);
            clientHost.AddServiceEndpoint((typeof(TClientInterface)), new NetNamedPipeBinding(), consumerAddress);//"net.pipe://localhost/Client_" + _clientID.ToString()
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

        public  void Execute<T>(Action<T> action, string endpointAddress)
        {
            //var binding = BindingsFactory.GetBinding();
            var binding = new NetNamedPipeBinding();
            binding.MaxReceivedMessageSize = int.MaxValue;
            using (ChannelFactory<T> factory = new ChannelFactory<T>(binding, new EndpointAddress(endpointAddress))) //BindingsFactory.get
            {
                T clientToServerChannel = factory.CreateChannel();
                try
                {
                    action.Invoke(clientToServerChannel);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    CloseChannel(clientToServerChannel);
                }
            }
        }
        private void CloseChannel(ICommunicationObject channel)
        {
            try
            {
                channel.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                channel.Abort();
            }
        }
        private void CloseChannel(object channel)
        {
            CloseChannel((ICommunicationObject)channel);
        }

    }
}
