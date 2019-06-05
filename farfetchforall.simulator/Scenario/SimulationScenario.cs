namespace FarfetchForAll.Simulator.Scenario
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Taxes;

    public abstract class SimulationScenario
    {
        private readonly ITaxCalculator taxCalculator;

        public SimulationScenario(ITaxCalculator taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }

        protected TaxPayer TaxPayer { get; private set; }

        public ScenarioResult Run(TaxPayer taxPayer)
        {
            this.TaxPayer = taxPayer;

            var sharesInfo = this.BuildSharesInfo();

            var yearMovements = sharesInfo.SharesMovements
               .GroupBy(i => i.Year)
               .Select(i => YearResult(taxPayer, i.Key, i))
               .ToList();

            return new ScenarioResult
            {
                Name = this.GetType().Name,
                SharesInfo = sharesInfo,
                Results = yearMovements
            };
        }

        protected abstract SharesInfo BuildSharesInfo();

        protected YearResult YearResult(TaxPayer taxPayer, int year, IEnumerable<ShareMovement> shareMovements)
        {
            var taxResult = this.taxCalculator.Run(taxPayer, shareMovements);

            var sharesProfit = shareMovements.Sum(i => i.SellIncome.GetValueOrDefault()) - taxResult.TaxToPay;

            return new YearResult
            {
                Year = year,
                TaxResult = taxResult,
                SharesProfit = sharesProfit
            };
        }
    }
}