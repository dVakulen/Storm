using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ITestSample//todo : remove
    {
        [OperationContract]
        string GetString(string data);
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class TestSample : ITestSample //todo : remove
    {

        public string GetString(string data)
        {
            return data + "processed";
        }
    }
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ITestASmLoader//todo : remove
    {
        [OperationContract]
        byte[] GetAsm(string data);
    }

   
    public interface IStreamingSample
    {
        [OperationContract]
        Stream GetStream(string data);

        [OperationContract]
        bool UploadStream(Stream stream);

        [OperationContract]
        Stream EchoStream(Stream stream);

        [OperationContract]
        Stream GetReversedStream();

    }
}
