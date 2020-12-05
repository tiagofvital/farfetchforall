namespace FarfetchForAll.Simulator.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Commands;
    using FarfetchForAll.Simulator.Events;
    using FarfetchForAll.Simulator.Queries;
    using FarfetchForAll.Simulator.Shares;
    using MediatR;

    public class ShareControllers
    {
        private readonly IMediator mediator;

        public ShareControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void VestShares(int amount, float value, float exerciseCost, int year)
        {
            this.mediator.Send(new VestShareCommand()
            {
                Amount = amount,
                ShareValue = value,
                ExerciseCost = exerciseCost,
                Year = year
            });
        }

        public void Sell(int amount, float value, int year)
        {
            this.mediator.Send(new SellShareCommand()
            {
                Amount = amount,
                ShareValue = value,
                Year = year
            });
        }

        public IEnumerable<ShareMvt> GetMovements()
        {
            var result = this.mediator.Send(new GetShareMovements())
                 .GetAwaiter()
                 .GetResult();

            return result.Movements;
        }

        public void Clear()
        {
            this.ClearMovements();

            this.BuildFrom(new List<ShareMvt>());
        }

        public void Undo()
        {
            var shareMvts = this.GetMovements();
            var lastMvt = shareMvts.Last();

            if (lastMvt == null)
            {
                return;
            }

            var mvts = shareMvts.Where(i => i.TransactionId != lastMvt.TransactionId).ToList();

            this.ClearMovements(lastMvt.TransactionId);
            this.BuildFrom(mvts);
        }

        public void BuildFrom(IEnumerable<Shares.ShareMvt> mvts)
        {
            var rebuildSharesCommand = new BuildSharesCommand()
            {
                Movements = mvts
            };

            this.mediator.Send(rebuildSharesCommand);
        }

        private void ClearMovements(string transactionId = null)
        {
            var clearMovementsCommand = new ClearMovementsCommand()
            {
                TransactionId = transactionId
            };

            this.mediator.Send(clearMovementsCommand);
        }
    }
}