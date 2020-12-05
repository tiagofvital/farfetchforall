namespace FarfetchForAll.Simulator.API.Models
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Scenario;
    using FarfetchForAll.Simulator.Taxes;

    public class SimulationContextModel
    {
        public string Id { get; set; }

        public float SpecificDeductions { get; set; }

        public float AnnualGain { get; set; }

        public float TaxPayed { get; set; }

        public float TaxDeductions { get; set; }

        public int FamilyCoeficient { get; set; }
    }

    public class SimulationRunModel
    {
        public string Id { get; set; }

        public string SimulationId { get; set; }

        public FamilyAggregate FamilyAggregate { get; set; }

        public IEnumerable<YearResultModel> YearResults { get; set; }

        public float TotalGains { get; set; }

        public ShareModel Shares { get; set; }
    }

    public class ShareModel
    {
        public int TotalCount { get; set; }
    }

    public class YearResultModel
    {
        public int Year { get; set; }

        public TaxFile TaxResult { get; set; }

        public float SharesProfit { get; internal set; }

        public IEnumerable<MovementsModel> Movements { get; set; }
    }

    public class MovementsModel
    {
        public float ShareValue { get; set; }

        public float ShareCost { get; set; }

        public float ExerciseCost { get; set; }

        public MovementType Type { get; set; }
    }

    public enum MovementType
    {
        Vesting = 1,

        Selling = 2
    }
}