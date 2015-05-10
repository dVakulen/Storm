
//using System;

//public class OutputFieldsGetter implements OutputFieldsDeclarer {
//    private Map<string, StreamInfo> _fields = new HashMap<string, StreamInfo>();

//    public void declare(Fields fields) {
//        declare(false, fields);
//    }

//    public void declare(boolean direct, Fields fields) {
//        declareStream(Utils.DEFAULT_STREAM_ID, direct, fields);
//    }

//    public void declareStream(string streamId, Fields fields) {
//        declareStream(streamId, false, fields);
//    }

//    public void declareStream(string streamId, boolean direct, Fields fields) {
//        if(_fields.containsKey(streamId)) {
//            throw new IllegalArgumentException("Fields for " + streamId + " already set");
//        }
//        _fields.put(streamId, new StreamInfo(fields.toList(), direct));
//    }


//    public Map<string, StreamInfo> getFieldsDeclaration() {
//        return _fields;
//    }

//}