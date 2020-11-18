namespace FarfetchForAll.Simulator.Handlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FarfetchForAll.Simulator.Commands;
    using FarfetchForAll.Simulator.DomainEvents;
    using FarfetchForAll.Simulator.Queries;
    using FarfetchForAll.Simulator.Repositories;
    using FarfetchForAll.Simulator.Shares;
    using MediatR;

    public class ShareMovementsHandler : INotificationHandler<ShareVested>, INotificationHandler<ShareSold>, IRequestHandler<GetShareMovements, GetShareMovementsResult>, IRequestHandler<ClearMovementsCommand>
    {
        private readonly ShareMovementsRepository shareMovementsRepository;

        public ShareMovementsHandler(ShareMovementsRepository shareMovementsRepository)
        {
            this.shareMovementsRepository = shareMovementsRepository;
        }

        public Task Handle(ShareVested request, CancellationToken cancellationToken)
        {
            ShareMvt mvt = ShareMvt.CreateFrom(request);

            this.shareMovementsRepository.Add(mvt);

            return Unit.Task;
        }

        public Task Handle(ShareSold notification, CancellationToken cancellationToken)
        {
            ShareMvt mvt = ShareMvt.CreateFrom(notification);

            this.shareMovementsRepository.Add(mvt);

            return Unit.Task;
        }

        public async Task<GetShareMovementsResult> Handle(GetShareMovements request, CancellationToken cancellationToken)
        {
            var result = this.shareMovementsRepository.Get().ToList();

            return await Task.FromResult(new GetShareMovementsResult
            {
                Movements = result
            });
        }

        public Task<Unit> Handle(ClearMovementsCommand request, CancellationToken cancellationToken)
        {
            this.shareMovementsRepository.Clear(request.TransactionId);

            return Unit.Task;
        }
    }
}