namespace FarfetchForAll.Simulator.Taxes.Pt
{
    public class TaxSettlement
    {
        public TaxParcel Calculate(float taxPayed, float amount)
        {
            return new TaxParcel
            {
                Name = "Acerto (Valor a Pagar / Receber)",
                Amount = taxPayed - amount
            };
        }
    }
}