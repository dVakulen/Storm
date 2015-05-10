using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Topology
{
    public interface IBasicOutputCollector
    {
        List<int> Emit(string streamId, List<object> tuple);
        void EmitDirect(int taskId, string streamId, List<Object> tuple);
        void ReportError(Exception ex);
    }
}
