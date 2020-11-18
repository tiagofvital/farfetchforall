namespace FarfetchForAll.Simulator.RequestHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FarfetchForAll.Simulator.Events;
    using FarfetchForAll.Simulator.Repositories;
    using FarfetchForAll.Simulator.Shares;
    using MediatR;

    public class ShareHandler : IRequestHandler<VestShareCommand>, IRequestHandler<SellShareCommand>
    {
        private readonly IMediator mediator;
        private readonly SharesRepository sharesRepository;

        public ShareHandler(
            IMediator mediator,
            SharesRepository sharesRepository)
        {
            this.mediator = mediator;
            this.sharesRepository = sharesRepository;
        }

        public async Task<Unit> Handle(VestShareCommand vestShareCommand, CancellationToken cancellationToken)
        {
            Share.From(vestShareCommand)
                .ToList()
                .ForEach(share =>
                {
                    var evt = share.Vest(vestShareCommand.TransactionId);

                    this.sharesRepository.Add(share);

                    this.mediator.Publish(evt);
                });

            return await Unit.Task;
        }

        public async Task<Unit> Handle(SellShareCommand cmd, CancellationToken cancellationToken)
        {
            var spec = new VestedSharesSpecification();

            this.sharesRepository
                .Get(spec)
                .Take(cmd.Amount)
                .ToList()
                .ForEach(share =>
                {
                    var evt = share.Sell(cmd.ShareValue, cmd.Year, cmd.TransactionId);

                    this.mediator.Publish(evt);
                });

            return await Unit.Task;
        }
    }
}