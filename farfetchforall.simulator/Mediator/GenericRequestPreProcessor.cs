using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace FarfetchForAll.Simulator.Mediator
{
    public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
          //  System.Console.WriteLine("- Starting Up");

            return Task.FromResult(1);
        }
    }
}
