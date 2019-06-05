namespace FarfetchForAll.Simulator.Scenario
{
    using FarfetchForAll.Simulator.Taxes;

    public class BuyAndSellLaterScenario : SimulationScenario
    {
        private readonly ITaxCalculator taxCalculator;
        private readonly BaseScenario baseScenario;

        public BuyAndSellLaterScenario(ITaxCalculator taxCalculator) : base(taxCalculator)
        {
        }

        protected override SharesInfo BuildSharesInfo()
        {
            return new SharesInfoBuilder()
             .WithStockOptions(1500)
             .WithExercisePrice(6.4F)
             .Build()
             .Buy(1500, 15, 2019)
             .Sell(1500, 15, 35, 2020);
        }
    }
}