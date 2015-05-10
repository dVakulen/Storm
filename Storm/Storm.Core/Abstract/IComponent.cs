using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Core.Abstract
{
    public interface IComponent : ISerializable
    {

        /**
         * Declare the output schema for all the streams of this topology.
         *
         * @param declarer this is used to declare output stream ids, output fields, and whether or not each output stream is a direct stream
         */
        //void declareOutputFields(OutputFieldsDeclarer declarer);

        /**
         * Declare configuration specific to this component. Only a subset of the "topology.*" configs can
         * be overridden. The component configuration can be further overridden when constructing the 
         * topology using {@link TopologyBuilder}
         *
         */
        //  Map<string, Object> getComponentConfiguration();

    }
}
