/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;

public class GeneralTopologyContext  {
    //private StormTopology _topology;
    private Dictionary<int, string> _taskToComponent;
    private Dictionary<string, List<int>> _componentToTasks;
    private Dictionary<string, Dictionary<string, string>> _componentToStreamToFields; //Fields
    private string _stormId;
    protected Hashtable _stormConf;
    
    // pass in componentToSortedTasks for the case of running tons of tasks in single executor
    public GeneralTopologyContext(Hashtable stormConf, //StormTopology topology,
            Dictionary<int, string> taskToComponent, Dictionary<string, List<int>> componentToSortedTasks,
            Dictionary<string, Dictionary<string, string>> componentToStreamToFields, string stormId)
    {
      //  _topology = topology;
        _stormConf = stormConf;
        _taskToComponent = taskToComponent;
        _stormId = stormId;
        _componentToTasks = componentToSortedTasks;
        _componentToStreamToFields = componentToStreamToFields;
    }

    ///**
    // * Gets the unique id assigned to this topology. The id is the storm name with a
    // * unique nonce appended to it.
    // * @return the storm id
    // */
    //public string getStormId() {
    //    return _stormId;
    //}

    ///**
    // * Gets the Thrift object representing the topology.
    // * 
    // * @return the Thrift definition representing the topology
    // */
    //public StormTopology getRawTopology() {
    //    return _topology;
    //}

    ///**
    // * Gets the component id for the specified task id. The component id maps
    // * to a component id specified for a Spout or Bolt in the topology definition.
    // *
    // * @param taskId the task id
    // * @return the component id for the input task id
    // */
    //public string getComponentId(int taskId) {
    //    if(taskId==Constants.SYSTEM_TASK_ID) {
    //        return Constants.SYSTEM_COMPONENT_ID;
    //    } else {
    //        return _taskToComponent.get(taskId);
    //    }
    //}

    ///**
    // * Gets the set of streams declared for the specified component.
    // */
    //public Set<string> getComponentStreams(string componentId) {
    //    return getComponentCommon(componentId).get_streams().keySet();
    //}

    ///**
    // * Gets the task ids allocated for the given component id. The task ids are
    // * always returned in ascending order.
    // */
    //public List<int> getComponentTasks(string componentId) {
    //    List<int> ret = _componentToTasks.get(componentId);
    //    if(ret==null) return new ArrayList<int>();
    //    else return new ArrayList<int>(ret);
    //}

    ///**
    // * Gets the declared output fields for the specified component/stream.
    // */
    //public Fields getComponentOutputFields(string componentId, string streamId) {
    //    Fields ret = _componentToStreamToFields.get(componentId).get(streamId);
    //    if(ret==null) {
    //        throw new IllegalArgumentException("No output fields defined for component:stream " + componentId + ":" + streamId);
    //    }
    //    return ret;
    //}

    ///**
    // * Gets the declared output fields for the specified global stream id.
    // */
    //public Fields getComponentOutputFields(GlobalStreamId id) {
    //    return getComponentOutputFields(id.get_componentId(), id.get_streamId());
    //}    
    
    ///**
    // * Gets the declared inputs to the specified component.
    // *
    // * @return A map from subscribed component/stream to the grouping subscribed with.
    // */
    //public Map<GlobalStreamId, Grouping> getSources(string componentId) {
    //    return getComponentCommon(componentId).get_inputs();
    //}

    ///**
    // * Gets information about who is consuming the outputs of the specified component,
    // * and how.
    // *
    // * @return Map from stream id to component id to the Grouping used.
    // */
    //public Map<string, Map<string, Grouping>> getTargets(string componentId) {
    //    Map<string, Map<string, Grouping>> ret = new HashMap<string, Map<string, Grouping>>();
    //    for(string otherComponentId: getComponentIds()) {
    //        Map<GlobalStreamId, Grouping> inputs = getComponentCommon(otherComponentId).get_inputs();
    //        for(GlobalStreamId id: inputs.keySet()) {
    //            if(id.get_componentId().equals(componentId)) {
    //                Map<string, Grouping> curr = ret.get(id.get_streamId());
    //                if(curr==null) curr = new HashMap<string, Grouping>();
    //                curr.put(otherComponentId, inputs.get(id));
    //                ret.put(id.get_streamId(), curr);
    //            }
    //        }
    //    }
    //    return ret;
    //}

    //@Override
    //public string toJSONstring() {
    //    Map obj = new HashMap();
    //    obj.put("task->component", _taskToComponent);
    //    // TODO: jsonify StormTopology
    //    // at the minimum should send source info
    //    return JSONValue.toJSONstring(obj);
    //}

    ///**
    // * Gets a map from task id to component id.
    // */
    //public Map<int, string> getTaskToComponent() {
    //    return _taskToComponent;
    //}
    
    ///**
    // * Gets a list of all component ids in this topology
    // */
    //public Set<string> getComponentIds() {
    //    return ThriftTopologyUtils.getComponentIds(getRawTopology());
    //}

    //public ComponentCommon getComponentCommon(string componentId) {
    //    return ThriftTopologyUtils.getComponentCommon(getRawTopology(), componentId);
    //}
    
    //public int maxTopologyMessageTimeout() {
    //    int max = Utils.getInt(_stormConf.get(Config.TOPOLOGY_MESSAGE_TIMEOUT_SECS));
    //    for(string spout: getRawTopology().get_spouts().keySet()) {
    //        ComponentCommon common = getComponentCommon(spout);
    //        string jsonConf = common.get_json_conf();
    //        if(jsonConf!=null) {
    //            Map conf = (Map) JSONValue.parse(jsonConf);
    //            Object comp = conf.get(Config.TOPOLOGY_MESSAGE_TIMEOUT_SECS);
    //            if(comp!=null) {
    //                max = Math.max(Utils.getInt(comp), max);
    //            }
    //        }
    //    }
    //    return max;
    //}
}