namespace FarfetchForAll.Simulator.Scenario
{
    using FarfetchForAll.Simulator.Taxes;

    public class BuyAndSellScenario : SimulationScenario
    {
        private readonly ITaxCalculator taxCalculator;
        private readonly BaseScenario baseScenario;

        public BuyAndSellScenario(ITaxCalculator taxCalculator) : base(taxCalculator)
        {
        }

        protected override SharesInfo BuildSharesInfo()
        {
            return new SharesInfoBuilder()
             .WithStockOptions(3000)
             .WithExercisePrice(6.4F)
             .Build()
             .Buy(3000, 40, 2019)
             .Sell(3000, 40, 40, 2019);
        }
    }
}