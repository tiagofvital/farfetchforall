namespace FarfetchForAll.Simulator.Scenario
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Shares;
    using FarfetchForAll.Simulator.Taxes;

    public class SimulationScenario
    {
        private readonly ITaxCalculator taxCalculator;

        public SimulationScenario(ITaxCalculator taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }

        protected FamilyAggregate TaxPayer { get; private set; }

        public virtual ScenarioResult Run(
            FamilyAggregate taxPayer,
            List<ShareMvt> shareMovements)
        {
            this.TaxPayer = taxPayer;

            IEnumerable<YearResult> yearResults;

            if (shareMovements.Count > 0)
            {
                yearResults = shareMovements
                   .GroupBy(i => i.MovementYear)
                   .Select(i => YearResult(taxPayer, i.Key, i))
                   .ToList();
            }
            else
            {
                yearResults = new List<YearResult>()
                {
                    new YearResult{ TaxResult = this.taxCalculator.Run(taxPayer, shareMovements), Year = DateTime.Now.Year }
                };
            }

            return new ScenarioResult
            {
                Name = this.GetType().Name,
                Results = yearResults
            };
        }

        protected YearResult YearResult(FamilyAggregate taxPayer, int year, IEnumerable<ShareMvt> shareMovements)
        {
            var baseTaxResult = this.taxCalculator.Run(taxPayer, new List<ShareMvt>());

            var taxResult = this.taxCalculator.Run(taxPayer, shareMovements);

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