// Lance Roberts 04-Mar-2010
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Storm.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ITaskExecutor
    {
        [OperationContract(IsOneWay = true)]
        void Execute(string text);
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class TaskExecutor : ITaskExecutor
    {
        public void Execute(string text)
        {
            Console.WriteLine("executed " + text);
        }
    }
}
