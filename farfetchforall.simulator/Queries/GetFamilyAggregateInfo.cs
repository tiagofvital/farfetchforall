namespace FarfetchForAll.Simulator.Queries
{
    using FarfetchForAll.Simulator.Scenario;
    using MediatR;

    public class GetFamilyAggregateInfo : IRequest<FamilyAggregate>
    {
        public string Id { get; set; }
    }
}