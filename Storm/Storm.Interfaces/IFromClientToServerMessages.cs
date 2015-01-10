// Lance Roberts 04-Mar-2010
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Storm.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IFromClientToServerMessages
    {
        [OperationContract(IsOneWay = true)]
        void Register(Guid clientID);

        [OperationContract(IsOneWay = true)]
        void DisplayTextOnServer(string text);

        [OperationContract(IsOneWay = true)]
        void DisplayTextOnServerAsFromThisClient(Guid clientID, string text);

        [OperationContract]
        string GetLastAnonMessage();
    }
}
