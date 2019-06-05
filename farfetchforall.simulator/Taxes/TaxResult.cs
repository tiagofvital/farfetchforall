namespace FarfetchForAll.Simulator.Taxes
{
    using System.Collections.Generic;

    public class TaxResult
    {
        public float TaxToPay { get; set; }

        public List<TaxParcel> Parcels { get; set; } = new List<TaxParcel>();
    }
}