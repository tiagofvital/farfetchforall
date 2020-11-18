namespace FarfetchForAll.Simulator.Taxes.Pt
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Scenario;
    using FarfetchForAll.Simulator.Shares;

    public class IRSCalculator : ITaxCalculator
    {
        private FamilyCollectableIncome collectableIncome = new FamilyCollectableIncome();
        private ExtraGains extraGains = new ExtraGains();
        private TaxableIncome taxableIncome = new TaxableIncome();
        private TaxSettlement taxSettlement = new TaxSettlement();

        public TaxResult Run(FamilyAggregate aggregateInfo, IEnumerable<ShareMvt> shareMovements)
        {
            var result = new TaxResult();

            var incomeOnVest = shareMovements
                .Where(i => i.MovementType == Shares.ShareMovementType.Acquisition)
                .Sum(i => i.ValueCostDiff);

            var extraGainTaxParcel = extraGains.Calculate(shareMovements);

            var collectableAmountParcel = collectableIncome.Calculate(aggregateInfo, incomeOnVest);

            result.Parcels.Add(collectableAmountParcel);

            result.Parcels.Add(extraGainTaxParcel);

            var taxAmountParcel = taxableIncome.Calculate(collectableAmountParcel.Amount + extraGainTaxParcel.Amount, aggregateInfo);

            result.Parcels.Add(taxAmountParcel);

            var accountSettling = taxSettlement.Calculate(aggregateInfo.TaxPayed, taxAmountParcel.Amount);

            result.Parcels.Add(accountSettling);

            result.TaxSettling = accountSettling.Amount;

            return result;
        }
    }
}