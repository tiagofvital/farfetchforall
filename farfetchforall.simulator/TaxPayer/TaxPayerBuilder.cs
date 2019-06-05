namespace FarfetchForAll.Simulator.Scenario
{
    public class TaxPayerBuilder
    {
        public float AnualGain { get; private set; }
        public int FamilyCoeficient { get; private set; }
        public float TaxPayed { get; private set; }
        public float TaxDeductions { get; private set; }
        public float SpecificDeductions { get; private set; }

        public TaxPayerBuilder WithAnualGain(float anualGain)
        {
            this.AnualGain = anualGain;
            return this;
        }

        public TaxPayerBuilder WithFamilyCoeficient(int coeficient)
        {
            this.FamilyCoeficient = coeficient;
            return this;
        }

        public TaxPayerBuilder WithTaxPayed(float taxPayed)
        {
            this.TaxPayed = taxPayed;
            return this;
        }

        public TaxPayerBuilder WithDeductionsToTax(float deductions)
        {
            this.TaxDeductions = deductions;
            return this;
        }

        public TaxPayerBuilder WithSpecificDeductions(float deductions)
        {
            this.SpecificDeductions = deductions;
            return this;
        }

        public TaxPayer Build()
        {
            return new TaxPayer
            {
                AnualGain = this.AnualGain,
                FamilyCoeficient = this.FamilyCoeficient,
                SpecificDeductions = this.SpecificDeductions,
                TaxDeductions = this.TaxDeductions,
                TaxPayed = this.TaxPayed
            };
        }
    }
}