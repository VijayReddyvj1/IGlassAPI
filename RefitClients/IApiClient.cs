using System.Threading.Tasks;
using Refit;

namespace IGlassAPI.RefitClients
{
    public interface IApiClient
    {
        [Post("/api/payload")]
        Task PostPayloadAsync([Body] object payload);
    }
}