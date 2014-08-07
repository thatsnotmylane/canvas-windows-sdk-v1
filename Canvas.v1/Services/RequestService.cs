using System.Threading.Tasks;
using Canvas.v1.Request;
using Canvas.v1.Wrappers.Contracts;
using Nito.AsyncEx;

namespace Canvas.v1.Services
{
    public class RequestService : IRequestService
    {
        private const int NumberOfThreads = 2;
        private IRequestHandler _handler;

        // Used to limit the number of requests that go out
        AsyncSemaphore _throttler = new AsyncSemaphore(NumberOfThreads); 

        /// <summary>
        /// Instantiates a new RequestService with the provided handler
        /// </summary>
        /// <param name="handler"></param>
        public RequestService(IRequestHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Executes the request according to the default TaskScheduler
        /// This will allow for concurrent requests and is managed by the thread pool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IBoxResponse<T>> ToResponseAsync<T>(IBoxRequest request)
            where T : class
        {
            return await _handler.ExecuteAsync<T>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the request but limits the number of threads that can be used 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IBoxResponse<T>> EnqueueAsync<T>(IBoxRequest request)
            where T : class
        {
            await _throttler.WaitAsync().ConfigureAwait(false);

            try
            {
                return await _handler.ExecuteAsync<T>(request).ConfigureAwait(false);
            }
            finally
            {
                _throttler.Release();
            }
        }
    }
}
