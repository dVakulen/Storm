using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Core.Abstract
{
    public interface IStormTuple
    {

        /**
         * Returns the global stream id (component + stream) of this tuple.
         */
        // public GlobalStreamId getSourceGlobalStreamid();

        /**
         * Gets the id of the component that created this tuple.
         */
        string getSourceComponent();

        /**
         * Gets the id of the task that created this tuple.
         */
        int getSourceTask();

        /**
         * Gets the id of the stream that this tuple was emitted to.
         */
        string getSourceStreamId();

        /**
         * Gets the message id that associated with this tuple.
         */
        // MessageId getMessageId();
    }
}
