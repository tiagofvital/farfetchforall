using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace FarfetchForAll.Simulator.Mediator
{
    public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
          //  Console.WriteLine("- All Done");

            return Task.FromResult(1);
        }
    }
}
