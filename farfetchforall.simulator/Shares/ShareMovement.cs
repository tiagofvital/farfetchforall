using FarfetchForAll.Simulator.Shares;

namespace FarfetchForAll.Simulator.Scenario
{
    public class ShareMovement
    {
        public ShareMovement(
            int amount,
            float shareValue,
            float shareValueOnAcquisition,
            float exercisePrice,
            ShareMovementType movementType,
            int year)
        {
            this.Amount = amount;
            this.Movement = movementType;
            this.AcquisitionCost = amount * exercisePrice;
            this.AcquisitionValue = amount * shareValueOnAcquisition;
            this.ShareValue = shareValue;
            this.AcquisitionIncome = this.AcquisitionValue - this.AcquisitionCost;

            if (movementType == ShareMovementType.Sell)
            {
                this.SellIncome = this.Amount * this.ShareValue - this.AcquisitionCost;
            }

            this.Year = year;
        }

        public int Amount { get; set; }

        public float AcquisitionCost { get; set; }

        public float ShareValue { get; set; }

        public float AcquisitionValue { get; set; }

        public ShareMovementType Movement { get; set; }

        public float AcquisitionIncome { get; private set; }

        public float? SellIncome { get; private set; }

        public int Year { get; set; }
    }
}