using System.Threading.Tasks;

namespace IGlassAPI.Queue
{
    public interface IQueueProvider
    {
        Task EnqueueAsync(string payload);
    }
}