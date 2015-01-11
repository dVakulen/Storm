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
        void Execute(string input); //IStormTuple input, BasicOutputCollector collector
        string Execute1(string input);
        void Cleanup();
    }
}
