using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanSample.App.Common.BaseClasses
{
    public abstract class ActionCommand<TRequest> : IRequestHandler<TRequest>
        where TRequest : IRequest
    {
        private readonly ILogger<IRequestHandler<TRequest>> logger;
        public ActionCommand(ILogger<IRequestHandler<TRequest>> logger = default)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Wrapper method to orcherstrate all important steps of processing
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        async Task<Unit> IRequestHandler<TRequest, Unit>.Handle(TRequest request, CancellationToken cancellationToken = default)
        {
            logger.LogDebug($"ActionCommand {nameof(TRequest)} Handle {request} started");

            try
            {
                await CheckPreconditions(request);
                logger.LogDebug($"ActionCommand {nameof(TRequest)} CheckPreconditions check succeeded");

                CheckIfCancelled(cancellationToken);

                await Authorize(request);
                logger.LogDebug($"ActionCommand {nameof(TRequest)} Authorization succeeded");

                await Execute(request);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"ActionCommand {nameof(TRequest)} Handle {request} failed");
                throw;
            }

            logger.LogDebug($"ActionCommand {nameof(TRequest)} Handle {request} finished");
            return Unit.Value;
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
        protected abstract Task Execute(TRequest request);

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
                logger.LogWarning("ActionCommand request cancellation");
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}
