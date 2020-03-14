namespace FarfetchForAll.Simulator.DomainEvents
{
    using FarfetchForAll.Simulator.Shares;
    using MediatR;

    public class ShareMovementCreated : INotification
    {
        public ShareMvt Movement { get; set; }
    }
}