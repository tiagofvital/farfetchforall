using System;
using MediatR.Pipeline;

namespace FarfetchForAll.Simulator.Mediator
{
    public class GenericExceptionAction<TRequest> : RequestExceptionAction<TRequest>
    {
        protected override void Execute(TRequest request, Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}
