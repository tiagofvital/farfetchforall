namespace FarfetchForAll.Simulator.Shares
{
    using System.Collections.Generic;
    using System.Linq;
    using FarfetchForAll.Simulator.Domain.Model.Shares;
    using FarfetchForAll.Simulator.DomainEvents;
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
    }

    public class ShareBuilder
    {
        private float value;

        private float exerciseCost;

        private int year;

        public ShareBuilder WithShareValue(float value)
        {
            this.value = value;
            return this;
        }

        public ShareBuilder WithExerciseCost(float exerciseCost)
        {
            this.exerciseCost = exerciseCost;
            return this;
        }

        public ShareBuilder WithYear(int year)
        {
            this.year = year;
            return this;
        }

        public IEnumerable<Share> Build(int amount)
        {
            return Enumerable.Range(1, amount)
                .Select(i => new Share
                {
                    ExerciseCost = this.exerciseCost,
                    Value = this.value,
                    VestedYear = this.year
                });
        }
    }
}