namespace FarfetchForAll.Simulator.Taxes.Pt
{
    public class TaxRange
    {
        public float MinValue { get; set; }

        public float MaxValue { get; set; }

        public float Tax { get; set; }

        public float TaxValue { get; set; }

        public bool IsInRange(float value)
        {
            return this.MinValue < value && value <= this.MaxValue;
        }
    }
}