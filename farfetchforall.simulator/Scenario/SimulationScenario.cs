namespace FarfetchForAll.Simulator.Scenario
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Queries;
    using FarfetchForAll.Simulator.Shares;
    using FarfetchForAll.Simulator.Taxes;
    using MediatR;

    public class SimulationScenario
    {
        private readonly ITaxFileBuilder taxCalculator;
        private readonly IMediator mediator;

        public SimulationScenario(ITaxFileBuilder taxCalculator, IMediator mediator)
        {
            this.taxCalculator = taxCalculator;
            this.mediator = mediator;
        }

        public virtual ScenarioResult Run()
        {
            var shareMvts = this.GetShareMovements();
            var aggregate = this.GetFamilyAggregate();
            var vestedShares = this.GetVestedShares();

            this.taxCalculator
                .With(aggregate);

            IEnumerable<YearResult> yearResults;

            yearResults = YearResults(shareMvts);

            var result = new ScenarioResult
            {
                Name = this.GetType().Name,
                Aggregate = aggregate,
                Results = yearResults,
                Shares = vestedShares,
                TotalProfit = yearResults.Sum(i => i.SharesProfit)
            };

            return result;
        }

        private IEnumerable<ShareMvt> GetShareMovements()
        {
            var result = this.mediator.Send(new GetShareMovements())
                                     .GetAwaiter()
                                     .GetResult();

            return result.Movements;
        }

        private FamilyAggregate GetFamilyAggregate()
        {
            var request = new GetFamilyAggregateInfo();

            return this.mediator.Send(request)
            .GetAwaiter()
            .GetResult();
        }

        private IEnumerable<Share> GetVestedShares()
        {
            var shareResult = this.mediator.Send(new GetShares() { State = Share.ShareState.Vested })
                                     .GetAwaiter()
                                     .GetResult();

            return shareResult.Shares;
        }

        private IEnumerable<YearResult> YearResults(IEnumerable<ShareMvt> shareMvts)
        {
            IEnumerable<YearResult> yearResults;
            if (shareMvts.Count() > 0)
            {
                yearResults = shareMvts
                   .GroupBy(i => i.MovementYear)
                   .Select(i => YearResult(i.Key, i))
                   .ToList();
            }
            else
            {
                yearResults = new List<YearResult>()
                {
                    new YearResult{ TaxResult = this.taxCalculator.With(shareMvts).Build(), Year = DateTime.Now.Year }
                };
            }

            return yearResults;
        }

        protected YearResult YearResult(int year, IEnumerable<ShareMvt> shareMovements)
        {
            var taxResult = this.taxCalculator
                .With(shareMovements)
                .Build();

            var sharesProfit = shareMovements
                .Where(i => i.MovementType == ShareMovementType.Sell)
                .Sum(i => i.Income) + taxResult.TaxSettling;

            return new YearResult
            {
                Year = year,
                TaxResult = taxResult,
                SharesProfit = sharesProfit,
                Movements = shareMovements
            };
        }
    }
}