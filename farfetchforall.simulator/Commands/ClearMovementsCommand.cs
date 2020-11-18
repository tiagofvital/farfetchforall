namespace FarfetchForAll.Simulator.Commands
{
    using MediatR;

    public class ClearMovementsCommand : IRequest
    {
        public string TransactionId { get; set; }
    }
}