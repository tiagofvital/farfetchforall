namespace FarfetchForAll.Simulator.Domain.Services
{
    using FarfetchForAll.Simulator.DomainEvents;
    using FarfetchForAll.Simulator.Shares;

    public class ShareMovementDomainService
    {
        internal ShareMvt CreateFrom(ShareSold shareSold)
        {
            return new ShareMvt
            {
                MovementType = ShareMovementType.Sell,
                MovementYear = shareSold.Year,
                ShareValue = shareSold.ShareValue,
                ShareCost = shareSold.ShareCost,
                ExerciseCost = shareSold.ExerciseCost,
                ValueCostDiff = shareSold.ShareValue - shareSold.ShareCost,
                Income = shareSold.ShareValue - shareSold.ExerciseCost
            };
        }

        internal ShareMvt CreateFrom(ShareVested shareVested)
        {
            return new ShareMvt
            {
                MovementType = ShareMovementType.Acquisition,
                MovementYear = shareVested.Year,
                ShareValue = shareVested.ShareValue,
                ShareCost = shareVested.ExerciseCost,
                ExerciseCost = shareVested.ExerciseCost,
                ValueCostDiff = shareVested.ShareValue - shareVested.ExerciseCost,
                Income = 0
            };
        }
    }
}