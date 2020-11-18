namespace FarfetchForAll.Simulator.Events
{
    using System;
    using MediatR;

    public class SellShareCommand : IRequest
    {
        public int Amount { get; set; }
        public float ShareValue { get; set; }
        public int Year { get; set; }

        public string TransactionId { get; private set; } = Guid.NewGuid().ToString();
    }
}