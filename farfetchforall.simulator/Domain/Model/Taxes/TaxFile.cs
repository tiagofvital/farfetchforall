namespace FarfetchForAll.Simulator.Taxes
{
    using System.Collections.Generic;

    public class TaxFile
    {
        public float TaxSettling { get; set; }

        public List<TaxParcel> Parcels { get; set; } = new List<TaxParcel>();
    }
}