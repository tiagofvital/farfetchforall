namespace FarfetchForAll.Console
{
    using FarfetchForAll.Simulator.Scenario;
    using FarfetchForAll.Simulator.Taxes.Pt;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var aggregateInfo = new TaxPayerBuilder()
                .WithAnualGain(5000000F)
                .WithFamilyCoeficient(2)
                .WithDeductionsToTax(10)
                .WithSpecificDeductions(2000000F)
                .WithTaxPayed(8141F)
                .Build();

            //cenários:

            var baseScenario = new BaseScenario(new IRSCalculator());

            var baseResult = baseScenario.Run(aggregateInfo);
            baseResult.Display();

            var buyAndSellScenario = new BuyAndSellScenario(new IRSCalculator());
            var buyAndSellResult = buyAndSellScenario.Run(aggregateInfo);
            buyAndSellResult.Display();

            var buyAndSellLaterScenario = new BuyAndSellLaterScenario(new IRSCalculator());
            var buyAndSellLaterResult = buyAndSellLaterScenario.Run(aggregateInfo);
            buyAndSellLaterResult.Display();

            var buyAndSellCoverCostsScenario = new BuyAndSellCoverCostsScenario(new IRSCalculator());
            var buyAndSellCoverCostsResult = buyAndSellCoverCostsScenario.Run(aggregateInfo);
            buyAndSellCoverCostsResult.Display();

            System.Console.ReadLine();
        }
    }
}