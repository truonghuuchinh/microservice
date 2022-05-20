using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    public class UnhandledExceptionBehavior<TRequest, TRespone> : IPipelineBehavior<TRequest, TRespone>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TRespone> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TRespone> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: UnHandled Exception for Request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
