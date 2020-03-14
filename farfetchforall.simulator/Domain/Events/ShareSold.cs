namespace FarfetchForAll.Simulator.DomainEvents
{
    using MediatR;

    public class ShareSold : INotification
    {
        public float ShareValue { get; set; }

        public float ShareCost { get; set; }

        public float ExerciseCost { get; set; }

        public int Year { get; set; }
    }
}