
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public interface IOutputCollector {
    /**
     *  Returns the task ids that received the tuples.
     */
    List<int> emit(string streamId, Collection<object> anchors, List<Object> tuple);
    void emitDirect(int taskId, string streamId, Collection<object> anchors, List<Object> tuple); //tuple
    void ack(object input);
    void fail(object input);
}
