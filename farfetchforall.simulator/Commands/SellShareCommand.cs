namespace FarfetchForAll.Simulator.Events
{
    using MediatR;
    public class SellShareCommand : IRequest
    {
        public int Amount { get; set; }
        public float ShareValue { get; set; }
        public int Year { get; set; }
    }
}