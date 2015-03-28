using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Core.Abstract
{
    public interface IBasicBolt : IComponent
    {
        // void prepare(Map stormConf, TopologyContext context);
        /**
         * Process the input tuple and optionally emit new tuples based on the input tuple.
         * 
         * All acking is managed for you. Throw a FailedException if you want to fail the tuple.
         */

        /**
         * Process a single tuple of input. The Tuple object contains metadata on it
         * about which component/stream/task it came from. The values of the Tuple can
         * be accessed using Tuple#getValue. The IBolt does not have to process the Tuple
         * immediately. It is perfectly fine to hang onto a tuple and process it later
         * (for instance, to do an aggregation or join).
         *
         * <p>Tuples should be emitted using the OutputCollector provided through the prepare method.
         * It is required that all input tuples are acked or failed at some point using the OutputCollector.
         * Otherwise, Storm will be unable to determine when tuples coming off the spouts
         * have been completed.</p>
         *
         * <p>For the common case of acking an input tuple at the end of the execute method,
         * see IBasicBolt which automates this.</p>
         * 
         * @param input The input tuple to be processed.
         */
        void Execute(string input); //IStormTuple input, BasicOutputCollector collector
        string Execute1(string input);
        /**
  * Called when an IBolt is going to be shutdown. There is no guarentee that cleanup
  * will be called, because the supervisor kill -9's worker processes on the cluster.
  *
  * <p>The one context where cleanup is guaranteed to be called is when a topology
  * is killed when running Storm in local mode.</p>
  */
        void Cleanup();
    }
}
