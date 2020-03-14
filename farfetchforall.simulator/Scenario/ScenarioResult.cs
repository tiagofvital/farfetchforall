namespace FarfetchForAll.Simulator.Scenario
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Taxes;

    public class ScenarioResult
    {
        public string Name { get; set; }

        public IEnumerable<YearResult> Results { get; set; }
    }

    public class YearResult
    {
        public TaxResult TaxResult { get; set; }

        public float SharesProfit { get; internal set; }

        public int Year { get; set; }
    }
}