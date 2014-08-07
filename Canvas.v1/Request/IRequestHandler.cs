using System.Threading.Tasks;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Request
{
    public interface IRequestHandler
    {

        /// <summary>
        /// Executes the BoxRequest
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="request">The box request to execute</param>
        /// <returns>A BoxResponse</returns>
        Task<IBoxResponse<T>> ExecuteAsync<T>(IBoxRequest request)
            where T : class;
    }
}
