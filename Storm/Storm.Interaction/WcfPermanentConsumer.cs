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
    public class WcfPermanentConsumer<T, TFactory>
        //   where T : ICommunicationObject
        where TFactory : IBindingsFactory<Binding>
    {
        private string serverAddress;
        readonly Guid clientID;
        ServiceHost clientHost;
        private readonly TFactory BindingsFactory;
        private readonly string _endpointAdress ;
        private ChannelFactory<T> factory;
        private T channel;
        public WcfPermanentConsumer(TFactory factory, string endpointAdress)
        {
            BindingsFactory = factory;
            clientID = Guid.NewGuid();
            _endpointAdress = endpointAdress;
        }

        public void OpenConnection()
        {
             factory = new ChannelFactory<T>(BindingsFactory.GetBinding(),
                new EndpointAddress(_endpointAdress));
            channel = factory.CreateChannel();
         ( (ICommunicationObject) channel).Open();

        }
        public void Execute(Action<T> action)
        {
           action.Invoke(channel);
        }

        public void CloseChannel(ICommunicationObject  channelToCLose)
        {
            try
            {
                channelToCLose.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);//todo: logger
            }
            finally
            {
              //  factory.
                channelToCLose.Abort();
            }
        }

        public void CloseConnection ()
        {
            CloseChannel((ICommunicationObject)channel);
            //factory.BeginClose();
        }

    }
}
