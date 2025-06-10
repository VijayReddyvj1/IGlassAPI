
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace IGlassAPI.Queue
{
    public class InMemoryQueueProvider : IQueueProvider
    {
        public static readonly ConcurrentQueue<(string, string)> Queue = new();

        public Task EnqueueAsync(string message, string clientId)
        {
            Queue.Enqueue((message, clientId));
            return Task.CompletedTask;
        }
    }
}
