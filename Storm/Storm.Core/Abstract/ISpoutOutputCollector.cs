using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Core.Abstract
{
    public interface ISpoutOutputCollector
    {
        /**
            Returns the task ids that received the tuples.
        */
        List<int> Emit(string streamId, List<object> tuple, object messageId);
        void EmitDirect(int taskId, String streamId, List<object> tuple, object messageId);
        void ReportError(Exception error);
    }
}
