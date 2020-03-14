namespace FarfetchForAll.Simulator.Scenario
{
    public class TaxPayer
    {
        internal float SpecificDeductions;

        public float AnualGain { get; set; }

        public float TaxPayed { get; set; }

        public float TaxDeductions { get; set; }

        public int FamilyCoeficient { get; set; }
    }
}