using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Topology
{
   
//public class BasicOutputCollector : IBasicOutputCollector {
//    private OutputCollector outputCollector;
//    private Tuple inputTuple;

//    public BasicOutputCollector(OutputCollector outputCollector) {
//        this.outputCollector = outputCollector;
//    }

//    public List<int> emit(string streamId, List<Object> tuple) {
//        return outputCollector.emit(streamId, inputTuple, tuple);
//    }

//    public List<int> emit(List<Object> tuple) {
//        return emit(Utils.DEFAULT_STREAM_ID, tuple);
//    }

//    public void setContext(Tuple inputTuple) {
//        this.inputTuple = inputTuple;
//    }

//    public void emitDirect(int taskId, string streamId, List<Object> tuple) {
//        outputCollector.emitDirect(taskId, streamId, inputTuple, tuple);
//    }

//    public void emitDirect(int taskId, List<Object> tuple) {
//        emitDirect(taskId, Utils.DEFAULT_STREAM_ID, tuple);
//    }

//    protected IOutputCollector getOutputter() {
//        return outputCollector;
//    }

//    public void reportError(Exception t) {
//        outputCollector.reportError(t);
//    }
//}

}
