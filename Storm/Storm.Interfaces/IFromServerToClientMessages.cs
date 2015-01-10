// Lance Roberts 04-Mar-2010
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Storm.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IFromServerToClientMessages
    {
        [OperationContract(IsOneWay = true)]
        void DisplayTextInClient(string text);
    }
}
