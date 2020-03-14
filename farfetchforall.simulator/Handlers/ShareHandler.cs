namespace FarfetchForAll.Simulator.RequestHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FarfetchForAll.Simulator.Domain.Services;
    using FarfetchForAll.Simulator.Events;
    using FarfetchForAll.Simulator.Repositories;
    using MediatR;

    public class ShareHandler : IRequestHandler<VestShareCommand>, IRequestHandler<SellShareCommand>
    {
        private readonly IMediator mediator;
        private readonly SharesRepository sharesRepository;
        private readonly ShareDomainService shareDomainService;

        public ShareHandler(
            IMediator mediator,
            SharesRepository sharesRepository,
            ShareDomainService shareDomainService)
        {
            this.mediator = mediator;
            this.sharesRepository = sharesRepository;
            this.shareDomainService = shareDomainService;
        }

        public Task<Unit> Handle(VestShareCommand vestShareCommand, CancellationToken cancellationToken)
        {
            this.shareDomainService.From(vestShareCommand)
                .ToList()
                .ForEach(share =>
                {
                    var evt = this.shareDomainService.Vest(share);

                    this.sharesRepository.Add(share);

                    this.mediator.Publish(evt);
                });

            return Unit.Task;
        }

        public Task<Unit> Handle(SellShareCommand cmd, CancellationToken cancellationToken)
        {
            var spec = new VestedSharesSpecification();

            this.sharesRepository
                .Get(spec)
                .Take(cmd.Amount)
                .ToList()
                .ForEach(share =>
                {
                    var evt = this.shareDomainService.Sell(share, cmd.ShareValue, cmd.Year);

                    this.mediator.Publish(evt);
                });

            return Unit.Task;
        }
    }
}