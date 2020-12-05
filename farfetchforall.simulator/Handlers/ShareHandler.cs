namespace FarfetchForAll.Simulator.RequestHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FarfetchForAll.Simulator.Commands;
    using FarfetchForAll.Simulator.DomainEvents;
    using FarfetchForAll.Simulator.Events;
    using FarfetchForAll.Simulator.Queries;
    using FarfetchForAll.Simulator.Repositories;
    using FarfetchForAll.Simulator.Shares;
    using MediatR;

    public class ShareHandler : IRequestHandler<VestShareCommand>, IRequestHandler<SellShareCommand>, IRequestHandler<GetShares, GetSharesResult>, IRequestHandler<BuildSharesCommand>
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
            var evts = this.Vest(vestShareCommand.Amount,
                vestShareCommand.ExerciseCost,
                vestShareCommand.ShareValue,
                vestShareCommand.Year,
                vestShareCommand.TransactionId);

            evts.Select(async evt => await this.mediator.Publish(evt))
                .ToList();

            return await Unit.Task;
        }

        public async Task<Unit> Handle(SellShareCommand cmd, CancellationToken cancellationToken)
        {
            var evts = this.Sell(cmd.Amount, cmd.ShareValue, cmd.Year, cmd.TransactionId);

            evts.ForEach(async evt => await this.mediator.Publish(evt));

            return await Unit.Task;
        }

        public async Task<GetSharesResult> Handle(GetShares cmd, CancellationToken cancellationToken)
        {
            var spec = new ShareStateSpecification(cmd.State);

            var shares = this.sharesRepository
                .Get(spec)
                .ToList();

            var result = new GetSharesResult
            {
                Shares = shares
            };

            return await Task.FromResult(result);
        }

        public async Task<Unit> Handle(BuildSharesCommand cmd, CancellationToken cancellationToken)
        {
            this.sharesRepository.Clear();

            foreach (var mvt in cmd.Movements)
            {
                if (mvt.MovementType == ShareMovementType.Acquisition)
                {
                    this.Vest(1, mvt.ExerciseCost, mvt.ShareValue, mvt.MovementYear, mvt.TransactionId)
                        .ToList();
                }
                else
                {
                    this.Sell(1, mvt.ShareValue, mvt.MovementYear, mvt.TransactionId);
                }
            }

            return await Unit.Task;
        }

        private IEnumerable<ShareVested> Vest(int amount, float exerciseCost, float shareValue, int year, string transactionId)
        {
            var shares = new ShareBuilder()
                .WithExerciseCost(exerciseCost)
                .WithShareValue(shareValue)
                .WithYear(year)
                .Build(amount)
                .ToList();

            foreach (var share in shares)
            {
                this.sharesRepository.Add(share);

                yield return share.Vest(transactionId);
            }
        }

        private List<ShareSold> Sell(int amount, float shareValue, int year, string transactionId)
        {
            var spec = new ShareStateSpecification(Share.ShareState.Vested);

            return this.sharesRepository
                .Get(spec)
                .Take(amount)
                .Select(share => share.Sell(shareValue, year, transactionId))
                .ToList();
        }
    }
}