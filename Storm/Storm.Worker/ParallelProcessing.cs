using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Worker
{
    class ParallelProcessing<T, TMapResult, TReduceResult>
    {
        private IEnumerable<T> Source;
        public BlockingCollection<TMapResult> MapResults;

        public BlockingCollection<TReduceResult> ReduceResults;
        public void Initialize(IEnumerable<T> source)
        {
            MapResults = new BlockingCollection<TMapResult>();
            Source = source;
            ReduceResults = new BlockingCollection<TReduceResult>();
        }
        public void Map(IEnumerable<T> source, Func<T, TMapResult> bodyAction)
        {
            Parallel.ForEach(Source, item =>
            {
                MapResults.Add(bodyAction(item));
            });
            MapResults.CompleteAdding();
        }
        public void Reduce(Func<TMapResult, TReduceResult> bodyAction)
        {
            Parallel.ForEach(MapResults.GetConsumingEnumerable(), item =>
            {
                ReduceResults.Add(bodyAction(item));
            });
            ReduceResults.CompleteAdding();
        }
    }
}
