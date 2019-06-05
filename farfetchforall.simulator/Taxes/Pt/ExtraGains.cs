namespace FarfetchForAll.Simulator.Taxes.Pt
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Scenario;

    internal class ExtraGains
    {
        private const float taxRate = 0.28F;

        public TaxParcel Calculate(IEnumerable<ShareMovement> movements)
        {
            TaxParcel taxParcel = new TaxParcel { Name = "Mais Valias" };

            var taxableAmount = movements
                .Where(i => i.SellIncome.HasValue)
                .Sum(i => i.SellIncome.Value - i.AcquisitionIncome);

            if (taxableAmount <= 0)
            {
                return taxParcel;
            }

            taxParcel.Amount = taxableAmount * taxRate;

            return taxParcel;
        }
    }
}