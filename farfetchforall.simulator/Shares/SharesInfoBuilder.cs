namespace FarfetchForAll.Simulator.Scenario
{
    public class SharesInfoBuilder
    {
        private float StockOptionsCost;
        private int StockOptionsAmount;

        public SharesInfoBuilder WithExercisePrice(float exerciseCost)
        {
            this.StockOptionsCost = exerciseCost;
            return this;
        }

        public SharesInfoBuilder WithStockOptions(int amount)
        {
            this.StockOptionsAmount = amount;
            return this;
        }

        public SharesInfo Build()
        {
            return new SharesInfo
            {
                StockOptions = new Shares.StockOption
                {
                    Amount = this.StockOptionsAmount,
                    Cost = this.StockOptionsCost
                }
            };
        }
    }
}