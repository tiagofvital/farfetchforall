namespace FarfetchForAll.Simulator.Domain.Services
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.DomainEvents;
    using FarfetchForAll.Simulator.Events;
    using FarfetchForAll.Simulator.Shared;
    using FarfetchForAll.Simulator.Shares;

    public class ShareDomainService
    {
        public ShareSold Sell(Share share, float shareValue, int year)
        {
            Guard.AgainstNull(share, "Can't sell a null share!");

            Guard.Against(share.State == Share.ShareState.Sold, "Share already Sold!");

            share.State = Share.ShareState.Sold;

            return new ShareSold
            {
                Year = year,
                ShareValue = shareValue,
                ShareCost = share.Value,
                ExerciseCost = share.ExerciseCost
            };
        }

        public ShareVested Vest(Share share)
        {
            Guard.AgainstNull(share, "Can't vest a null share!");

            share.State = Share.ShareState.Vested;

            var shareEvt = new ShareVested
            {
                Year = share.VestedYear,
                ShareValue = share.Value,
                ExerciseCost = share.ExerciseCost
            };

            return shareEvt;
        }

        internal IEnumerable<Share> From(VestShareCommand vestShareCommand)
        {
            for (int i = 0; i < vestShareCommand.Amount; i++)
            {
                yield return new Share
                {
                    ExerciseCost = vestShareCommand.ExerciseCost,
                    Value = vestShareCommand.ShareValue,
                    VestedYear = vestShareCommand.Year
                };
            }
        }
    }
}