using System.Threading.Tasks;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Services
{
    public interface IRequestService
    {
        /// <summary>
        /// Executes the provided ApiRequest and returns a ApiResponse immedeately on the thread pool
        /// </summary>
        /// <typeparam name="T">The return type of the response</typeparam>
        /// <param name="request">The Box Request to execute</param>
        /// <returns></returns>
        Task<IApiResponse<T>> ToResponseAsync<T>(IApiRequest request)
            where T : class;

        /// <summary>
        /// Queues the ApiRequest and executes it as threads become available, returning a ApiResponse
        /// </summary>
        /// <typeparam name="T">The return type of the response</typeparam>
        /// <param name="request">The Box Request to execute</param>
        /// <returns></returns>
        Task<IApiResponse<T>> EnqueueAsync<T>(IApiRequest request)
            where T : class;
    }
}
