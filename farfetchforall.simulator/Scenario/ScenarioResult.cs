namespace FarfetchForAll.Simulator.Scenario
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Shares;
    using FarfetchForAll.Simulator.Taxes;

    public class ScenarioResult
    {
        public string Name { get; set; }

        public FamilyAggregate Aggregate { get; set; }

        public IEnumerable<YearResult> Results { get; set; } = new List<YearResult>();

        public IEnumerable<Share> Shares { get; set; }

        public float TotalProfit { get; internal set; }
    }

    public class YearResult
    {
        public IEnumerable<ShareMvt> Movements { get; set; } = new List<ShareMvt>();

        public TaxFile TaxResult { get; set; }

        public float SharesProfit { get; internal set; }

        public int Year { get; set; }
    }
}