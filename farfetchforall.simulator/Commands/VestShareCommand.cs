namespace FarfetchForAll.Simulator.Events
{
    using MediatR;

    public class VestShareCommand : IRequest
    {
        public int Amount { get; set; }
        public float ShareValue { get; set; }
        public float ExerciseCost { get; set; }
        public int Year { get; set; }
    }
}