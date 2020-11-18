namespace FarfetchForAll.Simulator.Taxes
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Scenario;
    using FarfetchForAll.Simulator.Shares;

    public interface ITaxCalculator
    {
        TaxResult Run(FamilyAggregate aggregateInfo, IEnumerable<ShareMvt> shareMovements);
    }
}