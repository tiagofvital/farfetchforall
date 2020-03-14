namespace FarfetchForAll.Simulator.DomainEvents
{
    using MediatR;

    public class ShareVested : INotification
    {
        public float ShareValue { get; set; }

        public float ExerciseCost { get; set; }

        public int Year { get; set; }
    }
}