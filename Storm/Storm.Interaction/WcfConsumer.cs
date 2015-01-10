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
    public class WcfConsumer<T, TFactory, TBinding>
        where T : ICommunicationObject
        where TBinding : Binding
        where TFactory : IBindingsFactory<TBinding>
    {
        private string serverAddress;
        readonly Guid clientID;
        ServiceHost clientHost;
        private readonly TFactory BindingsFactory;
        public WcfConsumer(TFactory factory)
        {
            BindingsFactory = factory;
            clientID = Guid.NewGuid();
            clientHost = new ServiceHost(this);
            serverAddress = Settings.GetServerAddress();
            //  _clientHost.AddServiceEndpoint((typeof(IFromServerToClientMessages)), new NetNamedPipeBinding(), "net.pipe://localhost/Client_" + _clientID.ToString());
            //  _clientHost.Open();
        }


        public void Execute(Action<T> action)
        {
            using (ChannelFactory<T> factory = new ChannelFactory<T>(new NetNamedPipeBinding(), new EndpointAddress(serverAddress))) //BindingsFactory.get
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
        private void CloseChannel(T channel)
        {
            CloseChannel((ICommunicationObject)channel);
        }

    }
}
