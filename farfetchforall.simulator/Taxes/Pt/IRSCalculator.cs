namespace FarfetchForAll.Simulator.Taxes.Pt
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Scenario;

    public class IRSCalculator : ITaxCalculator
    {
        private FamilyCollectableIncome collectableIncome = new FamilyCollectableIncome();
        private ExtraGains extraGains = new ExtraGains();
        private TaxableIncome taxableIncome = new TaxableIncome();
        private TaxSettlement taxSettlement = new TaxSettlement();

        public TaxResult Run(TaxPayer aggregateInfo, IEnumerable<ShareMovement> shareMovements)
        {
            var result = new TaxResult();

            var incomeOnBuy = shareMovements
                .Where(i => i.Movement == Shares.ShareMovementType.Acquisition)
                .Sum(i => i.AcquisitionIncome);

            var extraGainTaxParcel = extraGains.Calculate(shareMovements);

            var collectableAmountParcel = collectableIncome.Calculate(aggregateInfo, incomeOnBuy);

            result.Parcels.Add(collectableAmountParcel);

            result.Parcels.Add(extraGainTaxParcel);

            var taxAmountParcel = taxableIncome.Calculate(collectableAmountParcel.Amount + extraGainTaxParcel.Amount, aggregateInfo);

            result.Parcels.Add(taxAmountParcel);

            var accountSettling = taxSettlement.Calculate(aggregateInfo.TaxPayed, taxAmountParcel.Amount);

            result.Parcels.Add(accountSettling);
            result.TaxToPay = accountSettling.Amount < 0 ? -1 * accountSettling.Amount : 0;

            var sharesTotal = shareMovements
                .Where(i => i.Movement == Shares.ShareMovementType.Sell)
                .Sum(i => i.Amount * i.ShareValue - i.AcquisitionCost);

            return result;
        }
    }
}