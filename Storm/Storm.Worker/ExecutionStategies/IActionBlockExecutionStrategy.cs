using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Storm.Worker.ExecutionStategies
{
    class IActionBlockExecutionStrategy
    {
        void Execute()
        {
            var workerBlock = new ActionBlock<int>(
                // Simulate work by suspending the current thread.
         millisecondsTimeout => Thread.Sleep(millisecondsTimeout),
                // Specify a maximum degree of parallelism. 
         new ExecutionDataflowBlockOptions
         {
             MaxDegreeOfParallelism = 4
         });

            // Compute the time that it takes for several messages to  
            // flow through the dataflow block.

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 5; i++)
            {
                workerBlock.Post(1000);
            }
            workerBlock.Complete();

            // Wait for all messages to propagate through the network.
            workerBlock.Completion.Wait();

            // Stop the timer and return the elapsed number of milliseconds.
            stopwatch.Stop();
            // stopwatch.Elapsed;
        }
    }
}
