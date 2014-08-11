using System.Threading.Tasks;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Request
{
    public interface IRequestHandler
    {

        /// <summary>
        /// Executes the ApiRequest
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="request">The box request to execute</param>
        /// <returns>A ApiResponse</returns>
        Task<IApiResponse<T>> ExecuteAsync<T>(IApiRequest request)
            where T : class;
    }
}
