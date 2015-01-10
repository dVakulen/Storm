using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Storm.Interfaces;

namespace Storm.Interaction
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class WcfConsumer
    {
        Guid _clientID;
        ServiceHost _clientHost;
        public WcfConsumer()
        {
            _clientID = Guid.NewGuid();
            _clientHost = new ServiceHost(this);

            //  _clientHost.AddServiceEndpoint((typeof(IFromServerToClientMessages)), new NetNamedPipeBinding(), "net.pipe://localhost/Client_" + _clientID.ToString());
            //  _clientHost.Open();
        }
        public void Register(Guid clientID)
        {
            using (ChannelFactory<IFromClientToServerMessages> factory = new ChannelFactory<IFromClientToServerMessages>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/Server")))
            {
                IFromClientToServerMessages clientToServerChannel = factory.CreateChannel();
                try
                {
                    clientToServerChannel.Register(clientID);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    CloseChannel((ICommunicationObject)clientToServerChannel);
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

    }
}
