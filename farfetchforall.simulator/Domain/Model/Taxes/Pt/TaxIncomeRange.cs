namespace FarfetchForAll.Simulator.Taxes.Pt
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Scenario;

    public class TaxableIncome
    {
        private readonly IEnumerable<TaxRange> ranges;

        public TaxableIncome()
        {
            this.ranges = new List<TaxRange>
            {
                new TaxRange { MinValue = 0F, MaxValue= 7091F, Tax = 14.5F, TaxValue = 0 },
                new TaxRange { MinValue = 7091F, MaxValue= 10700F, Tax = 23F, TaxValue = 602.74F },
                new TaxRange { MinValue = 10700F, MaxValue= 20261F, Tax = 28.5F, TaxValue = 1191.24F },
                new TaxRange { MinValue = 20261F, MaxValue= 25000F, Tax = 35F, TaxValue = 2508.11F },
                new TaxRange { MinValue = 25000F, MaxValue= 36856F, Tax = 37F, TaxValue = 3008.2F },
                new TaxRange { MinValue = 36856F, MaxValue= 80640F, Tax = 45F, TaxValue = 5956.68F },
                new TaxRange { MinValue = 80640F, MaxValue= float.MaxValue, Tax = 48F, TaxValue = 8375.88F },
            };
        }

        public TaxParcel Calculate(float income, TaxPayer aggregateInfo)
        {
            var individualIncome = income / aggregateInfo.FamilyCoeficient;

            var range = this.ranges.Single(i => i.IsInRange(individualIncome));

            var taxedIncome = range.Tax * individualIncome / 100;

            var tableIncome = range.TaxValue;

            var taxableIncome = (taxedIncome - range.TaxValue) * aggregateInfo.FamilyCoeficient;

            return new TaxParcel
            {
                Name = "Importância Liquida",
                Amount = taxableIncome - aggregateInfo.TaxDeductions
            };
        }
    }
}