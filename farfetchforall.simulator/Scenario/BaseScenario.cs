namespace FarfetchForAll.Simulator.Scenario
{
    using FarfetchForAll.Simulator.Taxes;

    public class BaseScenario : SimulationScenario
    {
        private readonly ITaxCalculator taxCalculator;

        public BaseScenario(ITaxCalculator taxCalculator)
            : base(taxCalculator)
        {
        }

        protected override SharesInfo BuildSharesInfo()
        {
            return new SharesInfoBuilder()
                   .WithStockOptions(0)
                   .Build()
                   .Buy(0, 0, 2019);
        }
    }
}