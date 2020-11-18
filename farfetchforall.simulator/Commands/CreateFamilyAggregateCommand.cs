namespace FarfetchForAll.Simulator.Commands
{
    using FarfetchForAll.Simulator.Scenario;
    using MediatR;

    public class CreateFamilyAggregateCommand : IRequest<FamilyAggregate>
    {
        public string Id { get; set; }
        public float SpecificDeductions { get; set; }

        public float AnualGain { get; set; }

        public float TaxPayed { get; set; }

        public float TaxDeductions { get; set; }

        public int FamilyCoeficient { get; set; }
    }
}