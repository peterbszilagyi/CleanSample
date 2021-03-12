using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanSample.App.Common.BaseClasses
{
    public abstract class QueryCommand<TRequest, TResponseModel> :
      IRequestHandler<TRequest, TResponseModel> where TRequest : IRequest<TResponseModel>
    {
        private readonly ILogger<IRequestHandler<TRequest, TResponseModel>> logger;
        public QueryCommand(ILogger<IRequestHandler<TRequest, TResponseModel>> logger = default)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Wrapper method to orcherstrate all important steps of processing
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>ResponseModel</returns>
        public async Task<TResponseModel> Handle(TRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug($"QueryCommand {nameof(TRequest)} Handle {request} started");
            var result = default(TResponseModel);
            try
            {
                CheckPreconditions(request);
                logger.LogDebug($"QueryCommand {nameof(TRequest)} CheckPreconditions check succeeded");

                CheckIfCancelled(cancellationToken);

                await Authorize(request);
                logger.LogDebug($"QueryCommand {nameof(TRequest)} Authorization succeeded");

                result = await Execute(request);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"QueryCommand {nameof(TRequest)} Handle {request} failed");
                throw;
            }
            logger.LogDebug($"QueryCommand {nameof(TRequest)} Handle {request} finished");
            return result;
        }
        /// <summary>
        /// Check all preconditions and throw exception if failed
        /// </summary>
        /// <param name="request"></param>
        public virtual async Task CheckPreconditions(TRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
        }
        /// <summary>
        /// Checks if the token for cancellation
        /// Throws a System.OperationCanceledException if this token has had cancellation
        /// Exceptions:
        ///   T:System.OperationCanceledException:
        ///    The token has had cancellation requested
        /// <param name="cancellationToken"></param>
        private void CheckIfCancelled(CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                logger.LogWarning("QueryCommand request cancellation");
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
        /// <summary>
        /// Perform all authorization and throw UnauthorizedAccessException if failed
        /// </summary>
        /// <param name="request"></param>
        protected abstract Task Authorize(TRequest request);

        /// <summary>
        /// Perform all steps to process the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>ResponseModel</returns>
        protected abstract Task<TResponseModel> Execute(TRequest request);
    }

}
