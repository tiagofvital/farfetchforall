namespace FarfetchForAll.Simulator.Shares
{
    using FarfetchForAll.Simulator.Domain.Model.Shares;

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
    }
}