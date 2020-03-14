namespace FarfetchForAll.Simulator.Taxes.Pt
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Shares;

    internal class ExtraGains
    {
        private const float taxRate = 0.28F;

        public TaxParcel Calculate(IEnumerable<ShareMvt> movements)
        {
            TaxParcel taxParcel = new TaxParcel { Name = "Mais Valias" };

            var taxableAmount = movements
                .Where(i => i.MovementType == ShareMovementType.Sell)
                .Sum(i => i.ValueCostDiff);

            if (taxableAmount <= 0)
            {
                return taxParcel;
            }

            taxParcel.Amount = taxableAmount * taxRate;

            return taxParcel;
        }
    }
}