namespace FarfetchForAll.Simulator.Taxes
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Scenario;
    using FarfetchForAll.Simulator.Shares;

    public interface ITaxFileBuilder
    {
        ITaxFileBuilder With(FamilyAggregate familyAggregate);

        ITaxFileBuilder With(IEnumerable<ShareMvt> shareMvts);

        TaxFile Build();
    }
}