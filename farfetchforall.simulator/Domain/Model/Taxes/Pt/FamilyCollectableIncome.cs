using FarfetchForAll.Simulator.Scenario;

namespace FarfetchForAll.Simulator.Taxes.Pt
{
    public class FamilyCollectableIncome
    {
        public TaxParcel Calculate(FamilyAggregate aggregateInfo, float amount)
        {
            var collectableAmount = aggregateInfo.AnualGain + amount - aggregateInfo.SpecificDeductions;
            return new TaxParcel
            {
                Name = "Rendimento Colectável",
                Amount = collectableAmount
            };
        }
    }
}