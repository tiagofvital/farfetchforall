namespace FarfetchForAll.Simulator.Taxes.Pt
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Scenario;
    using FarfetchForAll.Simulator.Shares;

    public class IRSTaxFileBuilder : ITaxFileBuilder
    {
        private FamilyCollectableIncome collectableIncome = new FamilyCollectableIncome();
        private ExtraGains extraGains = new ExtraGains();
        private TaxableIncome taxableIncome = new TaxableIncome();
        private TaxSettlement taxSettlement = new TaxSettlement();

        public float TaxSettling { get; private set; }

        public List<TaxParcel> Parcels { get; private set; } = new List<TaxParcel>();

        public FamilyAggregate AggregateInfo { get; private set; }

        public IEnumerable<ShareMvt> ShareMovements { get; private set; }

        public ITaxFileBuilder With(FamilyAggregate familyAggregate)
        {
            this.AggregateInfo = familyAggregate;

            return this;
        }

        public ITaxFileBuilder With(IEnumerable<ShareMvt> shareMvts)
        {
            this.ShareMovements = shareMvts;

            return this;
        }

        public TaxFile Build()
        {
            var result = new TaxFile();

            var incomeOnVest = this.ShareMovements
                .Where(i => i.MovementType == Shares.ShareMovementType.Acquisition)
                .Sum(i => i.ValueCostDiff);

            var extraGainTaxParcel = extraGains.Calculate(this.ShareMovements);

            var collectableAmountParcel = collectableIncome.Calculate(this.AggregateInfo, incomeOnVest);

            result.Parcels.Add(collectableAmountParcel);

            result.Parcels.Add(extraGainTaxParcel);

            var taxAmountParcel = taxableIncome.Calculate(collectableAmountParcel.Amount + extraGainTaxParcel.Amount, this.AggregateInfo);

            result.Parcels.Add(taxAmountParcel);

            var accountSettling = taxSettlement.Calculate(this.AggregateInfo.TaxPayed, taxAmountParcel.Amount);

            result.Parcels.Add(accountSettling);

            result.TaxSettling = accountSettling.Amount;

            return result;
        }
    }
}