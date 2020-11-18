namespace FarfetchForAll.Simulator.Scenario
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Shares;
    using FarfetchForAll.Simulator.Taxes;

    public class SimulationScenario
    {
        private readonly ITaxFileBuilder taxCalculator;

        public SimulationScenario(ITaxFileBuilder taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }

        public virtual ScenarioResult Run(
            FamilyAggregate taxPayer,
            List<ShareMvt> shareMovements)
        {
            this.taxCalculator
                .With(taxPayer);

            IEnumerable<YearResult> yearResults;

            if (shareMovements.Count > 0)
            {
                yearResults = shareMovements
                   .GroupBy(i => i.MovementYear)
                   .Select(i => YearResult(i.Key, i))
                   .ToList();
            }
            else
            {
                yearResults = new List<YearResult>()
                {
                    new YearResult{ TaxResult = this.taxCalculator.With(shareMovements).Build(), Year = DateTime.Now.Year }
                };
            }

            return new ScenarioResult
            {
                Name = this.GetType().Name,
                Results = yearResults
            };
        }

        protected YearResult YearResult(int year, IEnumerable<ShareMvt> shareMovements)
        {
            var baseTaxResult = this.taxCalculator
                .With(new List<ShareMvt>())
                .Build();

            var taxResult = this.taxCalculator
                .With(shareMovements)
                .Build();

            var taxDiff = baseTaxResult.TaxSettling - taxResult.TaxSettling;

            var sharesProfit = shareMovements
                .Where(i => i.MovementType == ShareMovementType.Sell)
                .Sum(i => i.Income) - taxDiff;

            return new YearResult
            {
                Year = year,
                TaxResult = taxResult,
                SharesProfit = sharesProfit
            };
        }
    }
}