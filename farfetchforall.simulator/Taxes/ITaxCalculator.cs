namespace FarfetchForAll.Simulator.Taxes
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Scenario;

    public interface ITaxCalculator
    {
        TaxResult Run(TaxPayer aggregateInfo, IEnumerable<ShareMovement> shareMovements);
    }
}