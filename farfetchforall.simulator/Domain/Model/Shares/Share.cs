namespace FarfetchForAll.Simulator.Shares
{
    using System.Collections.Generic;
    using FarfetchForAll.Simulator.Domain.Model.Shares;
    using FarfetchForAll.Simulator.DomainEvents;
    using FarfetchForAll.Simulator.Events;
    using FarfetchForAll.Simulator.Shared;

    public class Share : Entity
    {
        public enum ShareState
        {
            None,
            Vested,
            Sold
        }

        public float Value { get; set; }

        public float ExerciseCost { get; set; }

        public int VestedYear { get; set; }

        public ShareState State { get; set; }

        public string FamillyAggregateId { get; set; }

        public ShareVested Vest(string transactionId)
        {
            Guard.Against(this.State == Share.ShareState.Vested, "Share already vested!");
            Guard.Against(this.State == Share.ShareState.Sold, "Share already Sold!");

            this.State = Share.ShareState.Vested;

            return new ShareVested
            {
                Year = this.VestedYear,
                ShareValue = this.Value,
                ExerciseCost = this.ExerciseCost,
                TransactionId = transactionId
            };
        }

        public ShareSold Sell(float shareValue, int year, string transactionId)
        {
            Guard.Against(this.State == Share.ShareState.Sold, "Share already Sold!");

            this.State = Share.ShareState.Sold;

            return new ShareSold
            {
                Year = year,
                ShareValue = shareValue,
                ShareCost = this.Value,
                ExerciseCost = this.ExerciseCost,
                TransactionId = transactionId
            };
        }

        public static IEnumerable<Share> From(VestShareCommand vestShareCommand)
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