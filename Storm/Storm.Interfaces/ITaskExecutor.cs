// Lance Roberts 04-Mar-2010
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace Storm.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ITaskExecutor
    {
        [OperationContract(IsOneWay = true)]
        void Execute(string text);
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public class TaskExecutor : ITaskExecutor
    {
        public void Execute(string text)
        {
           // Thread.Sleep(5);
        //   Console.WriteLine("executed " + text);
          //  var z = "executed " + text;
         //   var b = z;
        }
    }
}
