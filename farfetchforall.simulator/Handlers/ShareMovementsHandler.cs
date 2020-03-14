namespace FarfetchForAll.Simulator.Handlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FarfetchForAll.Simulator.Domain.Services;
    using FarfetchForAll.Simulator.DomainEvents;
    using FarfetchForAll.Simulator.Queries;
    using FarfetchForAll.Simulator.Repositories;
    using FarfetchForAll.Simulator.Shares;
    using MediatR;

    public class ShareMovementsHandler : INotificationHandler<ShareVested>, INotificationHandler<ShareSold>, IRequestHandler<GetShareMovements, GetShareMovementsResult>
    {
        private readonly ShareMovementsRepository shareMovementsRepository;
        private readonly ShareMovementDomainService shareMovementDomainService;

        public ShareMovementsHandler(
            ShareMovementsRepository shareMovementsRepository,
            ShareMovementDomainService shareMovementDomainService)
        {
            this.shareMovementsRepository = shareMovementsRepository;
            this.shareMovementDomainService = shareMovementDomainService;
        }

        public Task Handle(ShareVested request, CancellationToken cancellationToken)
        {
            ShareMvt mvt = this.shareMovementDomainService.CreateFrom(request);

            this.shareMovementsRepository.Add(mvt);

            return Unit.Task;
        }

        public Task Handle(ShareSold notification, CancellationToken cancellationToken)
        {
            ShareMvt mvt = this.shareMovementDomainService.CreateFrom(notification);

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
    }
}