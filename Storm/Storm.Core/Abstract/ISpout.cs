using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Core.Abstract
{
    public interface ISpout : ISerializable
    { /**
     * Called when a task for this component is initialized within a worker on the cluster.
     * It provides the spout with the environment in which the spout executes.
     *
     * <p>This includes the:</p>
     *
     * @param conf The Storm configuration for this spout. This is the configuration provided to the topology merged in with cluster configuration on this machine.
     * @param context This object can be used to get information about this task's place within the topology, including the task id and component id of this task, input and output information, etc.
     * @param collector The collector is used to emit tuples from this spout. Tuples can be emitted at any time, including the open and close methods. The collector is thread-safe and should be saved as an instance variable of this spout object.
     */
        void Open(); //Map conf, TopologyContext context, SpoutOutputCollector collector
        /**
   * Called when an ISpout is going to be shutdown. There is no guarentee that close
   * will be called, because the supervisor kill -9's worker processes on the cluster.
   *
   * <p>The one context where close is guaranteed to be called is a topology is
   * killed when running Storm in local mode.</p>
   */
        void Close();

        /**
         * Called when a spout has been activated out of a deactivated mode.
         * nextTuple will be called on this spout soon. A spout can become activated
         * after having been deactivated when the topology is manipulated using the 
         * `storm` client. 
         */
        void Activate();
        /**
   * Called when a spout has been deactivated. nextTuple will not be called while
   * a spout is deactivated. The spout may or may not be reactivated in the future.
   */
        void Deactivate();

        /**
         * When this method is called, Storm is requesting that the Spout emit tuples to the 
         * output collector. This method should be non-blocking, so if the Spout has no tuples
         * to emit, this method should return. nextTuple, ack, and fail are all called in a tight
         * loop in a single thread in the spout task. When there are no tuples to emit, it is courteous
         * to have nextTuple sleep for a short amount of time (like a single millisecond)
         * so as not to waste too much CPU.
         */
        void NextTuple();

        /**
         * Storm has determined that the tuple emitted by this spout with the msgId identifier
         * has been fully processed. Typically, an implementation of this method will take that
         * message off the queue and prevent it from being replayed.
         */
        void Ack(object msgId);

        /**
         * The tuple emitted by this spout with the msgId identifier has failed to be
         * fully processed. Typically, an implementation of this method will put that
         * message back on the queue to be replayed at a later time.
         */
        void Fail(object msgId);
    }
}
