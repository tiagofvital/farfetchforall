namespace FarfetchForAll.Simulator.Shares
{
    using FarfetchForAll.Simulator.DomainEvents;

    public class ShareMvt
    {
        public ShareMovementType MovementType { get; set; }

        public int MovementYear { get; set; }

        public float ShareValue { get; set; }

        public float ShareCost { get; set; }

        public float ExerciseCost { get; set; }

        public float ValueCostDiff { get; set; }

        public float Income { get; set; }

        public string TransactionId { get; set; }

        public static ShareMvt CreateFrom(ShareSold shareSold)
        {
            return new ShareMvt
            {
                MovementType = ShareMovementType.Sell,
                MovementYear = shareSold.Year,
                ShareValue = shareSold.ShareValue,
                ShareCost = shareSold.ShareCost,
                ExerciseCost = shareSold.ExerciseCost,
                ValueCostDiff = shareSold.ShareValue - shareSold.ShareCost,
                Income = shareSold.ShareValue - shareSold.ExerciseCost,
                TransactionId = shareSold.TransactionId
            };
        }

        public static ShareMvt CreateFrom(ShareVested shareVested)
        {
            return new ShareMvt
            {
                MovementType = ShareMovementType.Acquisition,
                MovementYear = shareVested.Year,
                ShareValue = shareVested.ShareValue,
                ShareCost = shareVested.ExerciseCost,
                ExerciseCost = shareVested.ExerciseCost,
                ValueCostDiff = shareVested.ShareValue - shareVested.ExerciseCost,
                Income = 0,
                TransactionId = shareVested.TransactionId
            };
        }
    }
}