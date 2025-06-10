
using Refit;
using System.Threading.Tasks;

namespace RefitQueueWorkerHybrid.RefitClients
{
    public interface IApiClient
    {
        [Post("/api/external")]
        Task PostPayloadAsync([Body] object payload);
    }
}
