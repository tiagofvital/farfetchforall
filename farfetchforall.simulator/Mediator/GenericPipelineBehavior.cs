using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace FarfetchForAll.Simulator.Mediator
{
    public class GenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Console.WriteLine("-- Handling Request");
            var response = await next();
            //Console.WriteLine("-- Finished Request");

            return response;
        }
    }
}
