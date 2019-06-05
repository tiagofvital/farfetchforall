namespace FarfetchForAll.Simulator.Scenario
{
    using FarfetchForAll.Simulator.Taxes;

    public class BuyAndSellCoverCostsScenario : SimulationScenario
    {
        public BuyAndSellCoverCostsScenario(ITaxCalculator taxCalculator)
            : base(taxCalculator)
        {
        }

        protected override SharesInfo BuildSharesInfo()
        {
            var sharesInfo = new SharesInfoBuilder()
               .WithStockOptions(3000)
               .WithExercisePrice(6.4F)
               .Build();

            var acquisitionValue = 15.0F;
            var sellValue = 40F;

            sharesInfo.Buy(3000, acquisitionValue, 2019);

            var yearResult = base.YearResult(base.TaxPayer, 2019, sharesInfo.SharesMovements);

            var taxAndExerciseCosts = yearResult.TaxResult.TaxToPay + sharesInfo.ExerciseCost(3000);

            var sharesToCoverCosts = sharesInfo.SharesToCover(taxAndExerciseCosts, acquisitionValue);
            var remainingShares = sharesInfo.StockOptions.Amount - sharesToCoverCosts;

            sharesInfo.Sell(sharesToCoverCosts, acquisitionValue, acquisitionValue, 2019);
            sharesInfo.Sell(remainingShares, acquisitionValue, sellValue, 2020);

            return sharesInfo;
        }
    }
}