namespace FarfetchForAll.Simulator.Scenario
{
    using System;
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Shares;

    public class SharesInfo
    {
        public StockOption StockOptions { get; set; } = new StockOption();

        public List<ShareMovement> SharesMovements { get; private set; } = new List<ShareMovement>();

        public SharesInfo Buy(int amount, float shareValue, int year)
        {
            this.AddMovement(new ShareMovement(amount, shareValue, shareValue, this.StockOptions.Cost, ShareMovementType.Acquisition, year));
            return this;
        }

        public SharesInfo Sell(int amount, float shareValueOnAcquisition, float shareValue, int year)
        {
            this.AddMovement(new ShareMovement(amount, shareValue, shareValueOnAcquisition, this.StockOptions.Cost, ShareMovementType.Sell, year));
            return this;
        }

        public SharesInfo AddMovement(ShareMovement shareMovement)
        {
            this.SharesMovements.Add(shareMovement);
            return this;
        }

        public float ExerciseCost(int amount)
        {
            return amount * this.StockOptions.Cost;
        }

        public int SharesToCover(float amount, float shareValue)
        {
            return (int)Math.Round(amount / shareValue, 0);
        }
    }
}