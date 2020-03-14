using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace FarfetchForAll.Simulator.Mediator
{
    public class AsyncGenericExceptionHandler<TRequest, TResponse> : AsyncRequestExceptionHandler<TRequest, TResponse>         
    {
        protected override Task Handle(
            TRequest request, 
            Exception exception, 
            RequestExceptionHandlerState<TResponse> state, 
            CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());

            state.SetHandled();

            return Task.FromResult(1);
        }
    }

    public class GenericExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException> where TException : Exception
    {
        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());

            state.SetHandled();

            return Task.FromResult(1);
        }
    }
}
